﻿// Author: Joon Song, Steven Ung
// File Name: Tile.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold Tile object

using System;
using ISU_Medieval_Odyssey.Utility;

namespace ISU_Medieval_Odyssey
{
    public delegate void TileChangedEventHandler(object sender, TileEventArgs args);
    public class TileEventArgs : EventArgs
    {
        public Tile Tile { get; }
        public TileEventArgs(Tile tile)
        {
            Tile = tile;
        }
    }

    public sealed class Tile
    {
        public const int Size = 32;

        /// <summary>
        /// The position of this <see cref="Tile"/> in the <see cref="Chunk"/>.
        /// </summary>
        public Vector2Int Position { get; }

        /// <summary>
        /// The position of this <see cref="Tile"/> in world-space.
        /// </summary>
        public Vector2Int WorldPosition { get; }

        /// <summary>
        /// The <see cref="Chunk"/> that this tile belongs to.
        /// </summary>
        public Chunk Chunk { get; }

        private TileType type;
        public TileType Type
        {
            get => type;
            set
            {
                TileType oldTileType = type;
                type = value;

                if (oldTileType == type) return;
                World.Current.OnTileChanged(new TileEventArgs(this));
            }
        }

        public Tile(TileType type, Vector2Int position, Vector2Int worldPosition, Chunk chunk)
        {
            Type = type;
            WorldPosition = worldPosition;
            Position = position;
            Chunk = chunk;
        }
    }
}
