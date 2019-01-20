using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class Skeleton : PrimitiveEnemy
    {
        // Various Skeleton specific graphics and corresponding variables
        private const int WIDTH = 60;
        private const int HEIGHT = 60;
        private const int NUM_FRAMES = 9;
        private const int COUNTER_MAX = 1;
        private const int HITBOX_BUFFER_X = 20;
        private const int HITBOX_BUFFER_Y = 15;
        private static new DirectionalSpriteSheet directionalSpriteSheet;

        // Various constants regarding the Skeleton's attributes
        private const int MIN_HEALTH = 10;
        private const int MAX_HEALTH = 20;
        private const int MIN_DAMAGE = 10;
        private const int MAX_DAMAGE = 25;
        private const int SCAN_RANGE = 10;
        private const float SPEED = 2.5f;

        /// <summary>
        /// Static constructor for <see cref="Skeleton"/> object
        /// </summary>
        static Skeleton()
        {
            // Importing skeleton images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Skeleton", "skeleton", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Skeleton"/> object
        /// </summary>
        /// <param name="tileCoordinate">The coordinate of the <see cref="Tile"/> this <see cref="Wizard"/> is to be created at</param>
        public Skeleton(Vector2Int tileCoordinate)
        {
            // Setting up various components of Skeleton
            base.directionalSpriteSheet = directionalSpriteSheet;
            InitializeGraphics(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, NUM_FRAMES, COUNTER_MAX);
            InitializeStatistics(SCAN_RANGE, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, SPEED);
        }
    }
}
