using System;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    public class TerrainWorldGenerator : IWorldGenerator
    {
        private const long PrimeSeed = 4294967295;

        private static readonly HashSet<Tuple<float, TileType>> TileTypeHeightMap = new HashSet<Tuple<float, TileType>>
        {
            new Tuple<float, TileType>(0.05f, TileType.Water),
            new Tuple<float, TileType>(0.055f, TileType.WetSand),
            new Tuple<float, TileType>(0.1f, TileType.Sand),
            new Tuple<float, TileType>(0.25f, TileType.Grass),
            new Tuple<float, TileType>(0.6f, TileType.ForestGrass),
            new Tuple<float, TileType>(0.7f, TileType.Stone),
            new Tuple<float, TileType>(1, TileType.Snow)
        };

        private readonly FastNoise noiseEngine;

        public TerrainWorldGenerator()
        {
            noiseEngine = new FastNoise();
            noiseEngine.SetFractalOctaves(6);
            noiseEngine.SetFractalLacunarity(2);

            Reseed();
        }

        public void Reseed()
        {
            // Seed the noise generator using the datetime ticks
            noiseEngine.SetSeed((int)(DateTime.UtcNow.Ticks * PrimeSeed) % int.MaxValue);
        }

        public void Reseed(int seed)
        {
            noiseEngine.SetSeed(seed);
        }

        public void Generate(WorldData data)
        {
            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    float noise = noiseEngine.GetPerlinFractal(x, y);
                    TileType type = TileType.Empty;
                    foreach (Tuple<float, TileType> pair in TileTypeHeightMap)
                    {
                        if (!(noise < pair.Item1)) continue;

                        type = pair.Item2;
                        break;
                    }
       
                    data.Tiles[x, y] = type;
                }
            }
        }
    }
}
