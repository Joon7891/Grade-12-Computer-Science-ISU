// Author: Joon Song
// Project Name: ISU_Medieval_Odyssey
// File Name: KeyboardHelper.cs
// Creation Date: 09/20/2018
// Modified Date: 09/20/2018
// Desription: Class to hold various subprograms to help with keyboard functionality

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
    public static class KeyboardHelper
    {
        /// <summary>
        /// Subprogram to check if a keystroke was a new one
        /// </summary>
        /// <param name="key">The key to check for new keystroke</param>
        /// <returns>Whether the keystroke was a new one</returns>
        public static bool NewKeyStroke(Keys key) => Main.Instance.NewKeyboard.IsKeyDown(key) && !Main.Instance.OldKeyboard.IsKeyDown(key);
    }
}