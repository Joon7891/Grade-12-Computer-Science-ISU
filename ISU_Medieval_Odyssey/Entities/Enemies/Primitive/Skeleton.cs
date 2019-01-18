using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class Skeleton : PrimitiveEnemy
    {
        private static new DirectionalSpriteSheet directionalSpriteSheet;
        private const int NUM_FRAMES = 9;
        private const int COUNTER_MAX = 5;
        private const int WIDTH = 60;
        private const int HEIGHT = 60;

        static Skeleton()
        {
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Enemies/Skeleton", "skeleton", NUM_FRAMES);
        }

        public Skeleton(Vector2Int tileCoordinate)
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
        }
    }
}
