// Author: Joon Song
// Project Name: ISU_Medieval_Odyssey
// File Name: ColissionHelper.cs
// Creation Date: 09/10/2018
// Modified Date: 01/06/2018
// Desription: Class to hold various collision detection functions

using System;
using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public static class CollisionHelper
    {
        /// <summary>
        /// Subprogram to detect for collision between a point and a rectangle
        /// </summary>
        /// <param name="point">The point to check for collision</param>
        /// <param name="rect">The rectangle to check for collision</param>
        /// <returns>Whether the point is inside the rectangle</returns>
        public static bool PointToRect(Vector2 point, Rectangle rect) => rect.Contains(point.X, point.Y);

        /// <summary>
        /// Subprogram to detect for colission between a point and a circle
        /// </summary>
        /// <param name="point">The point to check for collision</param>
        /// <param name="circle">The circle to check for collision</param>
        /// <returns>Whether the point is inside circle</returns>
        public static bool PointToCircle(Vector2 point, Circle circle) => 
            Math.Pow(point.X - circle.X, 2) + Math.Pow(point.Y - circle.Y, 2) <= Math.Pow(circle.Radius, 2);
    }
}