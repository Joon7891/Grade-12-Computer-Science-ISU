// Author: Joon Song
// File Name: KeyBinding.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/12/2019
// Modified Date: 01/12/2019
// Description: Class to hold KeyBinding object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public sealed class KeyBinding
    {
        /// <summary>
        /// The allowed <see cref="Keys"/> bindings
        /// </summary>
        public static HashSet<Keys> AllowedBindings { get; private set; } = new HashSet<Keys>();

        /// <summary>
        /// The disallowed <see cref="Keys"/> bindings
        /// </summary>
        public static HashSet<Keys> DisallowedBindings { get; private set; } = new HashSet<Keys>();

        /// <summary>
        /// The <see cref="Keys"/> assosiated with this <see cref="KeyBinding"/>
        /// </summary>
        public Keys Key
        {
            get => key;
            set
            {
                DisallowedBindings.Remove(key);
                key = value;
                DisallowedBindings.Add(key);
            }
        }
        private Keys key;

        /// <summary>
        /// Static constructor for <see cref="KeyBinding"/> object
        /// </summary>
        static KeyBinding()
        {
            // Adding keys to allowed bindings
            for (Keys key = Keys.D0; key <= Keys.Z; ++key)
            {
                AllowedBindings.Add(key);
            }
            for (Keys key = Keys.Left; key <= Keys.Down; ++key)
            {
                AllowedBindings.Add(key);
            }
            AllowedBindings.Add(Keys.Tab);
            AllowedBindings.Add(Keys.Space);
            AllowedBindings.Add(Keys.Enter);
            AllowedBindings.Add(Keys.Escape);
            AllowedBindings.Add(Keys.CapsLock);
            AllowedBindings.Add(Keys.LeftShift);
            AllowedBindings.Add(Keys.RightShift);
        }
    }
}
