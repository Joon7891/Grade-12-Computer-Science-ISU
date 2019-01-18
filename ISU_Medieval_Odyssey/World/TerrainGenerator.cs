// Author: Joon Song
// File Name: TerrainGenerator.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold TerrainGeneator object

using System;
using Newtonsoft.Json;

namespace ISU_Medieval_Odyssey
{
    public sealed class TerrainGenerator
    {
        /// <summary>
        /// The seed for this <see cref="TerrainGenerator"/>
        /// </summary>
        [JsonProperty]
        public int Seed { get; }
        private const long PRIME_SEED = 4294967295;

        // The noise engine to generate terrain
        [JsonProperty]
        private FastNoise noiseEngine;

        // HashSet to hold tile noise maps for all tile types
        private static readonly TileNoiseMap[] tileNoiseMaps = 
        {
            new TileNoiseMap(0.00f, 0.10f, TileType.DeepWater),
            new TileNoiseMap(0.10f, 0.50f, TileType.Water),
            new TileNoiseMap(0.50f, 0.65f, TileType.WetSand),
            new TileNoiseMap(0.65f, 0.75f, TileType.Sand),
            new TileNoiseMap(0.75f, 0.85f, TileType.Dirt),
            new TileNoiseMap(0.85f, 0.95f, TileType.DryGrass),
            new TileNoiseMap(0.95f, 1.10f, TileType.Grass),
            new TileNoiseMap(1.10f, 1.25f, TileType.ForestGrass),
            new TileNoiseMap(1.25f, 1.40f, TileType.Stone),
            new TileNoiseMap(1.40f, 1.60f, TileType.Snow),
            new TileNoiseMap(1.60f, 1.80f, TileType.IcySnow),
            new TileNoiseMap(1.80f, 2.00f, TileType.Ice),
        };

        /// <summary>
        /// Constructor for <see cref="TerrainGenerator"/> object
        /// </summary>
        /// <param name="seed">The seed of the <see cref="TerrainGenerator"/></param>
        public TerrainGenerator(int? seed = null)
        {
            // If seed was not provided, generate new seed
            if (seed == null)
            {
                seed = (int)(DateTime.UtcNow.Ticks * PRIME_SEED) % int.MaxValue;
            }

            // Setting up noise engine and setting seed
            noiseEngine = new FastNoise();
            noiseEngine.SetFractalOctaves(12);
            noiseEngine.SetFractalLacunarity(2);
            Seed = (int)seed;
            noiseEngine.SetSeed(Seed);
        }

        /// <summary>
        /// Subprogram to generate a <see cref="Chunk"/>'s <see cref="Tile"/>s
        /// </summary>
        /// <param name="chunkPosition">The position of the <see cref="Chunk"/></param>
        /// <returns>The <see cref="Chunk"/>'s <see cref="Tile"/>s</returns>
        public Tile[,] GenerateChunkTiles(Vector2Int chunkPosition)
        {
            // 2D array of tiles and the world position of the tile
            Tile[,] tiles = new Tile[Chunk.SIZE, Chunk.SIZE];
            Vector2Int worldPosition = Vector2Int.Zero;

            // Loop through each tile in the chunk and constructing tile
            for (int i = 0; i < Chunk.SIZE; ++i)
            {
                for (int j = 0; j < Chunk.SIZE; ++j)
                {
                    worldPosition.X = chunkPosition.X * Chunk.SIZE + i;
                    worldPosition.Y = chunkPosition.Y * Chunk.SIZE + j;
                    tiles[i, j] = new Tile(FloatToTileType(1.0f + noiseEngine.GetPerlinFractal(worldPosition.X, worldPosition.Y)), worldPosition);
                }
            }

            // Returning chunk tiles
            return tiles;
        }

        /// <summary>
        /// Subprogram to map a given noise height to the appropraite tile type
        /// </summary>
        /// <param name="noiseHeight">The height of the noise</param>
        /// <returns>The corresponding TileType</returns>
        private TileType FloatToTileType(float noiseHeight)
        {
            // Returning appropraite tile type
            for (byte i = 0; i < tileNoiseMaps.Length; ++i)
            {
                if (tileNoiseMaps[i].NoiseInterval.Contains(noiseHeight))
                {
                    return tileNoiseMaps[i].Type;
                }
            }

            // Returning empty tile type; never actually reaches here but C# doesn't know that
            return TileType.Empty;
        }
    }
}
