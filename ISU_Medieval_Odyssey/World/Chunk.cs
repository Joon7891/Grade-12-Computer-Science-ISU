// Author: Joon Song, Steven Ung
// File Name: Chunk.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold Chunk object - used to optimize graphics rendering

using System;
using System.Collections.Specialized;
using ISU_Medieval_Odyssey.Data_Structures;
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
        /// The amount of tiles that a signal chunk contains
        /// </summary>
        public const int SIZE = 32;

        /// <summary>
        /// Gets or sets a tile
        /// </summary>
        /// <param name="x">The x-coordinate of the tile</param>
        /// <param name="y">The y-coordinate of the tile</param>
        /// <returns>The tile at a given cartesian coordinate</returns>
        public Tile this[int x, int y]
        {
            get => GetTileAt(x, y);
            set => SetTileAt(x, y, value);
        }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in chunk-space.
        /// </summary>
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in world-space.
        /// </summary>
        public Vector2Int WorldPosition => Position * SIZE;

        /// <summary>
        /// Indicates whether the chunk is loaded.
        /// </summary>
        public bool Loaded { get; set; }

        private Tile[,] tiles;

        public Chunk()
        {
            Generate();
        }

        private void Generate()
        {
            tiles = new Tile[SIZE, SIZE];
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    tiles[x, y] = new Tile(TileType.Empty, Vector2Int.Zero, this);
                }
            }
        }

        public Tile GetTileAt(int x, int y)
        {
            if (x < 0 || x >= SIZE || y < 0 || y >= SIZE) return null;
            return tiles?[x, y];
        }

        /// <summary>
        /// Setter for Tile
        /// </summary>
        /// <param name="x">The x-coordinate of the tile</param>
        /// <param name="y">The y-coordinate of the tile</param>
        /// <param name="value">The value of the assignment</param>
        public void SetTileAt(int x, int y, Tile value)
        {
            // Assigning value of coordinatesa are in range and assignment is not null
            if (0 <= x && x < SIZE && 0 <= y && y < SIZE && value != null)
            {
                tiles[x, y] = value;
            }
        }

        public void Load(WorldData worldData)
        {
            if (Loaded) return;

            Loaded = true;

            Generate();
            int startX = Position.X * SIZE;
            int startY = Position.Y * SIZE;

            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    tiles[x, y].Position.X = x;
                    tiles[x, y].Position.Y = y;
                    tiles[x, y].Type = worldData.Tiles[startX + x, startY + y];
                }
            }

            World.Current.OnChunkLoaded(new ChunkEventArgs(this));
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
        }

        public void Unload()
        {
            if (!Loaded) return;
            Loaded = false;
            World.Current.OnChunkUnloaded(new ChunkEventArgs(this));
        }
    }
}
