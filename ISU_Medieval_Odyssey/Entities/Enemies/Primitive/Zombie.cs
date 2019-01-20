// Author: Joon Song
// File Name: Zombie.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/17/2019
// Modified Date: 01/17/2019
// Description: Class to hold Zombie object

namespace ISU_Medieval_Odyssey
{
    public sealed class Zombie : PrimitiveEnemy
    {
        // Various Zombie specific graphics and corresponding variables
        private const int WIDTH = 40;
        private const int HEIGHT = 80;
        private const int NUM_FRAMES = 3;
        private const int COUNTER_MAX = 5;
        private const int HITBOX_BUFFER_X = 0;
        private const int HITBOX_BUFFER_Y = 20;
        private static new DirectionalSpriteSheet directionalSpriteSheet;

        // Various constants regarding the Zombie's attributes
        private const int MIN_HEALTH = 50;
        private const int MAX_HEALTH = 60;
        private const int MIN_DAMAGE = 20;
        private const int MAX_DAMAGE = 30;
        private const int SCAN_RANGE = 8;
        private const float SPEED = 2.5f;

        /// <summary>
        /// Static constructor for <see cref="Zombie"/>
        /// </summary>
        static Zombie()
        {
            // Loading in various Zombie specific images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Zombie", "zombie", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Zombie"/> object
        /// </summary>
        /// <param name="tileCoordinate">The <see cref="Tile"/> coordiante of where to create this <see cref="Enemy"/></param>
        public Zombie(Vector2Int tileCoordinate)
        {
            // Setting up various components of Zombie
            base.directionalSpriteSheet = directionalSpriteSheet;
            InitializeGraphics(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, NUM_FRAMES, COUNTER_MAX);
            InitializeStatistics(SCAN_RANGE, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, SPEED);
        }
    }
}
