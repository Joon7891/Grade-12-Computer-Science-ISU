using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class TerrainGenerator
    {
        // Noise engine and arbitrary large prime for reseeding
        private readonly FastNoise noiseEngine;
        private const long PRIME_SEED = 4294967295;

        // HashSet to hold tile height maps for all tile types
        private static readonly HashSet<TileHeightMap> tileHeightMaps = new HashSet<TileHeightMap>()
        {
            new TileHeightMap(0.00f, TileType.DeepWater),
            new TileHeightMap(0.10f, TileType.Water),
            new TileHeightMap(0.20f, TileType.WetSand),
            new TileHeightMap(0.40f, TileType.Sand),
            new TileHeightMap(0.50f, TileType.Dirt),
            new TileHeightMap(0.60f, TileType.DryGrass),
            new TileHeightMap(0.70f, TileType.Grass),
            new TileHeightMap(0.80f, TileType.ForestGrass),
            new TileHeightMap(0.90f, TileType.Stone),
            new TileHeightMap(1.00f, TileType.Snow),
            new TileHeightMap(1.25f, TileType.IcySnow),
            new TileHeightMap(1.50f, TileType.Ice),
        };

        /// <summary>
        /// Constructor for <see cref="TerrainGenerator"/> object
        /// </summary>
        public TerrainGenerator() : this((int)(DateTime.UtcNow.Ticks * PRIME_SEED) % int.MaxValue)
        {
            // Nothing to add as it calls other constructor
        }

        /// <summary>
        /// Constructor for <see cref="TerrainGenerator"/> object
        /// </summary>
        /// <param name="seed">The seed of the <see cref="TerrainGenerator"/></param>
        public TerrainGenerator(int seed)
        {
            // Setting up noise engine and setting seed
            noiseEngine = new FastNoise();
            noiseEngine.SetFractalOctaves(6);
            noiseEngine.SetFractalLacunarity(2);
            noiseEngine.SetSeed(seed);
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
            // Initially setting tile type as empty
            TileType tileType = TileType.Empty;

            // Looping through height maps to determine approprate tile type
            foreach (TileHeightMap heightMap in tileHeightMaps)
            {
                if (heightMap.MinHeight <= noiseHeight)
                {
                    tileType = heightMap.Type;
                }
            }

            // Returning tile type
            return tileType;
        }
    }
}
