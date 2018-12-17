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
        public static bool NewClick() => Main.NewMouse.LeftButton == ButtonState.Pressed && Main.OldMouse.LeftButton != ButtonState.Pressed;
    }
}