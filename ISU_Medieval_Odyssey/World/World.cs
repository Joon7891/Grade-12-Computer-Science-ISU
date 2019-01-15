// Author: Joon Song
// File Name: World.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/24/2018
// Description: Class to hold World object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    public sealed class World
    {
        public static World Instance { get; set; }

        private TerrainGenerator terrainGenerator;
        private const int LOADED_CHUNK_COUNT = 5;
        private Chunk[,] loadedChunks = new Chunk[LOADED_CHUNK_COUNT, LOADED_CHUNK_COUNT];

        private List<Projectile> projectiles = new List<Projectile>();

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
            terrainGenerator = new TerrainGenerator();

            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    chunkPosition = new Vector2Int(i, j);
                    loadedChunks[i, j] = new Chunk(chunkPosition, terrainGenerator);
                }
            }

            Instance = this;
        }

        public World(int seed)
        {
            Vector2Int chunkPosition;
            terrainGenerator = new TerrainGenerator(seed);

            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    chunkPosition = new Vector2Int(i, j);
                    loadedChunks[i, j] = new Chunk(chunkPosition, terrainGenerator);
                }
            }

            Instance = this;
        }

        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
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

            // Updating the projectiles in the world
            for (int i = 0; i < projectiles.Count; ++i)
            {
                projectiles[i].Update(gameTime);

                // Removing projectiles if they are not active
                if (!projectiles[i].Active)
                {
                    projectiles.RemoveAt(i);
                    --i;
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

            // Drawing projectiles
            for (int i = 0; i < projectiles.Count; ++i)
            {
                projectiles[i].Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public Vector2 GetTile(Vector2 coordLoc)
        {
            return new Vector2(coordLoc.X / Tile.HORIZONTAL_SIZE, coordLoc.Y / Tile.VERTICAL_SIZE);
        }

        public Vector2 GetUnroundedCoord(Vector2 tileLoc)
        {
            return new Vector2(tileLoc.X * Tile.HORIZONTAL_SIZE, tileLoc.Y * Tile.VERTICAL_SIZE);
        }

        public Vector2Int GetCoord(Vector2 tileLoc)
        {
            return new Vector2Int(Convert.ToInt32(tileLoc.X * Tile.HORIZONTAL_SIZE),
                                  Convert.ToInt32(tileLoc.Y * Tile.VERTICAL_SIZE));
        }
    }
}
