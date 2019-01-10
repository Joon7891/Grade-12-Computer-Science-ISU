using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LongSpear : ThrustWeapon
    {
        private new static DirectionalSpriteSheet directionalSpriteSheet;

        static LongSpear()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Weapon/Thrust/LongSpear/";
            string weaponTypeName = "longSpear";
            directionalSpriteSheet = new DirectionalSpriteSheet(basePath, weaponTypeName, NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="LongSpear"/> object
        /// </summary>
        public LongSpear()
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
        }

        /// <summary>
        /// Draw subprogram for <see cref="LongSpear"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="playerRectangle"></param>
        /// <param name="direction"></param>
        /// <param name="currentFrame"></param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, Direction direction, int currentFrame)
        {
            adjustedRectangle.X = playerRectangle.X - playerRectangle.Width;
            adjustedRectangle.Y = playerRectangle.Y - playerRectangle.Height;
            directionalSpriteSheet.Draw(spriteBatch, direction, currentFrame, adjustedRectangle);
        }
    }
}
