// Author: Joon Song
// File Name: Rapier.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Rapier object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Rapier : SlashWeapon
    {
        // Rapier specific graphics
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private new static Texture2D iconImage;

        /// <summary>
        /// Static constructor for <see cref="Rapier"/> object
        /// </summary>
        static Rapier()
        {
            // Loading in various Rapier graphics
            string basePath = "Images/Sprites/Weapon/Slash/Rapier/";
            string weaponTypeName = "rapier";
            directionalSpriteSheet = new DirectionalSpriteSheet(basePath, weaponTypeName, NUM_FRAMES);
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/rapierIcon");
        }

        /// <summary>
        /// Constructor for <see cref="Rapier"/> object
        /// </summary>
        public Rapier()
        {
            // Setting up Rapier
            base.directionalSpriteSheet = directionalSpriteSheet;
            base.iconImage = iconImage;
        }

        /// <summary>
        /// Subprogram to draw <see cref="Rapier"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="playerRectangle">The corresponding player's rectangle</param>
        /// <param name="movementType">The movement type</param>
        /// <param name="direction">The current direction</param>
        /// <param name="currentFrame">The current frame number</param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, Direction direction, int currentFrame)
        {
            // Updating adjusted image rectangle and drawing Rapier
            adjustedRectangle.X = playerRectangle.X - playerRectangle.Width;
            adjustedRectangle.Y = playerRectangle.Y - playerRectangle.Height;
            directionalSpriteSheet.Draw(spriteBatch, direction, currentFrame, adjustedRectangle);
        }
    }
}
