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
using Newtonsoft.Json;

namespace ISU_Medieval_Odyssey
{
    public sealed class World
    {
        /// <summary>
        /// Static instance of <see cref="World"/> - see singleton
        /// </summary>
        [JsonIgnore]
        public static World Instance { get; set; }

        /// <summary>
        /// Whether the world is inside a <see cref="IBuilding"/>
        /// </summary>
        public bool IsInside { get; set; } = false;

        /// <summary>
        /// The current <see cref="IBuilding"/> the <see cref="Player"/> is in
        /// </summary>
        public IBuilding CurrentBuilding { get; set; }

        // The world's loaded chunks and loaded chunks
        [JsonProperty]
        private readonly TerrainGenerator terrainGenerator;
        private const int CHUNK_COUNT = 3;
        private Chunk[,] loadedChunks = new Chunk[CHUNK_COUNT, CHUNK_COUNT];
        private static Vector2Int[] borderChunkCoordinate =
        {
            new Vector2Int(2, 2),
            new Vector2Int(2, -2),
            new Vector2Int(-2, 2),
            new Vector2Int(-2, -2)
        };
        
        // List of various entities drawn above the world tilemap
        private List<Enemy> enemies = new List<Enemy>();
        private List<LiveItem> liveItems = new List<LiveItem>();
        private List<IBuilding> buildings = new List<IBuilding>();
        private List<Projectile> projectiles = new List<Projectile>();

        // The world bounds and a quadtree for collision detection
        private Rectangle worldBoundsRect;
        private CollisionTree collisionTree;

        private Shop test;

        /// <summary>
        /// Constructor for <see cref="World"/> object
        /// </summary>
        /// <param name="seed">The seed of this <see cref="World"/></param>
        public World(int? seed = null)
        {
            // Setting up singleton
            Instance = this;

            // Creating terrtain generator
            terrainGenerator = new TerrainGenerator(seed);

            // Generating chunks around world and adding them to file after clearing previously existing world
            IO.DeleteWorld();
            for (int y = 0; y < CHUNK_COUNT; ++y)
            {
                for (int x = 0; x < CHUNK_COUNT; ++x)
                {
                    loadedChunks[x, y] = InitializeChunkAt(x, y);
                }
            }
            AdjustLoadedChunks(Player.Instance.CurrentChunk);

            // Setting up world boundaries and quadtree
            worldBoundsRect = new Rectangle(loadedChunks[0, 0].WorldPosition.X, loadedChunks[0, 0].WorldPosition.Y,
                                                   Tile.SPACING * Chunk.SIZE * CHUNK_COUNT, Tile.SPACING * Chunk.SIZE * CHUNK_COUNT);
            collisionTree = new CollisionTree(1, worldBoundsRect);

            test = new Shop(new Vector2Int(1, 1));

        }

        public void Update(GameTime gameTime)
        {
            // Shifting chunk and loading chunks if needed, if current chunk is not centered
            if (Player.Instance.CurrentChunk != loadedChunks[CHUNK_COUNT / 2, CHUNK_COUNT / 2].Position)
            {
                AdjustLoadedChunks(Player.Instance.CurrentChunk);
            }

            worldBoundsRect.X = loadedChunks[0, 0].WorldPosition.X * Tile.SPACING;
            worldBoundsRect.Y = loadedChunks[0, 0].WorldPosition.Y * Tile.SPACING;
            collisionTree.Range = worldBoundsRect;

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

        /// <summary>
        /// Subprogram to adjust the loaded chunk
        /// </summary>
        /// <param name="centerChunk">A <see cref="Vector2Int"/> representing the center of the loaded chunk</param>
        public void AdjustLoadedChunks(Vector2Int centerChunk)        
        {
            // The newly loaded chunks
            Chunk[,] newLoadedChunks = new Chunk[CHUNK_COUNT, CHUNK_COUNT];
            
            // Iterating through chunks that should be loaded and setting it
            for (int y = 0; y < CHUNK_COUNT; ++y)
            {
                for (int x = 0; x < CHUNK_COUNT; ++x)
                {
                    newLoadedChunks[x, y] = GetChunkAt(centerChunk.X + x - CHUNK_COUNT / 2, centerChunk.Y + y - CHUNK_COUNT / 2);
                }
            }

            // Setting the newly loaded chunks as current loaded chunks
            loadedChunks = newLoadedChunks;
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

            // Drawing chunks and outside of buildings if player is outside, otherwise draw current building inside
            if (!IsInside)
            {
                // Drawing the various loaded chunks
                for (int y = 0; y < CHUNK_COUNT; ++y)
                {
                    for (byte x = 0; x < CHUNK_COUNT; ++x)
                    {
                        loadedChunks[x, y].Draw(spriteBatch);
                    }
                }

                for (int i = 0; i < buildings.Count; ++i)
                {
                    buildings[i].DrawOutside(spriteBatch);
                }
            }
            else
            {
                CurrentBuilding?.DrawInside(spriteBatch);
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
        
        public Chunk GetChunkAt(int x, int y)
        {
            Chunk chunk;
            int relativeX = x - loadedChunks[0, 0].Position.X;
            int relativeY = y - loadedChunks[0, 0].Position.Y;

            if (0 <= relativeX && relativeX < CHUNK_COUNT && 0 <= relativeY && relativeY < CHUNK_COUNT)
            {
                chunk = loadedChunks[relativeX, relativeY];
            }
            else if (IO.ChunkExists(x, y))
            {
                chunk = IO.LoadChunk(x, y);
            }
            else
            {
                chunk = new Chunk(x, y, terrainGenerator);
                IO.SaveChunk(chunk);
            }

            return chunk;
        }

        public Chunk InitializeChunkAt(int x, int y)
        {
            Chunk chunk = null;

            if (IO.ChunkExists(x, y))
            {
                chunk = IO.LoadChunk(x, y);
            }
            else
            {
                chunk = new Chunk(x, y, terrainGenerator);
            }

            return chunk;
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
            List<LiveItem> hitItems = collisionTree.GetCollisions(player.CollisionRectangle, liveItems);
            
            if (hitItems.Count > 0)
            {
                retrievedItem = hitItems[0].Item;
                liveItems.Remove(hitItems[0]);
            }

            return retrievedItem;
        }

        public string Serialize() => JsonConvert.SerializeObject(this);

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
