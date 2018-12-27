using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class Shop : Building
    {
        private Tile[,] groundTiles = new Tile[8, 8];

        public Shop(Vector2Int chunkPosition)
        {
            for (int i = 0; i < groundTiles.GetLength(0); ++i)
            {
                for (int j = 0; j < groundTiles.GetLength(1); ++j)
                {
                    groundTiles[i, j] = new Tile(
                        (i + j) % 2 == 0 ? TileType.WoodFloorHorizontal : TileType.WoodFloorVertical, 
                        new Vector2Int(chunkPosition.X * Chunk.SIZE + i + 4, chunkPosition.Y * Chunk.SIZE + j + 4));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < groundTiles.GetLength(0); ++i)
            {
                for (int j = 0; j < groundTiles.GetLength(1); ++j)
                {
                    groundTiles[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}