// Author: Joon Song, Steven Ung
// File Name: Chunk.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold Chunk object - used to optimize graphics rendering

using System;
using ISU_Medieval_Odyssey.Utility;
using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public delegate void ChunkLoadedEventHandler(object sender, ChunkEventArgs args);
    public class ChunkEventArgs : EventArgs
    {
        public Chunk Chunk { get; }
        public ChunkEventArgs(Chunk chunk)
        {
            Chunk = chunk;
        }
    }

    public sealed class Chunk
    {
        /// <summary>
        /// The amount of tiles that a signal chunk contains.
        /// </summary>
        public const int Size = 32;

        /// <summary>
        /// Gets or sets a tile.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <returns></returns>
        public Tile this[int x, int y]
        {
            get => GetTileAt(x, y);
            set => SetTileAt(x, y, value);
        }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in chunk-space.
        /// </summary>
        public Vector2Int Position { get; }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in world-space.
        /// </summary>
        public Vector2Int WorldPosition { get; }

        /// <summary>
        /// Indicates whether the chunk is loaded.
        /// </summary>
        public bool Loaded { get; set; }

        private Tile[,] tiles;

        public Chunk(Vector2Int position, Vector2Int worldPosition)
        {
            Position = position;
            WorldPosition = worldPosition;
            Generate();
        }

        private void Generate()
        {
            tiles = new Tile[Size, Size];
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    tiles[x, y] = new Tile(TileType.Empty, position, WorldPosition + position, this);
                }
            }
        }

        public Tile GetTileAt(int x, int y)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size) return null;
            return tiles?[x, y];
        }

        public void SetTileAt(int x, int y, Tile value)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size) return;
            if (value == null) return;

            tiles[x, y] = value;
        }

        public void Load(WorldData worldData)
        {
            if (Loaded) return;

            Loaded = true;

            Generate();
            int startX = (int)Position.X * Size;
            int startY = (int)Position.Y * Size;

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    tiles[x, y].Type = worldData.Tiles[startX + x, startY + y];
                }
            }

            World.Current.OnChunkLoaded(new ChunkEventArgs(this));
        }

        public void Unload()
        {
            if (!Loaded) return;

            Loaded = false;
            World.Current.OnChunkUnloaded(new ChunkEventArgs(this));

            tiles = null;
        }
    }
}
