using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class World
    {
        private TerrainGenerator terrainGenerator = new TerrainGenerator();
        private Chunk[,] loadedChunks = new Chunk[5, 5];

        public World()
        {
            Vector2Int chunkPosition;

            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    chunkPosition = new Vector2Int(i, j);
                    loadedChunks[i, j] = new Chunk(chunkPosition, terrainGenerator);
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    loadedChunks[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
