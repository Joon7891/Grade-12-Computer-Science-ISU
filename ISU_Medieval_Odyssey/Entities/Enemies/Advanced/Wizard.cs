// Author: Joon Song
// File Name: Wizard.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold Wizard object

namespace ISU_Medieval_Odyssey
{
    public sealed class Wizard : AdvancedEnemy
    {
        // Various Wizard specific graphics and corresponding variables
        private const int WIDTH = 50;
        private const int HEIGHT = 75;
        private const int NUM_FRAMES = 3;
        private const int COUNTER_MAX = 7;
        private const int HITBOX_BUFFER_X = 0;
        private const int HITBOX_BUFFER_Y = 0;
        private static new DirectionalSpriteSheet directionalSpriteSheet;

        // Various constants regarding the Wizard's attributes
        private const int MIN_HEALTH = 50;
        private const int MAX_HEALTH = 150;
        private const int MIN_DAMAGE = 100;
        private const int MAX_DAMAGE = 200;
        private const int SCAN_RANGE = 10;
        private const float SPEED = 2.5f;

        /// <summary>
        /// Static constructor for <see cref="Wizard"/> object
        /// </summary>
        static Wizard()
        {
            // Loading in Wizard images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Wizard", "wizard", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Wizard"/> object
        /// </summary>
        /// <param name="tileCoordinate">The coordinate of the <see cref="Tile"/> this <see cref="Wizard"/> is to be created at</param>
        public Wizard(Vector2Int tileCoordinate)
        {
            // Setting up various components of Wizard
            base.directionalSpriteSheet = directionalSpriteSheet;
            InitializeGraphics(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, NUM_FRAMES, COUNTER_MAX);
            InitializeStatistics(SCAN_RANGE, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, SPEED);
        }
    }
}
