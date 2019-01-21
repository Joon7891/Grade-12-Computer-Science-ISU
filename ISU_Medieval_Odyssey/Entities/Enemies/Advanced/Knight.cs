// Author: Joon Song
// File Name: Knight.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold Knight object

namespace ISU_Medieval_Odyssey
{
    public sealed class Knight : AdvancedEnemy
    {
        // Various Knight specific graphics and corresponding variables
        private const int WIDTH = 90;
        private const int HEIGHT = 90;
        private const int NUM_FRAMES = 4;
        private const int COUNTER_MAX = 3;
        private const int HITBOX_BUFFER_X = 30;
        private const int HITBOX_BUFFER_Y = 0;
        private static new DirectionalSpriteSheet directionalSpriteSheet;

        // Various constants regarding the Knight's attributes
        private const int MIN_HEALTH = 40;
        private const int MAX_HEALTH = 80;
        private const int MIN_DAMAGE = 10;
        private const int MAX_DAMAGE = 30;
        private const int SCAN_RANGE = 20;
        private const float SPEED = 4.0f;
        private const float ATTACK_SPEED = 2.0f;

        /// <summary>
        /// Static constructor for <see cref="Knight"/> object
        /// </summary>
        static Knight()
        {
            // Loading in Wizard images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Knight", "knight", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Knight"/> object
        /// </summary>
        /// <param name="tileCoordinate">The coordinate of the <see cref="Tile"/> this <see cref="Knight"/> is to be created at</param>
        public Knight(Vector2Int tileCoordinate, bool isInside)
        {
            // Setting up various components of Knight
            base.directionalSpriteSheet = directionalSpriteSheet;
            InitializeGraphics(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, NUM_FRAMES, COUNTER_MAX);
            InitializeStatistics(SCAN_RANGE, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, SPEED, ATTACK_SPEED);
            this.isInside = isInside;
        }
    }
}