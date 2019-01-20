using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class CollisionRectangle : ICollidable
    {
        public Rectangle HitBox { get; set; }

        public CollisionRectangle(Rectangle rectangle)
        {
            HitBox = rectangle;
        }
    }
}
