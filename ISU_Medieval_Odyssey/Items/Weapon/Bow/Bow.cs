// Author: Joon Song
// File Name: Bow.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Bow object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Bow : Weapon
    {
        /// <summary>
        /// The number of frames in <see cref="Bow"/> weapon's animation
        /// </summary>
        public const int NUM_FRAMES = 13;

        // Bow & Arrow specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private static DirectionalSpriteSheet arrowSpriteSheet;

        /// <summary>
        /// Static constructor for <see cref="Bow"/> object
        /// </summary>
        static Bow()
        {
            // Loading in various Bow images
            string basePath = "Images/Sprites/Weapon/Shoot/";
            string weaponTypeName = "bow";
            directionalSpriteSheet = new DirectionalSpriteSheet($"{basePath}Bow/", weaponTypeName, NUM_FRAMES);
            directionalSpriteSheet = new DirectionalSpriteSheet($"{basePath}Arrow/", "arrow", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Bow"/> object
        /// </summary>
        public Bow()
        {
            // Setting up Bow
            base.directionalSpriteSheet = directionalSpriteSheet;
        }

        /// <summary>
        /// Draw subprogram for <see cref="Bow"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="rectangle">The rectangle to draw the <see cref="Bow"/> in</param>
        /// <param name="direction">The direction the <see cref="Bow"/> is pointed at</param>
        /// <param name="currentFrame">The current frame of the <see cref="Bow"/>'s animation</param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Direction direction, int currentFrame)
        {
            base.Draw(spriteBatch, rectangle, direction, currentFrame);
            arrowSpriteSheet.Draw(spriteBatch, direction, currentFrame, rectangle);
        }
    }
}