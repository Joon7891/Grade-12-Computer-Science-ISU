﻿// Author: Joon Song
// File Name: World.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/24/2018
// Description: Class to hold World object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class World
    {
        private TerrainGenerator terrainGenerator = new TerrainGenerator();
        private const int LOADED_CHUNK_COUNT = 5;
        private Chunk[,] loadedChunks = new Chunk[LOADED_CHUNK_COUNT, LOADED_CHUNK_COUNT];

        public Tile this[int x, int y]
        {
            get
            {
                int tileX = x - loadedChunks[0, 0].Position.X * Chunk.SIZE;
                int tileY = y - loadedChunks[0, 0].Position.Y * Chunk.SIZE;
                return loadedChunks[tileX / Chunk.SIZE, tileY / Chunk.SIZE][tileX % Chunk.SIZE, tileY % Chunk.SIZE];
            }
        }

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

        public void Update(GameTime gameTime, Vector2Int currentPosition)
        {
            // Shifting chunk and loading chunks if needed, if current chunk is not centered
            if (currentPosition != loadedChunks[LOADED_CHUNK_COUNT / 2, LOADED_CHUNK_COUNT / 2].Position)
            {
                for (int i = 0; i < LOADED_CHUNK_COUNT; ++i)
                {
                    for (int j = 0; j < LOADED_CHUNK_COUNT; ++j)
                    {
                        Vector2Int newPos = currentPosition + new Vector2Int(i - 2, j - 2);
                        loadedChunks[i, j] = new Chunk(newPos, terrainGenerator);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(transformMatrix: camera.ViewMatrix, samplerState: SamplerState.PointClamp);

            for (byte i = 0; i < 5; ++i)
            {
                for (byte j = 0; j < 5; ++j)
                {
                    loadedChunks[i, j].Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }
    }
}
