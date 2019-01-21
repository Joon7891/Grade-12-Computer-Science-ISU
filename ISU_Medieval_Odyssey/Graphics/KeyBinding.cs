// Author: Joon Song
// File Name: KeyBinding.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/12/2019
// Modified Date: 01/18/2019
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
        /// The text describing this <see cref="KeyBinding"/>
        /// </summary>
        public string Text { get; }

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
        public Rectangle Rectangle { get; }

        /// <summary>
        /// The allowed <see cref="Keys"/> bindings
        /// </summary>
        public static HashSet<Keys> AllowedBindings { get; private set; } = new HashSet<Keys>();

        /// <summary>
        /// The disallowed <see cref="Keys"/> bindings
        /// </summary>
        public static HashSet<Keys> DisallowedBindings { get; private set; } = new HashSet<Keys>();

        // Various variables required for the drawing of the KeyBinding
        private static Dictionary<Keys, Texture2D> keyImages = new Dictionary<Keys, Texture2D>();
        private static SpriteFont textFont;
        private readonly Vector2 textLocation;
        private readonly bool showText;

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
            for (Keys key = Keys.F1; key <= Keys.F12; ++key)
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

            // Importing fonts
            textFont = Main.Content.Load<SpriteFont>("Fonts/KeyBindingFont");
        }

        /// <summary>
        /// Constructor for <see cref="KeyBinding"/> object
        /// </summary>
        /// <param name="key">The <see cref="Keys"/> assosiated with this <see cref="KeyBinding"/></param>
        /// <param name="text">The text to be draw alongside this <see cref="KeyBinding"/></param>
        /// <param name="rectangle">The <see cref="Rectangle"/> in which this <see cref="KeyBinding"/> is to be drawn in</param>
        /// <param name="showText">Whether to show the text on this <see cref="KeyBinding"/></param>
        public KeyBinding(Keys key, string text, Rectangle rectangle, bool showText = true)
        {
            // Assigning Keybinding object properties
            Key = key;
            Text = text;
            Rectangle = rectangle;
            this.showText = showText;
            DisallowedBindings.Add(key);
            textLocation = new Vector2(rectangle.X + (rectangle.Width - textFont.MeasureString(text).X) / 2, rectangle.Y - 35);
        }

        /// <summary>
        /// Draw subprogram for <see cref="KeyBinding"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="isSelected">Whether the <see cref="KeyBinding"/> is selected for modification</param>
        public void Draw(SpriteBatch spriteBatch, bool isSelected = false)
        {
            // Drawing key image and corresponding text
            spriteBatch.Draw(keyImages[key], Rectangle, !isSelected ? Color.White : Color.Yellow);
            if (showText)
            {
                spriteBatch.DrawString(textFont, Text, textLocation, !isSelected ? Color.White : Color.Yellow);
            }
        }

        /// <summary>
        /// Subprogram to simply draw a certain <see cref="Keys"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        /// <param name="key">The <see cref="Keys"/> to be drawn</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> to draw it in</param>
        public static void DrawKey(SpriteBatch spriteBatch, Keys key, Rectangle rectangle)
        {
            // Drawing key
            spriteBatch.Draw(keyImages[key], rectangle, Color.White);
        }
    }
}
