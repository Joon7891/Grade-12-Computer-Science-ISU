// Author: Joon Song
// File Name: Dragon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold Dragon object

namespace ISU_Medieval_Odyssey
{
    public sealed class Dragon : SmartEnemy
    {
        private static new DirectionalSpriteSheet directionalSpriteSheet;
        private const int NUM_FRAMES = 4;
        private const int COUNTER_MAX = 5;
        private const int WIDTH = 100;
        private const int HEIGHT = 100;

        static Dragon()
        {
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Dragon", "dragon", NUM_FRAMES);
        }

        public Dragon(Vector2Int tileCoordinate)
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
        }
    }
}
