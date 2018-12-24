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
            new TileHeightMap(0.01f, TileType.DeepWater),
            new TileHeightMap(0.05f, TileType.Water),
            new TileHeightMap(0.10f, TileType.WetSand),
            new TileHeightMap(0.15f, TileType.Sand),
            new TileHeightMap(0.25f, TileType.Mud),
            new TileHeightMap(0.30f, TileType.DryGrass),
            new TileHeightMap(0.35f, TileType.Grass),
            new TileHeightMap(0.40f, TileType.ForestGrass),
            new TileHeightMap(0.45f, TileType.Stone),
            new TileHeightMap(0.50f, TileType.Snow),
            new TileHeightMap(0.55f, TileType.IcySnow),
            new TileHeightMap(0.60f, TileType.Ice),
        }; // TO DO: Adjust values - note: must be ordered


        /// <summary>
        /// Constructor for <see cref="TerrainGenerator"/> object
        /// </summary>
        public TerrainGenerator()
        {
            // Setting up noise engine and reseeding
            noiseEngine = new FastNoise();
            noiseEngine.SetFractalOctaves(6);
            noiseEngine.SetFractalLacunarity(2);
            Reseed();
        }

        /// <summary>
        /// Subprogram to reseed the <see cref="TerrainGenerator"/> "randomly"
        /// </summary>
        public void Reseed() 
        {
            // Reseeding the noise engine with an arbitrary number
            noiseEngine.SetSeed((int)(DateTime.UtcNow.Ticks * PRIME_SEED) & int.MaxValue);
        }

        /// <summary>
        /// Subprogram to reseed the <see cref="TerrainGenerator"/>
        /// </summary>
        /// <param name="seed">The new seed</param>
        public void Reseed(int seed)
        {
            // Reseeding the noise engine
            noiseEngine.SetSeed(seed);
        }

        //public void Generate(WorldData worldData)
        //{
        //    // Float to temporarily hold the noise value
        //    float noiseValue;

        //    for (int i = 0; i < worldData.Width; ++i)
        //    {
        //        for (int j = 0; j < worldData.Height; ++j)
        //        {
        //            noiseValue = noiseEngine.GetPerlinFractal(i, j);

        //            foreach (TileHeightMap tileHeightMap in tileHeightMaps)
        //            {
        //                if (tileHeightMap.MaxHeight < noiseValue)
        //                {
        //                    worldData.Tiles[i, j] = tileHeightMap.Type;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
