using System;

namespace ISU_Medieval_Odyssey
{
    public class TerrainWorldGenerator : IWorldGenerator
    {
        private Random random;

        public TerrainWorldGenerator()
        {
            Reseed();
        }

        public void Reseed()
        {
            random = new Random();
        }

        public void Reseed(int seed)
        {
            random = new Random(seed);
        }

        public void Generate(WorldData data)
        {
            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    data.Tiles[x, y] = TileType.Grass;
                }
            }
        }
    }
}
