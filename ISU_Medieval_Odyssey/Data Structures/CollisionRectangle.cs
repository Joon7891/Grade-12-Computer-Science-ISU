// Author: Steven Ung
// File Name: CollisionRectangle
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Allows xna rectangles to be ICollidable
using Microsoft.Xna.Framework;

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
