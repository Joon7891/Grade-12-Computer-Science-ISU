// Author: Joon Song
// File Name: Sword.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Sword object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Sword : SlashWeapon
    {
        // Various Sword specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private new static Texture2D iconImage;

        /// <summary>
        /// Static constructor for <see cref="Sword"/> object
        /// </summary>
        static Sword()
        {
            // Loading in various Sword images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Weapon/Slash/Sword/", "sword", NUM_FRAMES);
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/swordIcon");
        }

        /// <summary>
        /// Constuctor for <see cref="Sword"/> object
        /// </summary>
        public Sword()
        {
            // Setting up sword
            base.directionalSpriteSheet = directionalSpriteSheet;
            base.iconImage = iconImage;
        }

        /// <summary>
        /// Draw subprogram for <see cref="Sword"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="rectangle">The rectangle in which to draw the <see cref="Sword"/></param>
        /// <param name="direction">The direction the <see cref="Sword"/> is pointed at</param>
        /// <param name="currentFrame">The current frame of the <see cref="Sword"/>'s animation/param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Direction direction, int currentFrame)
        {
            // Adjuting rectangle and drawing Sword
            adjustedRectangle.X = rectangle.X - rectangle.Width;
            adjustedRectangle.Y = rectangle.Y - rectangle.Height;
            directionalSpriteSheet.Draw(spriteBatch, direction, currentFrame, adjustedRectangle);
        }
    }
}
