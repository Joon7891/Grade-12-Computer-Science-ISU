// Author: Joon Song
// File Name: KeyBinding.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/12/2019
// Modified Date: 01/12/2019
// Description: Class to hold KeyBinding object

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        /// The <see cref="Rectangle"/> assosiated with this <see cref="KeyBinding"/>
        /// </summary>
        public Rectangle Rectangle { get;  }

        // Various variables required for the drawing of the KeyBinding
        private static Dictionary<Keys, Texture2D> keyImages = new Dictionary<Keys, Texture2D>();
        private static SpriteFont textFont;
        private readonly string text;
        private readonly Vector2 textLocation;

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

            // Adding images to hashmap
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (AllowedBindings.Contains(key))
                {
                    keyImages.Add(key, Main.Content.Load<Texture2D>($"Images/Sprites/Keys/keys_{key.ToString()}"));
                }
            }
        }

        /// <summary>
        /// Constructor for <see cref="KeyBinding"/> object
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <param name="rectangle"></param>
        public KeyBinding(Keys key, string text, Rectangle rectangle)
        {
            Key = key;
            this.text = text;
            Rectangle = rectangle;
        }

        /// <summary>
        /// Draw subprogram for <see cref="KeyBinding"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="isSelected">Whether the <see cref="KeyBinding"/> is selected for modification</param>
        public void Draw(SpriteBatch spriteBatch, bool isSelected)
        {
            // Drawing key image and corresponding text
            spriteBatch.Draw(keyImages[key], Rectangle, isSelected == false ? Color.White : Color.Yellow);
        }
    }
}
