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
using Simplex;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Chunk
    {
        private const float SCALE = 0.5f;
        private const int CHUNK_SIZE = 16;
        private Tile[,] tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];

        public static Chunk GenerateChunk(int x, int y)
        {
            float tileData;
            Tile[,] tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];

            for (int i = 0; i < CHUNK_SIZE; ++i)
            {
                for (int j = 0; j < CHUNK_SIZE; ++j)
                {
                    tileData = Noise.CalcPixel2D(x * CHUNK_SIZE + i, y * CHUNK_SIZE + j, SCALE);
                    tiles[i, j] = new Tile(tileData);
                }
            }

            return new Chunk(tiles);
        }

        public Chunk(Tile[,] tiles)
        {
            this.tiles = tiles;
        }


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
