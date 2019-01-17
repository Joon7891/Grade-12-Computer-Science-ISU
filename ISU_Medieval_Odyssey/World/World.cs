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
        private const int LOADED_CHUNK_COUNT = 3;
        private Chunk[,] loadedChunks = new Chunk[LOADED_CHUNK_COUNT, LOADED_CHUNK_COUNT];
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
            // Creating terrtain generator
            terrainGenerator = new TerrainGenerator(seed);

            // Generating chunks around world and adding them to file
            for (int y = 0; y < LOADED_CHUNK_COUNT; ++y)
            {
                for (int x = 0; x < LOADED_CHUNK_COUNT; ++x)
                {
                    loadedChunks[x, y] = new Chunk(x, y, terrainGenerator);
                }
            }

            Vector2Int initialChunk = new Vector2Int(0, 0);
            Rectangle loadedRegion = new Rectangle(loadedChunks[0, 0].WorldPosition.X, loadedChunks[0, 0].WorldPosition.Y,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT);
            collisionTree = new CollisionTree(0, loadedRegion);

            // Setting up singleton
            Instance = this;
        }

        public void Update(GameTime gameTime)
        {
            // Shifting chunk and loading chunks if needed, if current chunk is not centered
            if (GameScreen.Instance.Player.CurrentChunk != loadedChunks[LOADED_CHUNK_COUNT / 2, LOADED_CHUNK_COUNT / 2].Position)
            {
                AdjustLoadedChunks(GameScreen.Instance.Player.CurrentChunk);
            }
            

            Rectangle loadedRegion = new Rectangle(loadedChunks[0, 0].Position.X, loadedChunks[0, 0].Position.Y,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT,
                                                   Tile.SPACING * Chunk.SIZE * LOADED_CHUNK_COUNT);
            

            
            collisionTree = new CollisionTree(0, loadedRegion);

            // Updating the projectiles and collision info in the world
            int projectileCount = projectiles.Count - 1;
            for (int i = projectileCount; i >= 0; i--)
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
            int liveItemsCount = liveItems.Count - 1;
            for (int i = liveItemsCount; i >= 0; --i)
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
            // Iterating through the chunk locations of the chunks that should be loaded
            for (int y = 0; y < LOADED_CHUNK_COUNT; ++y)
            {
                for (int x = 0; x < LOADED_CHUNK_COUNT; ++x)
                {
                    loadedChunks[x, y] = new Chunk(centerChunk.X + x - 1, centerChunk.Y + y - 1, terrainGenerator);
                }
            }
        }

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
            for (int y = 0; y < LOADED_CHUNK_COUNT; ++y)
            {
                for (byte x = 0; x < LOADED_CHUNK_COUNT; ++x)
                {
                    loadedChunks[x, y].Draw(spriteBatch);
                }
            }

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
            Vector2Int relativeCoordinate = chunkCoordinate - loadedChunks[0, 0].Position;
            return loadedChunks[relativeCoordinate.X, relativeCoordinate.Y];
        }

        public Tile GetTileAt(Vector2Int tileCoordinate)
        {
            Vector2Int chunkCoordinate = TileToChunkCoordinate(tileCoordinate);
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
