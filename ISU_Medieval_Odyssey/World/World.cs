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

        // List of various entities drawn above the world tilemap
        private List<Enemy> enemies = new List<Enemy>();
        private List<LiveItem> liveItems = new List<LiveItem>();
        private List<IBuilding> buildings = new List<IBuilding>();
        private List<Projectile> projectiles = new List<Projectile>();

        /// <summary>
        /// The rectangle representing the bounds of this <see cref="World"/>
        /// </summary>
        public Rectangle WorldBoundsRect => worldBoundsRect;

        // The world bounds and a quadtree for collision detection
        private CollisionTree collisionTree;
        private Rectangle worldBoundsRect;

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

            // Setting up quadtree and search range
            worldBoundsRect = new Rectangle(0, 0, Tile.SPACING * Chunk.SIZE * CHUNK_COUNT, Tile.SPACING * Chunk.SIZE * CHUNK_COUNT);
            collisionTree = new CollisionTree(worldBoundsRect);

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

            enemies.Add(new Witch(new Vector2Int(1, 1)));
            buildings.Add(new Shop(new Vector2Int(2, 2)));
        }

        /// <summary>
        /// Subprogram to initialize a <see cref="Chunk"/>
        /// </summary>
        /// <param name="x">The x-coordinate of the <see cref="Chunk"/></param>
        /// <param name="y">The y-coordinate of the <see cref="Chunk"/></param>
        /// <returns>The initalized chunk</returns>
        private Chunk InitializeChunkAt(int x, int y)
        {
            // The chunk being initlaized
            Chunk chunk = null;

            // Reading chunk from file, if it exists
            if (IO.ChunkExists(x, y))
            {
                chunk = IO.LoadChunk(x, y);
            }
            else
            {
                // Otherwise, creating it and saving it to file
                chunk = new Chunk(x, y, terrainGenerator);
                IO.SaveChunk(chunk);
            }

            // Returning initialized chunk
            return chunk;
        }

        /// <summary>
        /// Update subprogram for <see cref="World"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Shifting chunk and loading chunks if needed, if current chunk is not centered
            if (Player.Instance.CurrentChunk != loadedChunks[CHUNK_COUNT / 2, CHUNK_COUNT / 2].Position)
            {
                AdjustLoadedChunks(Player.Instance.CurrentChunk);
            }

            // Updating enemies
            for (int i = enemies.Count - 1; i >= 0; --i)
            {
                enemies[i].Update(gameTime);

                // Removing enemies and giving player loot
                if (!enemies[i].Alive)
                {
                    Player.Instance.Experience += enemies[i].Experience;
                    enemies.RemoveAt(i);
                }
            }

            // Updating current building if inside a building 
            if (IsInside)
            {
                CurrentBuilding.Update(gameTime);
            }

            List<Enemy> projectileHits = new List<Enemy>();

            // Updating the projectiles and collision info in the world
            for (int i = projectiles.Count - 1; i >= 0; --i)
            {
                projectiles[i].Update(gameTime);

                // Removing projectiles if they are not active
                if (!projectiles[i].Active)
                {
                    projectiles.RemoveAt(i);
                    continue;
                }

                projectileHits = collisionTree.GetCollisions(projectiles[i].HitBox, enemies);

                for (int j = 0; j < projectileHits.Count; ++j)
                {
                    projectileHits[j].Health -= projectiles[i].DamageAmount;
                }

                if (projectileHits.Count > 0)
                {
                    projectiles.RemoveAt(i);
                }
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

        /// <summary>
        /// Subprogram to adjust the loaded chunk
        /// </summary>
        /// <param name="centerChunk">A <see cref="Vector2Int"/> representing the center of the loaded chunk</param>
        public void AdjustLoadedChunks(Vector2Int centerChunk)        
        {
            // The newly loaded chunks
            Chunk[,] newLoadedChunks = new Chunk[CHUNK_COUNT, CHUNK_COUNT];
            
            // Iterating through chunks that should be loaded and setting it
            for (short y = 0; y < CHUNK_COUNT; ++y)
            {
                for (short x = 0; x < CHUNK_COUNT; ++x)
                {
                    newLoadedChunks[x, y] = GetChunkAt(centerChunk.X + x - CHUNK_COUNT / 2, centerChunk.Y + y - CHUNK_COUNT / 2);
                }
            }
            loadedChunks = newLoadedChunks;

            // Setting the newly loaded chunks as current loaded chunks
            worldBoundsRect.X = loadedChunks[0, 0].Position.X * Tile.SPACING;
            worldBoundsRect.Y = loadedChunks[0, 0].Position.Y * Tile.SPACING;
            collisionTree.Range = worldBoundsRect;
        }

        /// <summary>
        /// Draw subprogram for <see cref="World"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing appropriate data, depending whether the player is inside or outside
            if (!IsInside)
            {
                // Drawing the various loaded chunks of the world
                for (int y = 0; y < CHUNK_COUNT; ++y)
                {
                    for (byte x = 0; x < CHUNK_COUNT; ++x)
                    {
                        loadedChunks[x, y].Draw(spriteBatch);
                    }
                }

                // Drawing the outsides of various buildings
                for (int i = 0; i < buildings.Count; ++i)
                {
                    buildings[i].DrawOutside(spriteBatch);
                }
            }
            else
            {
                // Drawing the current building the player is in
                CurrentBuilding.DrawInside(spriteBatch);
            }
            

            // Drawing enemies
            for (short i = 0; i < enemies.Count; ++i)
            {
                enemies[i].Draw(spriteBatch);
            }

            // Drawing projectiles
            for (byte i = 0; i < projectiles.Count; ++i)
            {
                projectiles[i].Draw(spriteBatch);
            }

            // Drawing live items
            for (short i = 0; i < liveItems.Count; ++i)
            {
                liveItems[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Subprogram to draw the various loaded <see cref="Chunk"/>s of this <see cref="World"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void DrawMini(SpriteBatch spriteBatch)
        {
            // Drawing the various loaded chunks
            for (int y = 0; y < CHUNK_COUNT; ++y)
            {
                for (byte x = 0; x < CHUNK_COUNT; ++x)
                {
                    loadedChunks[x, y].Draw(spriteBatch);
                }
            }

            // Drawing enemies in mini form
            for (short i = 0; i < enemies.Count; ++i)
            {
                enemies[i].DrawMini(spriteBatch);
            }

            // Drawing the various buildings
            for (int i = 0; i < buildings.Count; ++i)
            {
                buildings[i].DrawOutside(spriteBatch);
            }
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

        /// <summary>
        /// Subprogram to add an <see cref="Item"/> to the <see cref="World"/>
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to be added</param>
        /// <param name="playerRectangle">The <see cref="Rectangle"/> of the <see cref="Player"/> who is adding the item</param>
        public void AddItem(Item item, Rectangle playerRectangle)
        {
            // Adjusting rectangle and adding item as a LiveItem
            playerRectangle.Y = playerRectangle.Y + (playerRectangle.Height - ItemSlot.SIZE) / 2;
            playerRectangle.X = playerRectangle.X + (playerRectangle.Width - ItemSlot.SIZE) / 2;
            playerRectangle.Width = ItemSlot.SIZE;
            playerRectangle.Height = ItemSlot.SIZE;
            liveItems.Add(new LiveItem(item, playerRectangle));
        }

        /// <summary>
        /// Subprogram to add a <see cref="Enemy"/>
        /// </summary>
        /// <param name="enemy">The <see cref="Enemy"/> to be added</param>
        public void AddEnemy(Enemy enemy)
        {
            // Adding enemies to world
            enemies.Add(enemy);
        }

        /// <summary>
        /// Subprogram to retrieve an <see cref="Item"/> that the <see cref="Player"/> is on top of
        /// </summary>
        /// <param name="player">The <see cref="Player"/> retrieving the <see cref="Item"/></param>
        /// <returns>The retrieved <see cref="Item"/> if it exists</returns>
        public Item RetrieveItem(Player player)
        {
            // The items the player is on top of
            Item retrievedItem = null;
            List<LiveItem> hitItems = collisionTree.GetCollisions(player.HitBox, liveItems);
            
            // Retrieving item if the player is on top of one
            if (hitItems.Count > 0)
            {
                retrievedItem = hitItems[0].Item;
                liveItems.Remove(hitItems[0]);
            }

            // Returning the retrieved item
            return retrievedItem;
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
                //AddBuildingAt(x, y);
                chunk = new Chunk(x, y, terrainGenerator);
                IO.SaveChunk(chunk);
            }

            return chunk;
        }

        /// <summary>
        /// Subprogram to return the <see cref="Tile"/> at a specified <see cref="Tile"/> coordinate
        /// </summary>
        /// <param name="tileCoordinate">The coordinate of the <see cref="Tile"/> to retrieve</param>
        /// <returns>The <see cref="Tile"/> at the specified coordinate</returns>
        public Tile GetTileAt(Vector2Int tileCoordinate)
        {
            // Calculating other relevant coordinates and returning corresponding tile
            Vector2Int chunkCoordinate = TileToChunkCoordinate(tileCoordinate);
            Vector2Int newTileCoordinate = tileCoordinate - chunkCoordinate * Chunk.SIZE;
            return GetChunkAt(chunkCoordinate.X, chunkCoordinate.Y)[newTileCoordinate.X, newTileCoordinate.Y];
        }

        /// <summary>
        /// Subprogram to determine if a certain tile is obstructed
        /// </summary>
        /// <param name="tileCoordinate"></param>
        /// <returns></returns>
        public bool IsTileObstructed(Vector2Int tileCoordinate)
        {
            // The tile thaty may be obstructed
            Tile tile = GetTileAt(tileCoordinate);

            // If the player is inside a building return the inside obstruct state
            if (IsInside)
            {
                return tile.InsideObstructState;
            }

            // Otherwise return the outside obstruct state
            return tile.OutsideObstructState;
        }

        /// <summary>
        /// Subprogram to convert a pixel coordinate into a <see cref="Tile"/> coordinate
        /// </summary>
        /// <param name="pixelCoordinate">The pixel coordinate to be converted</param>
        /// <returns>The converted <see cref="Tile"/> coordinate</returns>
        public static Vector2Int PixelToTileCoordinate(Vector2Int pixelCoordinate)
        {
            // Calculating tile coordinate and returning it
            Vector2Int tileCoordinate = Vector2Int.Zero;
            tileCoordinate.X = (int)Math.Floor(pixelCoordinate.X / (float)Tile.SPACING);
            tileCoordinate.Y = (int)Math.Floor(pixelCoordinate.Y / (float)Tile.SPACING);
            return tileCoordinate;
        }

        /// <summary>
        /// Subprogram to convert a <see cref="Tile"/> coordinate to a <see cref="Chunk"/> coordinate
        /// </summary>
        /// <param name="tileCoordinate">The <see cref="Tile"/> coordinate to be converted</param>
        /// <returns>The converted <see cref="Chunk"/> coordinate</returns>
        public static Vector2Int TileToChunkCoordinate(Vector2Int tileCoordinate)
        {
            // Calculating chunk coordinate and returning it
            Vector2Int chunkCoordinate = Vector2Int.Zero;
            chunkCoordinate.X = (int)Math.Floor(tileCoordinate.X / (float)Chunk.SIZE);
            chunkCoordinate.Y = (int)Math.Floor(tileCoordinate.Y / (float)Chunk.SIZE);
            return chunkCoordinate;
        }

        /// <summary>
        /// Subprogram to serialize this <see cref="World"/>
        /// </summary>
        /// <returns>A <see cref="string"/> representing this <see cref="World"/>'s data</returns>
        public string Serialize() => JsonConvert.SerializeObject(this);
    }
}
