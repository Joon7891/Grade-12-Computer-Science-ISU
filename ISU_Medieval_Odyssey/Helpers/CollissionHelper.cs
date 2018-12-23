﻿// Author: Joon Song
// Project Name: ISU_Medieval_Odyssey
// File Name: ColissionHelper.cs
// Creation Date: 09/10/2018
// Modified Date: 09/18/2018
// Desription: Class to hold various collision detection functions

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey.Helpers
{
    public static class CollisionHelper
    {
        /// <summary>
        /// Subprogram to detect for collision between a point and a rect
        /// </summary>
        /// <param name="point">The point to check for collision</param>
        /// <param name="rect">The rectangle to check for collision</param>
        /// <returns>Whether or whether not point is inside rectangle</returns>
        public static bool PointToRect(Vector2 point, Rectangle rect) => rect.Contains(point.X, point.Y);
    }
}