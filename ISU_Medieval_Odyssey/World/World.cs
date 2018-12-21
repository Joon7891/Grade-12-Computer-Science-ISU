﻿using System;
using System.Collections.Generic;
using System.Linq;
using ISU_Medieval_Odyssey.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class World
    {
        public static World Current { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }

        public event TileChangedEventHandler TileChanged;
        public void OnTileChanged(TileEventArgs args) { TileChanged?.Invoke(this, args); }

        public event ChunkLoadedEventHandler ChunkLoaded;
        public void OnChunkLoaded(ChunkEventArgs args) { ChunkLoaded?.Invoke(this, args); }

        public event ChunkLoadedEventHandler ChunkUnloaded;
        public void OnChunkUnloaded(ChunkEventArgs args) { ChunkUnloaded?.Invoke(this, args); }

        private Chunk[,] chunks;
        private readonly HashSet<Chunk> loadedChunks;
        private readonly Queue<Chunk> loadChunkQueue;
        private readonly Queue<Chunk> unloadChunkQueue;

        private readonly List<IWorldGenerator> worldGeneratorPasses;
        private WorldData worldData;

        private readonly Dictionary<Tile, Texture2D> tiles;

        public World()
        {
            Current = this;

            loadedChunks = new HashSet<Chunk>();
            loadChunkQueue = new Queue<Chunk>();
            unloadChunkQueue = new Queue<Chunk>();

            worldGeneratorPasses = new List<IWorldGenerator>();
            tiles = new Dictionary<Tile, Texture2D>();

            ChunkLoaded += OnChunkLoaded;
            ChunkUnloaded += OnChunkUnloaded;
            TileChanged += OnTileChanged;
        }

        private void OnChunkLoaded(object sender, ChunkEventArgs args)
        {
            for (int x = 0; x < Chunk.Size; x++)
            {
                for (int y = 0; y < Chunk.Size; y++)
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
            for (int x = 0; x < Chunk.Size; x++)
            {
                for (int y = 0; y < Chunk.Size; y++)
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
                    tiles[args.Tile] = Main.Context.Content.Load<Texture2D>($"Images/Sprites/Tiles/Tile_{tileTypeName}");
                    break;
            }
        }

        public void Initialize(int width, int height)
        {
            Width = width;
            Height = height;

            WidthInTiles = width * Chunk.Size;
            HeightInTiles = height * Chunk.Size;

            chunks = new Chunk[Width, Height];
            worldData = new WorldData(WidthInTiles, HeightInTiles);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: Main.Context.Camera.ViewMatrix, samplerState: SamplerState.PointClamp);

            foreach (KeyValuePair<Tile, Texture2D> pair in tiles)
            {
                if (pair.Key == null || pair.Value == null) continue;

                float sx = Tile.Size / (float)pair.Value.Width;
                float sy = Tile.Size / (float)pair.Value.Height;

                spriteBatch.Draw(pair.Value, pair.Key.WorldPosition.ToVector2() * Tile.Size, null, Color.White, 0,
                    new Vector2(pair.Value.Width / 2.0f, pair.Value.Height / 2.0f), new Vector2(sx, sy), SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }

        public void Generate()
        {
            CreateChunks();
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

        public void CreateChunks()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    chunks[x, y] = new Chunk(new Vector2Int(x, y), new Vector2Int(x, y) * Chunk.Size);
                }
            }
        }

        public void AddGenerator(IWorldGenerator worldGenerator)
        {
            worldGeneratorPasses.Add(worldGenerator);
        }

        public Chunk GetChunkContaining(int x, int y)
        {
            if (x < 0 || x >= WidthInTiles || y < 0 || y >= HeightInTiles) return null;

            int chunkX = (int)Math.Floor(x / (double)Chunk.Size);
            int chunkY = (int)Math.Floor(y / (double)Chunk.Size);
            if (chunkX < 0 || chunkX >= Width || chunkY < 0 || chunkY >= Height) return null;

            return chunks[chunkX, chunkY];
        }

        public Tile GetTileAt(int x, int y)
        {
            if (x < 0 || x >= WidthInTiles || y < 0 || y >= HeightInTiles) return null;
            Chunk chunk = GetChunkContaining(x, y);

            // Tile (x) position relative to the chunk
            int tileX = x % Chunk.Size;
            // Tile (y) position relative to the chunk
            int tileY = y % Chunk.Size;

            if (tileX < 0 || tileX >= Chunk.Size || tileY < 0 || tileY >= Chunk.Size) return null;
            return chunk.GetTileAt(tileX, tileY);
        }

        public Chunk GetChunkAt(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return null;
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
            int screenWidth = Main.Context.GraphicsDevice.Viewport.Width;
            int screenHeight = Main.Context.GraphicsDevice.Viewport.Height;

            float zoom = Main.Context.Camera.OrthographicSize;
            int viewportWidth = (int)Math.Ceiling((double)screenWidth / (Chunk.Size * Tile.Size * 2 * zoom)) + 2;
            int viewportHeight = (int)Math.Ceiling((double)screenHeight / (Chunk.Size * Tile.Size * 2 * zoom)) + 2;

            Vector2 cameraPosition = Main.Context.Camera.Position;
            Vector2 tileAtCameraPosition = new Vector2(cameraPosition.X / Tile.Size, cameraPosition.Y / Tile.Size);
            Chunk chunkContaining = GetChunkContaining((int)Math.Round(tileAtCameraPosition.X), (int)Math.Round(tileAtCameraPosition.Y));

            if (chunkContaining == null) return;
            int viewpointX = (int)chunkContaining.Position.X;
            int viewpointY = (int)chunkContaining.Position.Y;

            HashSet<Chunk> chunksToLoad = new HashSet<Chunk>();

            for (int x = -viewportWidth; x <= viewportWidth; x++)
            {
                for (int y = -viewportHeight; y <= viewportHeight; y++)
                {
                    int chunkX = viewpointX + x;
                    int chunkY = viewpointY + y;
                    if (chunkX >= 0 && chunkX < Width && chunkY >= 0 && chunkY < Height)
                    {
                        chunksToLoad.Add(chunks[chunkX, chunkY]);
                    }
                }
            }

            IEnumerable<Chunk> chunksToUnload = loadedChunks.Except(chunksToLoad);
            foreach (Chunk chunk in chunksToUnload.ToList())
            {
                if (!unloadChunkQueue.Contains(chunk) && chunk.Loaded)
                {
                    unloadChunkQueue.Enqueue(chunk);
                }
            }

            foreach (Chunk chunk in chunksToLoad)
            {
                if (!loadChunkQueue.Contains(chunk) && !chunk.Loaded)
                {
                    loadChunkQueue.Enqueue(chunk);
                }
            }

            if (unloadChunkQueue.Count > 0)
            {
                Chunk chunk = unloadChunkQueue.Dequeue();
                chunk.Unload();

                loadedChunks.Remove(chunk);
            }

            if (loadChunkQueue.Count > 0)
            {
                Chunk chunk = loadChunkQueue.Dequeue();
                chunk.Load(worldData);

                loadedChunks.Add(chunk);
            }
        }
    }
}
