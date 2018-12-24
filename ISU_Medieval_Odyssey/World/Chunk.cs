// Author: Joon Song, Steven Ung
// File Name: Chunk.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/17/2018
// Modified Date: 12/29/2018
// Description: Class to hold Chunk object - used to optimize graphics rendering

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Chunk
    {
        /// <summary>
        /// The horizontal/vertical size of the chunk - a chunk will contain 16 x 16 tiles
        /// </summary>
        public const byte SIZE = 16;

        /// <summary>
        /// A 2D array containing the tiles in the chunk
        /// </summary>
        /// <param name="x">The x-coordinate of the tile (relative to the chunk)</param>
        /// <param name="y">The y-coordinate of the tile (relative to the chunk)</param>
        /// <returns>The tile at the given cartesian coordinate</returns>
        public Tile this[int x, int y]
        {
            // Getter for tile
            get => tiles[x, y];

            // Setter for tile
            set => tiles[x, y] = value;
        }
        private Tile[,] tiles;

        /// <summary>
        /// The position of this <see cref="Chunk"/> in chunk-space
        /// </summary>
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// The position of this <see cref="Chunk"/> in world/tile-space
        /// </summary>
        public Vector2Int WorldPosition { get; private set; }

        /// <summary>
        /// Whether the chunk is loaded or not
        /// </summary>
        public bool Loaded { get; set; }

        /// <summary>
        /// Constructor for <see cref="Chunk"/> object
        /// </summary>
        public Chunk()
        {
            tiles = new Tile[SIZE, SIZE];
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < SIZE; ++i)
            {
                for (int j = 0; j < SIZE; ++j)
                {
                    tiles[i, j].Draw(spriteBatch);
                }
            }
        }

    }
}
