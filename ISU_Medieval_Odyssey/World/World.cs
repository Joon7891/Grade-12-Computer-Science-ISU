using System;
using System.Collections.Generic;
using System.Linq;
using ISU_Medieval_Odyssey.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class World
    {
        private struct SpatialChunk
        {
            public Vector2Int Position { get; }
            public Chunk Data { get; }

            public SpatialChunk(Vector2Int position, Chunk data)
            {
                Position = position;
                Data = data;
            }
        }

        /// <summary>
        /// Static instance of World representing the current World
        /// </summary>
        public static World Current { get; private set; }

        /// <summary>
        /// The width of the world
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of the world
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The width of the world in tiles
        /// </summary>
        public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }

        public event TileChangedEventHandler TileChanged;
        public void OnTileChanged(TileEventArgs args) { TileChanged?.Invoke(this, args); }

        public event ChunkLoadedEventHandler ChunkLoaded;
        public void OnChunkLoaded(ChunkEventArgs args) { ChunkLoaded?.Invoke(this, args); }

        public event ChunkLoadedEventHandler ChunkUnloaded;
        public void OnChunkUnloaded(ChunkEventArgs args) { ChunkUnloaded?.Invoke(this, args); }

        private Chunk[,] chunks;
        private readonly HashSet<SpatialChunk> loadedChunks;
        private readonly Queue<SpatialChunk> loadChunkQueue;
        private readonly Queue<SpatialChunk> unloadChunkQueue;
        private readonly ObjectPool<Chunk> chunkPool;

        private readonly List<IWorldGenerator> worldGeneratorPasses;
        private WorldData worldData;

        private readonly Dictionary<Tile, Texture2D> tiles;

        public World()
        {
            Current = this;

            loadedChunks = new HashSet<SpatialChunk>();
            loadChunkQueue = new Queue<SpatialChunk>();
            unloadChunkQueue = new Queue<SpatialChunk>();

            worldGeneratorPasses = new List<IWorldGenerator>();
            tiles = new Dictionary<Tile, Texture2D>();

            chunkPool = new ObjectPool<Chunk>();
            ChunkLoaded += OnChunkLoaded;
            ChunkUnloaded += OnChunkUnloaded;
            TileChanged += OnTileChanged;
        }

        private void OnChunkLoaded(object sender, ChunkEventArgs args)
        {
            for (int x = 0; x < Chunk.SIZE; x++)
            {
                for (int y = 0; y < Chunk.SIZE; y++)
                {
                    Tile tile = args.Chunk[x, y];
                    if (tile == null) continue;

                    tiles[tile] = null;
                    OnTileChanged(this, new TileEventArgs(tile));
                }
            }
        }

        private void OnChunkUnloaded(object sender, ChunkEventArgs args)
        {
            for (int x = 0; x < Chunk.SIZE; x++)
            {
                for (int y = 0; y < Chunk.SIZE; y++)
                {
                    Tile tile = args.Chunk[x, y];
                    if (tile == null || !tiles.ContainsKey(tile)) continue;
                    tiles.Remove(tile);
                }
            }
        }

        private void OnTileChanged(object sender, TileEventArgs args)
        {
            if (!tiles.ContainsKey(args.Tile)) return;
            switch (args.Tile.Type)
            {
                case TileType.Empty:
                    tiles[args.Tile] = null;
                    break;
                default:
                    string tileTypeName = Enum.GetName(args.Tile.Type.GetType(), args.Tile.Type);
                    tiles[args.Tile] = Main.Instance.Content.Load<Texture2D>($"Images/Sprites/Tiles/Tile_{tileTypeName}");
                    break;
            }
        }

        public void Initialize(int width, int height)
        {
            Width = width;
            Height = height;

            WidthInTiles = width * Chunk.SIZE;
            HeightInTiles = height * Chunk.SIZE;

            chunks = new Chunk[Width, Height];
            worldData = new WorldData(WidthInTiles, HeightInTiles);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: Main.Instance.Camera.ViewMatrix, samplerState: SamplerState.PointClamp);

            foreach (KeyValuePair<Tile, Texture2D> pair in tiles)
            {
                if (pair.Key == null || pair.Value == null) continue;

                float sx = Tile.Size / (float)pair.Value.Width;
                float sy = Tile.Size / (float)pair.Value.Height;

                //Vector2Int isometricCoordinate = GetIsometricProjection(pair.Key.WorldPosition);
                spriteBatch.Draw(pair.Value, pair.Key.WorldPosition.ToVector2() * Tile.Size, null, Color.White, 0, 
                    new Vector2(pair.Value.Width / 2.0f, pair.Value.Height / 2.0f), new Vector2(sx, sy), SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }

        public void Generate()
        {
            //CreateChunks();
            foreach (IWorldGenerator pass in worldGeneratorPasses)
            {
                pass.Generate(worldData);
            }
        }

        public void Generate(int seed)
        {
            foreach (IWorldGenerator pass in worldGeneratorPasses)
            {
                pass.Reseed(seed);
            }

            Generate();
        }

        public void AddGenerator(IWorldGenerator worldGenerator)
        {
            worldGeneratorPasses.Add(worldGenerator);
        }

        public Chunk GetChunkContaining(int x, int y)
        {
            if (x < 0 || x >= WidthInTiles || y < 0 || y >= HeightInTiles) return null;

            int chunkX = (int)Math.Floor(x / (double)Chunk.SIZE);
            int chunkY = (int)Math.Floor(y / (double)Chunk.SIZE);
            if (chunkX < 0 || chunkX >= Width || chunkY < 0 || chunkY >= Height) return null;

            return GetChunkAt(chunkX, chunkY);
        }

        public Tile GetTileAt(int x, int y)
        {
            if (x < 0 || x >= WidthInTiles || y < 0 || y >= HeightInTiles) return null;
            Chunk chunk = GetChunkContaining(x, y);

            // Tile (x) position relative to the chunk
            int tileX = x % Chunk.SIZE;
            // Tile (y) position relative to the chunk
            int tileY = y % Chunk.SIZE;

            if (tileX < 0 || tileX >= Chunk.SIZE || tileY < 0 || tileY >= Chunk.SIZE) return null;
            return chunk.GetTileAt(tileX, tileY);
        }

        public Chunk GetChunkAt(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return null;
            if (chunks[x, y] != null) return chunks[x, y];

            Chunk chunk = chunkPool.Get();
            chunk.SetPosition(new Vector2Int(x, y));
            chunks[x, y] = chunk;

            return chunks[x, y];
        }

        public Tile GetTileFromWorldCoordinate(Vector2 worldCoordinate)
        {
            int x = (int) Math.Floor(worldCoordinate.X / Tile.Size + 0.5f);
            int y = (int) Math.Floor(worldCoordinate.Y / Tile.Size + 0.5f);
            return GetTileAt(x, y);
        }

        public void Update()
        {
            int screenWidth = Main.Instance.GraphicsDevice.Viewport.Width;
            int screenHeight = Main.Instance.GraphicsDevice.Viewport.Height;

            float zoom = Main.Instance.Camera.OrthographicSize;
            int viewportWidth = (int)Math.Ceiling((double)screenWidth / (Chunk.SIZE * Tile.Size * 2 * zoom)) + 2;
            int viewportHeight = (int)Math.Ceiling((double)screenHeight / (Chunk.SIZE * Tile.Size * 2 * zoom)) + 2;

            Vector2 cameraPosition = Main.Instance.Camera.Position;
            Vector2 tileAtCameraPosition = new Vector2(cameraPosition.X / Tile.Size, cameraPosition.Y / Tile.Size);
            Chunk chunkContaining = GetChunkContaining((int)Math.Round(tileAtCameraPosition.X), (int)Math.Round(tileAtCameraPosition.Y));

            if (chunkContaining == null) return;
            int viewpointX = chunkContaining.Position.X;
            int viewpointY = chunkContaining.Position.Y;

            HashSet<SpatialChunk> chunksToLoad = new HashSet<SpatialChunk>();
            for (int x = -viewportWidth; x <= viewportWidth; x++)
            {
                for (int y = -viewportHeight; y <= viewportHeight; y++)
                {
                    int chunkX = viewpointX + x;
                    int chunkY = viewpointY + y;
                    if (chunkX >= 0 && chunkX < Width && chunkY >= 0 && chunkY < Height)
                    {
                        chunksToLoad.Add(new SpatialChunk(new Vector2Int(chunkX, chunkY), GetChunkAt(chunkX, chunkY)));
                    }
                }
            }

            IEnumerable<SpatialChunk> chunksToUnload = loadedChunks.Except(chunksToLoad);
            foreach (SpatialChunk loadChunk in chunksToUnload.ToList())
            {
                if (!unloadChunkQueue.Contains(loadChunk) && loadChunk.Data.Loaded)
                {
                    unloadChunkQueue.Enqueue(loadChunk);
                }
            }

            foreach (SpatialChunk loadChunk in chunksToLoad)
            {
                if (!loadChunkQueue.Contains(loadChunk) && !loadChunk.Data.Loaded)
                {
                    loadChunkQueue.Enqueue(loadChunk);
                }
            }

            if (unloadChunkQueue.Count > 0)
            {
                SpatialChunk chunk = unloadChunkQueue.Dequeue();
                chunk.Data.Unload();
                chunkPool.Add(chunk.Data);
                loadedChunks.Remove(chunk);
            }

            if (loadChunkQueue.Count > 0)
            {
                SpatialChunk chunk = loadChunkQueue.Dequeue();
                chunk.Data.SetPosition(chunk.Position);
                chunk.Data.Load(worldData);
                loadedChunks.Add(chunk);
            }
        }

        private Vector2Int GetIsometricProjection(Vector2Int coordinate)
        {
            int px = (coordinate.X - coordinate.Y) * (Tile.Size / 2);
            int py = (coordinate.X + coordinate.Y) * (Tile.Size / 2);
            return new Vector2Int(px, py);
        }
    }
}
