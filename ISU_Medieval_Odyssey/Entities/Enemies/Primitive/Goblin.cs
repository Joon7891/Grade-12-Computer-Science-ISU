using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public sealed class Goblin : PrimitiveEnemy
    {
        // Goblin specific graphics and constants
        private static new DirectionalSpriteSheet directionalSpriteSheet;
        private const int NUM_FRAMES = 7;
        private const int COUNTER_MAX = 5;
        private const int WIDTH = 80;
        private const int HEIGHT = 80;
        private const int MIN_HEALTH = 10;
        private const int MAX_HEALTH = 15;
        private const int MIN_DAMAGE = 10;
        private const int MAX_DAMAGE = 15;

        /// <summary>
        /// Static constructor for <see cref="Goblin"/>
        /// </summary>
        static Goblin()
        {
            // Loading in various Goblin specific images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Goblin", "goblin", NUM_FRAMES);
        }

        public Goblin(Vector2Int tileCoordinate)
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
        }
    }
}
