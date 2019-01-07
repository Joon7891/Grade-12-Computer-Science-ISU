// Author: Joon Song
// File Name: KeyBindings.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/07/2019
// Modified Date: 01/07/2019
// Description: Class to hold various Keybindings

using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public static class KeyBindings
    {
        /// <summary>
        /// The keybinding for the move Up functionality
        /// </summary>
        public static Keys Up { get; set; } = Keys.W;

        /// <summary>
        /// The keybinding for the move Down functionality 
        /// </summary>
        public static Keys Right { get; set; } = Keys.D;

        /// <summary>
        /// The keybinding for the move Down functionality
        /// </summary>
        public static Keys Down { get; set; } = Keys.S;

        /// <summary>
        /// The keybinding for the move Left functionality
        /// </summary>
        public static Keys Left { get; set; } = Keys.A;

        /// <summary>
        /// The keybinding for the Interaction functionality
        /// </summary>
        public static Keys Interact { get; set; } = Keys.E;

        public static Keys[] HotbarShortcut { get; set; } = new Keys[10];

        static KeyBindings()
        {
            HotbarShortcut[0] = Keys.D1;
            HotbarShortcut[1] = Keys.D2;
            HotbarShortcut[2] = Keys.D3;
            HotbarShortcut[3] = Keys.D4;
            HotbarShortcut[4] = Keys.D5;
            HotbarShortcut[5] = Keys.D6;
            HotbarShortcut[6] = Keys.D7;
            HotbarShortcut[7] = Keys.D8;
            HotbarShortcut[8] = Keys.D9;
            HotbarShortcut[9] = Keys.D0;
        }
    }
}
