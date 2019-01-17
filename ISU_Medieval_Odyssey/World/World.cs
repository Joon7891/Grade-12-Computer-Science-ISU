// Author: Joon Song, Steven Ung
// File Name: World.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 1/15/2018
// Description: Class to hold World object / Information about the current map

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class World
    {
        /// <summary>
        /// Static instance of <see cref="World"/> - see singleton
        /// </summary>
        public static World Instance { get; set; }

        /// <summary>
        /// Whether the world is inside a <see cref="IBuilding"/>
        /// </summary>
        public bool IsInside { get; set; } = false;

        // The world's loaded chunks and loaded chunks
        private readonly TerrainGenerator terrainGenerator;
        private const int LOADED_CHUNK_COUNT = 5;
        private Dictionary<Vector2Int, Chunk> loadedChunks = new Dictionary<Vector2Int, Chunk>();
        private static Vector2Int[] borderChunkCoordinate =
        {
            new Vector2Int(2, 2),
            new Vector2Int(2, -2),
            new Vector2Int(-2, 2),
            new Vector2Int(-2, -2)
        };

        private List<Projectile> projectiles = new List<Projectile>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<LiveItem> liveItems = new List<LiveItem>();

        CollisionTree collisionTree;

        /// <summary>
        /// Constructor for <see cref="World"/> object
        /// </summary>
        /// <param name="seed">The seed of this <see cref="World"/></param>
        public World(int? seed = null)
        {
            // Vector to hold the current chunk location
            Vector2Int chunkLocation;

            // Creating terrtain generator
            terrainGenerator = new TerrainGenerator(seed);

            // Generating chunks around world and adding them to file
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    chunkLocation = new Vector2Int(i - 2, j - 2);
                    loadedChunks.Add(chunkLocation, new Chunk(chunkLocation, terrainGenerator));
                    IO.SaveChunk(loadedChunks[chunkLocation]);
                }
            }

            Vector2Int initialChunk = new Vector2Int(0, 0);
            Rectangle loadedRegion = new Rectangle(loadedChunks[initialChunk].WorldPosition.X, loadedChunks[initialChunk].WorldPosition.Y,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT);
            collisionTree = new CollisionTree(0, loadedRegion);

            // Setting up singleton
            Instance = this;

            loadedChunks[new Vector2Int(0, 0)][5, 5].OutsideObstructState = true;
        }

        public void Update(GameTime gameTime)
        {
            // Shifting chunk and loading chunks if needed, if current chunk is not centered
            foreach (Vector2Int adjustmentVector in borderChunkCoordinate)
            {
                if (!loadedChunks.ContainsKey(GameScreen.Instance.Player.CurrentChunk + adjustmentVector))
                {
                    AdjustLoadedChunks(GameScreen.Instance.Player.CurrentChunk);
                    break;
                }
            }

            // recreate new collision tree for new bounds
            Vector2Int playerCenter = GameScreen.Instance.Player.CurrentChunk - new Vector2Int(2, 2);
            Rectangle loadedRegion = new Rectangle(loadedChunks[playerCenter].WorldPosition.X, loadedChunks[playerCenter].WorldPosition.Y,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT);
            collisionTree = new CollisionTree(0, loadedRegion);

            // Updating the projectiles and collision info in the world
            for (int i = projectiles.Count- 1; i >= 0; i--)
            {
                projectiles[i].Update(gameTime);

                // Removing projectiles if they are not active
                if (!projectiles[i].Active)
                {
                    projectiles.RemoveAt(i);
                }

                collisionTree.Update(projectiles);
            }

            // Updating live items
            for (int i = liveItems.Count - 1; i >= 0; --i)
            {
                liveItems[i].Update(gameTime);

                if (!liveItems[i].Live)
                {
                    liveItems.RemoveAt(i);
                }
            }
        }

        public List<Projectile> CheckCollisions(Rectangle rectangle)
        {
            return collisionTree.ReturnCollisions(rectangle);
        } 

        /// <summary>
        /// Subprogram to adjust the loaded chunk
        /// </summary>
        /// <param name="centerChunk">A <see cref="Vector2Int"/> representing the center of the loaded chunk</param>
        public void AdjustLoadedChunks(Vector2Int centerChunk)        
        {
            // Vector to hold chunk location
            Vector2Int newChunkLocation;
            Vector2Int[] currentChunkLocations = loadedChunks.Keys.ToArray();

            foreach (Vector2Int chunkLocation in currentChunkLocations)
            {
                if (!(centerChunk.X - 2 <= chunkLocation.X && chunkLocation.X <= centerChunk.X + 2 &&
                      centerChunk.Y - 2 <= chunkLocation.Y && chunkLocation.Y <= centerChunk.Y + 2))
                {
                    IO.SaveChunk(loadedChunks[chunkLocation]);
                    loadedChunks.Remove(chunkLocation);
                }
            }

            // Iterating through the chunk locations of the chunks that should be loaded
            for (int x = centerChunk.X - 2; x <= centerChunk.X + 2; ++x)
            {
                for (int y = centerChunk.Y - 2; y <= centerChunk.Y + 2; ++y)
                {
                    newChunkLocation = new Vector2Int(x, y);
                   
                    // If this chunk isn't loaded, load it
                    if (!loadedChunks.ContainsKey(newChunkLocation))
                    {
                        // If the chunk is in file, load it, otherwise construct it
                        loadedChunks.Add(newChunkLocation, GetChunkAt(newChunkLocation));
                    }
                }
            }
        }

        Shop test = new Shop(new Vector2Int(0, 0));

        /// <summary>
        /// Subprogram to 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="camera"></param>
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            // Beginning spritebatch in adjusted camera
            spriteBatch.Begin(transformMatrix: camera.ViewMatrix, samplerState: SamplerState.PointClamp);

            // Drawing the various loaded chunks
            foreach (Chunk chunk in loadedChunks.Values.OrderBy(chunk => chunk.Position.Y))
            {
                chunk.Draw(spriteBatch);
            }

            test.DrawInside(spriteBatch);

            // Drawing projectiles
            for (int i = 0; i < projectiles.Count; ++i)
            {
                projectiles[i].Draw(spriteBatch);
            }

            // Drawing live items
            for (int i = 0; i < liveItems.Count; ++i)
            {
                liveItems[i].Draw(spriteBatch);
            }

            // Ending spriteBatch
            spriteBatch.End();
        }

        public Chunk GetChunkAt(Vector2Int chunkCoordinate)
        {
            Chunk newChunk;

            if (loadedChunks.ContainsKey(chunkCoordinate))
            {
                newChunk = loadedChunks[chunkCoordinate];
            }
            else if (IO.ChunkExists(chunkCoordinate))
            {
                newChunk = IO.LoadChunk(chunkCoordinate);
            }
            else
            {
                newChunk = new Chunk(chunkCoordinate, terrainGenerator);
                IO.SaveChunk(newChunk);
            }

            return newChunk;
        }

        public Tile GetTileAt(Vector2Int tileCoordinate)
        {
            Vector2Int chunkCoordinate = World.TileToChunkCoordinate(tileCoordinate);
            Vector2Int newTileCoordinate = tileCoordinate - chunkCoordinate * Chunk.SIZE;
            return GetChunkAt(chunkCoordinate)[newTileCoordinate.X, newTileCoordinate.Y];
        }

        public bool IsTileObstructed(Vector2Int tileCoordinate)
        {
            Tile tile = GetTileAt(tileCoordinate);

            if (IsInside)
            {
                return tile.InsideObstructState;
            }

            return tile.OutsideObstructState;
        }

        /// <summary>
        /// Subprogram to add a projectile into this <see cref="World"/>
        /// </summary>
        /// <param name="projectile">The <see cref="Projectile"/> to be added</param>
        public void AddProjectile(Projectile projectile)
        {
            // Adding projectile
            projectiles.Add(projectile);
        }

        public void AddItem(Item item, Rectangle rectangle)
        {
            rectangle.Y = rectangle.Y + (rectangle.Height - ItemSlot.SIZE) / 2;
            rectangle.X = rectangle.X + (rectangle.Width - ItemSlot.SIZE) / 2;
            rectangle.Width = ItemSlot.SIZE;
            rectangle.Height = ItemSlot.SIZE;
            liveItems.Add(new LiveItem(item, rectangle));
        }

        public Item RetrieveItem(Player player)
        {
            Item retrievedItem = null;

            for (int i = 0; i < liveItems.Count; ++i)
            {
                if (player.CollisionRectangle.Intersects(liveItems[i].Rectangle))
                {
                    retrievedItem = liveItems[i].Item;
                    liveItems.RemoveAt(i);
                    break;
                }
            }

            return retrievedItem;
        }

        public static Vector2Int PixelToTileCoordinate(Vector2Int pixelCoordinate)
        {
            Vector2Int tileCoordinate = Vector2Int.Zero;
            tileCoordinate.X = (int)Math.Floor(pixelCoordinate.X / (float)Tile.SPACING);
            tileCoordinate.Y = (int)Math.Floor(pixelCoordinate.Y / (float)Tile.SPACING);
            return tileCoordinate;
        }

        public static Vector2Int TileToChunkCoordinate(Vector2Int tileCoordinate)
        {
            Vector2Int chunkCoordinate = Vector2Int.Zero;
            chunkCoordinate.X = (int)Math.Floor(tileCoordinate.X / (float)Chunk.SIZE);
            chunkCoordinate.Y = (int)Math.Floor(tileCoordinate.Y / (float)Chunk.SIZE);
            return chunkCoordinate;
        }
    }
}
