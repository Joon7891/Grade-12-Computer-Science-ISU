using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class TerrainGenerator
    {
        [JsonProperty]
        private FastNoise NoiseEngine { get; }

        /// <summary>
        /// The seed for this <see cref="TerrainGenerator"/>
        /// </summary>
        [JsonProperty]
        public int Seed { get; }
        private const long PRIME_SEED = 4294967295;

        // HashSet to hold tile noise maps for all tile types
        private static readonly List<TileNoiseMap> tileNoiseMaps = new List<TileNoiseMap>()
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
            NoiseEngine = new FastNoise();
            NoiseEngine.SetFractalOctaves(12);
            NoiseEngine.SetFractalLacunarity(2);
            Seed = (int)seed;
            NoiseEngine.SetSeed(Seed);
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
                    tiles[i, j] = new Tile(FloatToTileType(1.0f + NoiseEngine.GetPerlinFractal(worldPosition.X, worldPosition.Y)), worldPosition);
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
            foreach (TileNoiseMap tileNoiseMap in tileNoiseMaps)
            {
                if (tileNoiseMap.NoiseInterval.Contains(noiseHeight))
                {
                    return tileNoiseMap.Type;
                }
            }

            // Returning empty tile type; never actually reaches here but C# doesn't know that
            return TileType.Empty;
        }
    }
}
