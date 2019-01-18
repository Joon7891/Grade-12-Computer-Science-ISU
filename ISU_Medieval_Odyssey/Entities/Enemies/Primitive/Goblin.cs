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
        // Goblin specific graphics and consants
        private static new DirectionalSpriteSheet directionalSpriteSheet;
        private const int NUM_FRAMES = 7;
        private const int COUNTER_MAX = 5;
        private const int WIDTH = 100;
        private const int HEIGHT = 100;

        /// <summary>
        /// Static constructor for <see cref="Goblin"/>
        /// </summary>
        static Goblin()
        {
            // Loading in various Goblin specific images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Goblin", "goblin", NUM_FRAMES);
        }
    }
}
