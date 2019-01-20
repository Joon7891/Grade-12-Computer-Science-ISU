// Author: Joon Song
// File Name: Goblin.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/17/2019
// Modified Date: 01/17/2019
// Description: Class to hold Goblin object

namespace ISU_Medieval_Odyssey
{
    public sealed class Goblin : PrimitiveEnemy
    {
        // Various Goblin specific graphics and corresponding variables
        private const int WIDTH = 80;
        private const int HEIGHT = 80;
        private const int NUM_FRAMES = 7;
        private const int COUNTER_MAX = 5;
        private const int HITBOX_BUFFER_X = 0;
        private const int HITBOX_BUFFER_Y = 10;
        private static new DirectionalSpriteSheet directionalSpriteSheet;

        // Various constants regarding the Goblin's attributes
        private const int MIN_HEALTH = 10;
        private const int MAX_HEALTH = 15;
        private const int MIN_DAMAGE = 5;
        private const int MAX_DAMAGE = 10;
        private const int SCAN_RANGE = 8;
        private const float SPEED = 4.5f;
        private const float ATTACK_SPEED = 1.0f;

        /// <summary>
        /// Static constructor for <see cref="Goblin"/>
        /// </summary>
        static Goblin()
        {
            // Loading in various Goblin specific images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Goblin", "goblin", NUM_FRAMES);
        }

        /// <summary>
        /// Constuctor for <see cref="Goblin"/> object
        /// </summary>
        /// <param name="tileCoordinate">The <see cref="Tile"/> coordinate of where to create the Goblin</param>
        public Goblin(Vector2Int tileCoordinate)
        {
            // Setting up various components of Goblin
            base.directionalSpriteSheet = directionalSpriteSheet;
            InitializeGraphics(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, NUM_FRAMES, COUNTER_MAX);
            InitializeStatistics(SCAN_RANGE, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, SPEED, ATTACK_SPEED);
        }
    }
}
