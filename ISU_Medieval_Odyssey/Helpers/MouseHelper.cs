// Author: Joon Song
// Project Name: ISU_Medieval_Odyssey
// File Name: MouseHelper.cs
// Creation Date: 09/20/2018
// Modified Date: 09/20/2018
// Desription: Class to hold various subprograms to help with mouse functionality

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public static class MouseHelper
    {
        /// <summary>
        /// Subprogram to check if a mouse click was a new one
        /// </summary>
        /// <returns>Whether or whether not a mouse click was a new one</returns>
        public static bool NewClick() => Main.Instance.NewMouse.LeftButton == ButtonState.Pressed && Main.Instance.OldMouse.LeftButton != ButtonState.Pressed;

        /// <summary>
        /// Subprogram to check if a rectangle is clicked
        /// </summary>
        /// <param name="rectangle">The rectangle to check if clicked</param>
        /// <returns>Whether the rectangle was clicked</returns>
        public static bool IsRectangleClicked(Rectangle rectangle) => NewClick() && CollisionHelper.PointToRect(Location, rectangle);

        /// <summary>
        /// Subprogram to determine the amount that the scroll wheel has moved in the last update
        /// </summary>
        /// <returns>The amount of "Scroll Units" the scroll wheel has moved</returns>
        public static int ScrollAmount() => (Main.Instance.NewMouse.ScrollWheelValue - Main.Instance.OldMouse.ScrollWheelValue) / 120;

        /// <summary>
        /// The location of the mouse on the screen
        /// </summary>
        public static Vector2 Location => Main.Instance.NewMouse.Position.ToVector2();
    }
}