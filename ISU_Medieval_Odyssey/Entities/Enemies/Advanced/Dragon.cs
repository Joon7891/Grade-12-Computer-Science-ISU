// Author: Joon Song
// File Name: Dragon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold Dragon object

namespace ISU_Medieval_Odyssey
{
    public sealed class Dragon : AdvancedEnemy
    {
        // Various Dragon specific graphics and corresponding variables
        private const int WIDTH = 100;
        private const int HEIGHT = 100;
        private const int NUM_FRAMES = 4;
        private const int COUNTER_MAX = 8;       
        private const int HITBOX_BUFFER_X = 0;
        private const int HITBOX_BUFFER_Y = 0;
        private static new DirectionalSpriteSheet directionalSpriteSheet;

        // Various constants regarding the Dragon's attributes
        private const int MIN_HEALTH = 150;
        private const int MAX_HEALTH = 300;
        private const int MIN_DAMAGE = 50;
        private const int MAX_DAMAGE = 80;
        private const int SCAN_RANGE = 8;
        private const float SPEED = 0.9f;
        private const float ATTACK_SPEED = 3.5f;

        /// <summary>
        /// Static constructor for <see cref="Dragon"/> object
        /// </summary>
        static Dragon()
        {
            // Loading in Dragon graphics
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Dragon", "dragon", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Dragon"/> object
        /// </summary>
        /// <param name="tileCoordinate">The coordinate of the <see cref="Tile"/> this <see cref="Dragon"/> is to be created at</param>
        public Dragon(Vector2Int tileCoordinate, bool isInside)
        {
            // Setting up various components of this Dragon
            base.directionalSpriteSheet = directionalSpriteSheet;
            InitializeGraphics(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, NUM_FRAMES, COUNTER_MAX);
            InitializeStatistics(SCAN_RANGE, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, SPEED, ATTACK_SPEED);
        }
    }
}
