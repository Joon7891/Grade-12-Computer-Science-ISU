// Author: Joon Song, Steven Ung
// File Name: World.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 1/15/2018
// Description: Class to hold World object / Information about the current map

using System;
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
        [JsonIgnore]
        public IBuilding CurrentBuilding { get; set; }

        /// <summary>
        /// The number of <see cref="Enemy"/> in this <see cref="World"/>
        /// </summary>
        [JsonIgnore]
        public int EnemiesLoaded => enemies.Count;

        // The world's loaded chunks and loaded chunks
        [JsonProperty]
        private readonly TerrainGenerator terrainGenerator;
        private const int CHUNK_COUNT = 3;
        private Chunk[,] loadedChunks = new Chunk[CHUNK_COUNT, CHUNK_COUNT];

        // List of various entities drawn above the world tilemap
        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> hitEnemies = new List<Enemy>();
        private List<LiveItem> liveItems = new List<LiveItem>();
        private List<IBuilding> buildings = new List<IBuilding>();
        private List<Projectile> projectiles = new List<Projectile>();
        public List<Enemy> DungeonEnemies { get; set; } = new List<Enemy>();

        // A set of cached buildings
        [JsonProperty]
        private HashSet<IBuilding> cachedBuildings = new HashSet<IBuilding>();
        private HashSet<Vector2Int> visitedChunks = new HashSet<Vector2Int>();
        private Dictionary<Vector2Int, Chunk> cachedChunks = new Dictionary<Vector2Int, Chunk>();

        /// <summary>
        /// The rectangle representing the bounds of this <see cref="World"/>
        /// </summary>
        public Rectangle WorldBoundsRect => worldBoundsRect;

        // The world bounds and a quadtree for collision detection
        private CollisionTree collisionTree;
        private Rectangle worldBoundsRect;

        // Variables related to enemy generation throughout the world
        private float enemyGenerationTimer = 0;
        private const int ENEMY_GENERATE_TIME = 3;
        private int chunkBounaryID;
        private Vector2Int[] chunkBoundaries =
        {
            new Vector2Int(0, Chunk.SIZE),
            new Vector2Int(0, -Chunk.SIZE),
            new Vector2Int(Chunk.SIZE, 0),
            new Vector2Int(-Chunk.SIZE, 0),
        };

        /// <summary>
        /// Constructor for <see cref="World"/> object
        /// </summary>
        /// <param name="seed">The seed of this <see cref="World"/></param>
        public World(int? seed = null)
        {
            // Setting up singleton
            Instance = this;

            // Setting up quadtree and search range
            worldBoundsRect = new Rectangle(0, 0, Tile.SPACING * Chunk.SIZE * CHUNK_COUNT, Tile.SPACING * Chunk.SIZE * CHUNK_COUNT);
            collisionTree = new CollisionTree(worldBoundsRect);

            // Creating terrtain generator and generating terrain
            terrainGenerator = new TerrainGenerator(seed);
            AdjustLoadedChunks(new Vector2Int(0, 0));
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
            Vector2Int coordinate = new Vector2Int(x, y);
            Chunk chunk = null;

            // Creating chunk and caching it
            chunk = new Chunk(x, y, terrainGenerator);
            cachedChunks.Add(coordinate, chunk);
            
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

            // Updating current building if inside a building 
            if (IsInside)
            {
                CurrentBuilding.Update(gameTime);
            }

            // Generating enemies every 3s
            enemyGenerationTimer += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (enemyGenerationTimer > ENEMY_GENERATE_TIME)
            {
                chunkBounaryID = SharedData.RNG.Next(chunkBoundaries.Length);

                if (!GetTileAt(chunkBoundaries[chunkBounaryID] + Player.Instance.CurrentChunk).OutsideObstructState)
                {
                    enemies.Add(Enemy.RandomEnemy(chunkBoundaries[chunkBounaryID] + Player.Instance.CurrentTile, false));
                }
                enemyGenerationTimer = 0;
            }

            // Updating enemies
            if (!IsInside)
            {
                for (int i = enemies.Count - 1; i >= 0; --i)
                {
                    enemies[i].Update(gameTime);

                    // Removing enemies if they die and giving player corresponding loot
                    if (!enemies[i].Alive)
                    {
                        Player.Instance.Experience += enemies[i].Experience;
                        Player.Instance.Gold += enemies[i].Gold;
                        AddItems(enemies[i].HitBox, enemies[i].LootTable);
                        enemies.RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = DungeonEnemies.Count - 1; i >= 0; --i)
                {
                    DungeonEnemies[i].Update(gameTime);

                    // Removing enemies if they die and giving player corresponding loot
                    if (!DungeonEnemies[i].Alive)
                    {
                        Player.Instance.Experience += DungeonEnemies[i].Experience;
                        Player.Instance.Gold += DungeonEnemies[i].Gold;
                        AddItems(DungeonEnemies[i].HitBox, DungeonEnemies[i].LootTable);
                        DungeonEnemies.RemoveAt(i);
                    }
                }
            }

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

                // Inflicting damage and removing projectile if it hits a enemy
                if (IsInside)
                {
                    hitEnemies.AddRange(collisionTree.GetCollisions(projectiles[i].HitBox, DungeonEnemies));
                }
                else
                {
                    hitEnemies = collisionTree.GetCollisions(projectiles[i].HitBox, enemies);
                }
                
                for (int j = 0; j < hitEnemies.Count; ++j)
                {
                    hitEnemies[j].Health -= projectiles[i].DamageAmount;
                }
                if (hitEnemies.Count > 0)
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

            // Removing enemies outside of the loaded world
            for (int i = enemies.Count - 1; i >= 0; --i)
            {
                if (!worldBoundsRect.Contains(enemies[i].HitBox))
                {
                    enemies.RemoveAt(i);
                }
            }
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

            // Drawing enemies
            if (!IsInside)
            {
                for (short i = 0; i < enemies.Count; ++i)
                {
                    enemies[i].Draw(spriteBatch);
                }
            }
            else
            {
                for(int i = 0; i < DungeonEnemies.Count; i++)
                {
                    DungeonEnemies[i].Draw(spriteBatch);
                }
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
        /// Subprogram to adjust the loaded chunks
        /// </summary>
        /// <param name="centerChunk">A <see cref="Vector2Int"/> representing the center of the loaded chunk</param>
        public void AdjustLoadedChunks(Vector2Int centerChunk)
        {
            // The newly loaded chunks and difference
            Vector2Int currentChunkCoodinate = Vector2Int.Zero;
            Chunk[,] newLoadedChunks = new Chunk[CHUNK_COUNT, CHUNK_COUNT];

            // Iterating through chunks that should be loaded and setting it
            for (short y = 0; y < CHUNK_COUNT; ++y)
            {
                for (short x = 0; x < CHUNK_COUNT; ++x)
                {
                    currentChunkCoodinate.X = centerChunk.X + x - CHUNK_COUNT / 2;
                    currentChunkCoodinate.Y = centerChunk.Y + y - CHUNK_COUNT / 2;
                    newLoadedChunks[x, y] = GetChunkAt(currentChunkCoodinate.X, currentChunkCoodinate.Y);
                }
            }
            loadedChunks = newLoadedChunks;

            // Setting the newly loaded chunks as current loaded chunks
            worldBoundsRect.X = loadedChunks[0, 0].Position.X * Tile.SPACING * Chunk.SIZE;
            worldBoundsRect.Y = loadedChunks[0, 0].Position.Y * Tile.SPACING * Chunk.SIZE;
            collisionTree.Range = worldBoundsRect;

            // Removing buildings outside of loaded chunks 
            for (int i = buildings.Count - 1; i >= 0; --i)
            {
                if (!worldBoundsRect.Contains(buildings[i].Rectangle))
                {
                    buildings.RemoveAt(i);
                }
            }

            // Adding buildings that are now in the world
            foreach (IBuilding building in cachedBuildings)
            {
                if (worldBoundsRect.Contains(building.Rectangle) && !buildings.Contains(building))
                {
                    buildings.Add(building);
                    building.SetTiles();
                }
            }
        }

        /// <summary>
        /// Subprogram to retrieve a <see cref="Chunk"/> at a specified <see cref="Chunk"/> coordinate
        /// </summary>
        /// <param name="x">The x-component of the <see cref="Chunk"/></param>
        /// <param name="y">The y-component of the <see cref="Chunk"/></param>
        /// <returns>The <see cref="Chunk"/> at the query</returns>
        public Chunk GetChunkAt(int x, int y)
        {
            // Various variables required for chunk searching in memory
            Chunk chunk;
            Vector2Int chunkCoordinate = new Vector2Int(x, y);

            if (cachedChunks.ContainsKey(chunkCoordinate))
            {
                chunk = cachedChunks[chunkCoordinate];
            }
            else
            {
                chunk = new Chunk(x, y, terrainGenerator);
                cachedChunks.Add(chunkCoordinate, chunk);
                if (!visitedChunks.Contains(chunkCoordinate))
                {
                    AddBuilding(chunkCoordinate.X, chunkCoordinate.Y);
                }
                visitedChunks.Add(chunkCoordinate);
            }

            // Returning the chunk
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
        /// <param name="isEnemy">Whether the <see cref="Entity"/> querying this information is a <see cref="Enemy"/></param>
        /// <returns>Whether a given <see cref="Entity"/> can walk/move onto this tile</returns>
        public bool IsTileObstructed(Vector2Int tileCoordinate, bool isEnemy = false)
        {
            // The tile thaty may be obstructed
            Tile tile = GetTileAt(tileCoordinate);

            // Returning the obstruct state depending whether player is inside/outside
            if (!IsInside || isEnemy)
            {
                return tile.OutsideObstructState;
            }
            else
            {
                return tile.InsideObstructState;
            }
        }

        /// <summary>
        /// Subprogram to add a <see cref="IBuilding"/> into the world
        /// </summary>
        /// <param name="chunkX">The x-coordinate of the <see cref="Chunk"/></param>
        /// <param name="chunkY">The y-coordinate of the <see cref="Chunk"/></param>
        private void AddBuilding(int chunkX, int chunkY)
        {
            int buildingChance = SharedData.RNG.Next(25);
            if (buildingChance == 1)
            {
                cachedBuildings.Add(new Shop(new Vector2Int(chunkX * Chunk.SIZE + Chunk.SIZE / 3, chunkY * Chunk.SIZE + Chunk.SIZE / 3)));
            }
            if (buildingChance == 2)
            {
                cachedBuildings.Add(new Safehouse(new Vector2Int(chunkX * Chunk.SIZE + Chunk.SIZE / 3, chunkY * Chunk.SIZE + Chunk.SIZE / 3)));
            }

            buildingChance = SharedData.RNG.Next(200);
            if (buildingChance == 3)
            {
                cachedBuildings.Add(new Dungeon(new Vector2Int(chunkX * Chunk.SIZE + Chunk.SIZE / 3, chunkY * Chunk.SIZE + Chunk.SIZE / 3)));
            }
        }

        /// <summary>
        /// Subprogram to add a projectile into this <see cref="World"/>
        /// </summary>
        /// <param name="projectile">The <see cref="Projectile"/> to be added</param>
        public void AddProjectile(Projectile projectile) => projectiles.Add(projectile);

        /// <summary>
        /// Subprogram to add an <see cref="Enemy"/> to this <see cref="World"/>
        /// </summary>
        /// <param name="enemy">The <see cref="Enemy"/> to be added</param>
        public void AddEnemy(Enemy enemy) => enemies.Add(enemy);

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
                retrievedItem = hitItems[hitItems.Count - 1].Item;
                liveItems.Remove(hitItems[hitItems.Count - 1]);
            }

            // Returning the retrieved item
            return retrievedItem;
        }

        /// <summary>
        /// Subprogram to add a <see cref="HashSet{Item}"/> of <see cref="Item"/> to this <see cref="World"/>
        /// </summary>
        /// <param name="enemyRectangle">The rectangle that the <see cref="Entity"/> is at</param>
        /// <param name="lootTable">The <see cref="HashSet{T}"/> containing the <see cref="Item"/>s</param>
        public void AddItems(Rectangle entityRectangle, HashSet<Item> lootTable)
        {
            // Adjusting entity rectangle
            entityRectangle.Y = entityRectangle.Y + (entityRectangle.Height - ItemSlot.SIZE) / 2;
            entityRectangle.X = entityRectangle.X + (entityRectangle.Width - ItemSlot.SIZE) / 2;
            entityRectangle.Width = ItemSlot.SIZE;
            entityRectangle.Height = ItemSlot.SIZE;

            // Adding items to the world
            foreach (Item item in lootTable)
            {
                liveItems.Add(new LiveItem(item, entityRectangle));
            }
        }

        /// <summary>
        /// Subprogram to inflict melee damage from a <see cref="Player"/> into the <see cref="World"/>
        /// </summary>
        /// <param name="weaponHitBox">The <see cref="Weapon"/>'s hit box</param>
        /// <param name="damageAmount">The damage amount of the <see cref="Weapon"/></param>
        /// <param name="direction">The direction of the <see cref="Player"/></param>
        public void InflictMeleeDamage(Rectangle weaponHitBox, int damageAmount, Direction direction)
        {
            // Determine the enemies who were hit
            List<Enemy> enemiesHit = new List<Enemy>();
            if (IsInside)
            {
                enemiesHit = collisionTree.GetCollisions(weaponHitBox, DungeonEnemies);
            }
            else
            {
                enemiesHit = collisionTree.GetCollisions(weaponHitBox, enemies);
            }

            // Hitting all applicable enemies and resetting their path finding
            for (short i = 0; i < enemiesHit.Count; ++i)
            {
                enemiesHit[i].Health -= damageAmount;
                enemiesHit[i].FindPathToPlayer();
            }
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

        /// <summary>
        /// Subprogram to deserialize a <see cref="World"/>
        /// </summary>
        /// <param name="serializedData">A <see cref="string"/> representing a <see cref="World"/>'s data</param>
        /// <returns>The deserialized <see cref="World"/></returns>
        public static World Deserialize(string serializedData) => JsonConvert.DeserializeObject<World>(serializedData);
    }
}
