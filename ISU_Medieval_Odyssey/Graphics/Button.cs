// Author: Joon Song
// Project Name: ISU_Medieval_Odyssey
// File Name: Button.cs
// Creation Date: 09/10/2018
// Modified Date: 09/18/2018
// Desription: Class to hold Button object

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace ISU_Medieval_Odyssey
{
    public sealed class Button : IGraphic
    {
        // Variables button click related fields
        private static SoundEffect clickSoundEffect;
        private static SoundEffect errorSoundEffect;

        /// <summary>
        /// The procedure to execute on a <see cref="Button"/>'s click
        /// </summary>
        public delegate void OnClick();
        private OnClick onClick;

        // Variables to hold the image and rectangular dimensions of button
        private Texture2D image;
        private Rectangle rect;

        /// <summary>
        /// Whether or whether not the button is active
        /// </summary>
        public bool Active { get; set; }
        
        /// <summary>
        /// Whether the mouse is hovering above the button
        /// </summary>
        public bool IsMouseHovering => CollisionHelper.PointToRect(Main.NewMouse.Position.ToVector2(), rect);

        /// <summary>
        /// Static constructor to setup <see cref="Button"/> components
        /// </summary>
        static Button()
        {
            // Importing various sound effects
            clickSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/buttonClick");
            errorSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/errorSoundEffect");
        }

        /// <summary>
        /// Constructor for button object
        /// </summary>
        /// <param name="image">The image of the button</param>
        /// <param name="rect">The rectangle representing the rectangular dimensions of the button</param>
        /// <param name="onClick">The behavior of the button when clicked</param>
        /// <param name="active">Whether the button is active/can be used - true by default</param>
        public Button(Texture2D image, Rectangle rect, OnClick onClick, bool active = true)
        {
            // Setting up button variables from parameters
            this.onClick = onClick;
            this.image = image;
            this.rect = rect;
            Active = active;
        }

        /// <summary>
        /// Update various button components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // If button is pressed - inoke behavior and click sound if button is active, otherwise error sound
            if (MouseHelper.IsRectangleLeftClicked(rect))
            {
                if (Active)
                {
                    clickSoundEffect.CreateInstance().Play();
                    onClick();
                }
                else
                {
                    errorSoundEffect.CreateInstance().Play();
                }
            }
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing button; transparency is higher if mouse is not hovering over button
            spriteBatch.Draw(image, rect, Color.White * (0.6f + 0.4f * Convert.ToByte(IsMouseHovering && Active)));
        }
    }
}