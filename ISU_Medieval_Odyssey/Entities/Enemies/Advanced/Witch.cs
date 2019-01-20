// Author: Joon Song
// File Name: Witch.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold Witch object

using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public sealed class Witch : AdvancedEnemy
    {
        // Various Witch specific graphics and corresponding variables
        private const int WIDTH = 60;
        private const int HEIGHT = 90;
        private const int NUM_FRAMES = 4;
        private const int COUNTER_MAX = 5;
        private const int HITBOX_BUFFER_X = 0;
        private const int HITBOX_BUFFER_Y = 0;
        private static new DirectionalSpriteSheet directionalSpriteSheet;

        // Various constants regarding the Witch's attributes
        private const int MIN_HEALTH = 50;
        private const int MAX_HEALTH = 150;
        private const int MIN_DAMAGE = 50;
        private const int MAX_DAMAGE = 80;
        private const int SCAN_RANGE = 20;
        private const float SPEED = 2.5f;

        // Various variables for the Witch's ability to span skeleton
        private float timeToSpawn = 0;
        private const int SPAWN_TIME = 5;

        /// <summary>
        /// Static constructor for <see cref="Witch"/> object =
        /// </summary>
        static Witch()
        {
            // Loading in Witch graphics
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Witch", "witch", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Witch"/> object
        /// </summary>
        /// <param name="tileCoordinate">The coordinate of the <see cref="Tile"/> this <see cref="Witch"/> is to be created at</param>
        public Witch(Vector2Int tileCoordinate)
        {
            // Setting up various components of Witch
            base.directionalSpriteSheet = directionalSpriteSheet;
            InitializeGraphics(tileCoordinate, WIDTH, HEIGHT, HITBOX_BUFFER_X, HITBOX_BUFFER_Y, NUM_FRAMES, COUNTER_MAX);
            InitializeStatistics(SCAN_RANGE, MIN_HEALTH, MAX_HEALTH, MIN_DAMAGE, MAX_DAMAGE, SPEED);
        }

        /// <summary>
        /// Update subprogram for <see cref="Witch"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing objects</param>
        public override void Update(GameTime gameTime)
        {
            // Calling base update subprogram
            base.Update(gameTime);

            // Spawning a Skeleton every 5 seconds
            timeToSpawn += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (timeToSpawn >= SPAWN_TIME)
            {
                World.Instance.AddEnemy(new Skeleton(CurrentTile));
                timeToSpawn = 0;
            }
        }
    }
}
