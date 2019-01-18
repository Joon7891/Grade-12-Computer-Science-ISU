using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class Witch : SmartEnemy
    {
        private static new DirectionalSpriteSheet directionalSpriteSheet;
        private const int NUM_FRAMES = 4;
        private const int COUNTER_MAX = 5;
        private const int WIDTH = 60;
        private const int HEIGHT = 90;

        static Witch()
        {
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Witch", "witch", NUM_FRAMES);
        }

        public Witch(Vector2Int tileCoordinate)
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
        }
    }
}
