namespace ISU_Medieval_Odyssey
{
    public interface IWorldGenerator
    {
        void Reseed();
        void Reseed(int seed);
        void Generate(WorldData data);
    }
}
