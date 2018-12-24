// Author: Joon Song, Steven Ung
// File Name: Tile.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold Tile object

using System;
using ISU_Medieval_Odyssey.Data_Structures;

namespace ISU_Medieval_Odyssey
{
    public sealed class Tile
    {
        /// <summary>
        /// The height of the <see cref="Tile"/>, in pixels
        /// </summary>
        public const int HEIGHT = 16;

        /// <summary>
        /// The width of the <see cref="Tile"/>, in pixels
        /// </summary>
        public const int WIDTH = 2 * HEIGHT;

        /// <summary>
        /// The <see cref="Chunk"/> that this tile belongs to
        /// </summary>
        public Chunk Chunk { get; }

        /// <summary>
        /// The position of the <see cref="Tile"/> relative to its <see cref="Chunk"/>
        /// </summary>
        public Vector2Int RelativePosition { get; set; }

        /// <summary>
        /// The world position of this <see cref="Tile"/>
        /// </summary>
        public Vector2Int WorldPosition => RelativePosition + Chunk.WorldPosition;

        /// <summary>
        /// The type of the <see cref="Tile"/>
        /// </summary>
        public TileType Type { get; set; }

        /// <summary>
        /// Constructor for <see cref="Tile"/> object
        /// </summary>
        /// <param name="type">The type of the tile</param>
        /// <param name="chunk">The chunk containing the tile</param>
        /// <param name="relativePosition">The position of the tile, relative to its chunk</param>
        public Tile(TileType type, Chunk chunk, Vector2Int relativePosition)
        {
            // Setting constructor parameters to object properties
            Type = type;
            Chunk = chunk;
            RelativePosition = relativePosition;
        }
    }
}
