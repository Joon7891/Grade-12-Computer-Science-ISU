// Author: Joon Song, Steven Ung
// File Name: Chunk.cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2018
// Description: Class to hold Chunk object - used to optimize graphics rendering

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Chunk
    {
        private const int CHUNK_SIZE = 16;
        private Tile[,] tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];

        /// <summary>
        /// Subprogram to draw a given chunk
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing tiles in chunk
            for (int i = 0; i < CHUNK_SIZE; ++i)
            {
                for (int j = 0; j < CHUNK_SIZE; ++j)
                {
                    tiles[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
