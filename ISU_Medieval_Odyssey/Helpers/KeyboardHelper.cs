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

namespace ISU_Medieval_Odyssey
{
    public static class KeyboardHelper
    {        
        /// <summary>
        /// Subprogram to check if a keystroke was a new one
        /// </summary>
        /// <param name="key">The key to check for new keystroke</param>
        /// <returns>Whether the keystroke was a new one</returns>
        public static bool NewKeyStroke(Keys key) => Main.NewKeyboard.IsKeyDown(key) && !Main.OldKeyboard.IsKeyDown(key);

        /// <summary>
        /// Subprogram to check if a keystroke is currently pressed/down
        /// </summary>
        /// <param name="key">The key to check if it is down</param>
        /// <returns>Whether or whether not the key is currently down</returns>
        public static bool IsKeyDown(Keys key) => Main.NewKeyboard.IsKeyDown(key);

        /// <summary>
        /// Subprogram to determine if any keys in an array of keys are down
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool IsAnyKeyDown(params Keys[] keys)
        {
            // Returning true of any of the keys are down
            foreach (Keys key in keys)
            {
                if (IsKeyDown(key))
                {
                    return true;
                }
            }

            // Otherwise returning false
            return false;
        }

        /// <summary>
        /// Subprogram to determine the pressed key given a set of allowed and disallowed keys
        /// </summary>
        /// <param name="allowedKeys">The allowed keys</param>
        /// <param name="disallowedKeys">The disallowed keys</param>
        /// <returns>The key that is pressed and meets the set requirements</returns>
        public static Keys SelectedKeyFromSet(HashSet<Keys> allowedKeys, HashSet<Keys> disallowedKeys)
        {
            // Looping through new keystroke keys and returning if it meets set requirements
            foreach (Keys key in Main.NewKeyboard.GetPressedKeys())
            {
                if (NewKeyStroke(key) && allowedKeys.Contains(key) && !disallowedKeys.Contains(key))
                {
                    return key;
                }
            }
            
            // Otherwise returning None
            return Keys.None;
        }

        /// <summary>
        /// Subprogram to build a string with keystrokes
        /// </summary>
        /// <param name="stringToBuild">The string to be built</param>
        /// <param name="maxLength">The maximum allowed length of the string</param>
        public static void BuildString(ref string stringToBuild, int maxLength)
        {
            // Building string of max length has not been reached and a keystroke has been pressed
            if (stringToBuild.Length < maxLength)
            {
                // Buidling letters
                for (Keys key = Keys.A; key <= Keys.Z; ++key)
                {
                    if (NewKeyStroke(key))
                    {
                        stringToBuild += key.ToString();
                    }
                }

                // Building numbers
                for (Keys key = Keys.D0; key <= Keys.D9; ++key)
                {
                    if (NewKeyStroke(key))
                    {
                        stringToBuild += key.ToString()[1];
                    }
                }                      
            }

            // Removing last character if backspace is pressed and string is long enough
            if (0 < stringToBuild.Length && NewKeyStroke(Keys.Back))
            {
                stringToBuild = stringToBuild.Substring(0, stringToBuild.Length - 1);
            }
        }

        /// <summary>
        /// Subprogram to build a number using keystrokes
        /// </summary>
        /// <param name="numberToBuild">The number to build - in string format</param>
        /// <param name="maxLength">The maximum allowed length of the number</param>
        public static void BuildNumber(ref string numberToBuild, int maxLength)
        {
            // Building number string if max length has not been reached and number key is pressed
            if (numberToBuild.Length < maxLength)
            {
                for (Keys key = Keys.D0; key <= Keys.D9; ++key)
                {
                    if (NewKeyStroke(key))
                    {
                        numberToBuild += key.ToString()[1];
                    }
                }
            }

            // Removing last character if backspace is pressed and string is long enough
            if (0 < numberToBuild.Length && NewKeyStroke(Keys.Back))
            {
                numberToBuild = numberToBuild.Substring(0, numberToBuild.Length - 1);
            }
        }
    }
}