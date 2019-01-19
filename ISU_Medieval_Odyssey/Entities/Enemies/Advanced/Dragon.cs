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
        // Dragon specific graphics
        private static new DirectionalSpriteSheet directionalSpriteSheet;
        private const int NUM_FRAMES = 4;
        private const int COUNTER_MAX = 8;
        private const int WIDTH = 100;
        private const int HEIGHT = 100;
        private const int HITBOX_BUFFER_X = 0;
        private const int HITBOX_BUFFER_Y = 0;

        private const int MIN_HEALTH = 0;
        private const int MAX_HEALTH = 100;
        private const int MIN_DAMAGE = 50;
        private const int MAX_DAMAGE = 200;
        private const int SCAN_RANGE = 4;

        static Dragon()
        {
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Dragon", "dragon", NUM_FRAMES);
        }

        public Dragon(Vector2Int tileCoordinate)
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
            Initialize(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, NUM_FRAMES, COUNTER_MAX, SCAN_RANGE);
        }
    }
}
