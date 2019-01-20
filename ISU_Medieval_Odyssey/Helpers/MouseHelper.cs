// Author: Joon Song
// Project Name: ISU_Medieval_Odyssey
// File Name: MouseHelper.cs
// Creation Date: 09/20/2018
// Modified Date: 09/20/2018
// Desription: Class to hold various subprograms to help with mouse functionality

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public static class MouseHelper
    {
        /// <summary>
        /// Subprogram to check if a left mouse click was a new one
        /// </summary>
        /// <returns>Whether a left mouse click was a new one</returns>
        public static bool NewLeftClick() => Main.NewMouse.LeftButton == ButtonState.Pressed && Main.OldMouse.LeftButton != ButtonState.Pressed;

        /// <summary>
        /// Subprogram t check if a right mouse click was a new one
        /// </summary>
        /// <returns>Whwther a mouse right click was a new one</returns>
        public static bool NewRightClick() => Main.NewMouse.RightButton == ButtonState.Pressed && Main.OldMouse.RightButton != ButtonState.Pressed;

        /// <summary>
        /// Subprogram to check if the left button of the mouse is down
        /// </summary>
        /// <returns>Whether the left button of the mouse is down</returns>
        public static bool IsLeftDown() => Main.NewMouse.LeftButton == ButtonState.Pressed;

        /// <summary>
        /// Subprogram to check if the right button of the mouse is down
        /// </summary>
        /// <returns>Whether the right button of the mouse is down</returns>
        public static bool IsRightDown() => Main.NewMouse.RightButton == ButtonState.Pressed;

        /// <summary>
        /// Determine if the mouse is hovering over a <see cref="Rectangle"/>
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to check for hovering</param>
        /// <returns>Whether the mouse is hovering over the <see cref="Rectangle"/></returns>
        public static bool IsHovering(Rectangle rectangle) => rectangle.Contains(Location);

        /// <summary>
        /// Subprogram to check if a <see cref="Rectangle"/> is clicked via left mouse button
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to check if clicked via left mouse button</param>
        /// <returns>Whether the <see cref="Rectangle"/> was clicked</returns>
        public static bool IsRectangleLeftClicked(Rectangle rectangle) => NewLeftClick() && CollisionHelper.PointToRect(Location, rectangle);

        /// <summary>
        /// Subprogram to check if a <see cref="Rectangle"/> is selected via left mouse button
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to check if selected via left mouse button</param>
        /// <returns>Whether the <see cref="Rectangle"/> is selected</returns>
        public static bool IsRectangleLeftSelected(Rectangle rectangle) => IsLeftDown() && CollisionHelper.PointToRect(Location, rectangle);

        /// <summary>
        /// Subprogram to check if a <see cref="Rectangle"/> is clicked via right mouse button
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to check if clicked via right mouse button</param>
        /// <returns>Whether the <see cref="Rectangle"/> was clicked</returns>
        public static bool IsRectangleRightClicked(Rectangle rectangle) => NewRightClick() && CollisionHelper.PointToRect(Location, rectangle);

        /// <summary>
        /// Subprogram to check if a <see cref="Rectangle"/> is selected via right mouse button
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to check if selected via right mouse button</param>
        /// <returns>Whether the <see cref="Rectangle"/> is selected</returns>
        public static bool IsRectangleRightSelected(Rectangle rectangle) => IsRightDown() && CollisionHelper.PointToRect(Location, rectangle);

        /// <summary>
        /// Subprogram to check if a circle is selected via left mouse button
        /// </summary>
        /// <param name="circle">The circle ot check if selected</param>
        /// <returns>Whether the circle is seleected</returns>
        public static bool IsCircleSelected(Circle circle) => IsLeftDown() && CollisionHelper.PointToCircle(Location, circle);          

        /// <summary>
        /// Subprogram to determine the amount that the scroll wheel has moved in the last update
        /// </summary>
        /// <returns>The amount of "Scroll Units" the scroll wheel has moved</returns>
        public static int ScrollAmount() => (Main.NewMouse.ScrollWheelValue - Main.OldMouse.ScrollWheelValue) / 120;

        /// <summary>
        /// The location of the mouse on the screen
        /// </summary>
        public static Vector2 Location => Main.NewMouse.Position.ToVector2();
    }
}