using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LongSpear : ThrustWeapon
    {
        // LongSpear specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;

        /// <summary>
        /// Static constructor for <see cref="LongSpear"/> object
        /// </summary>
        static LongSpear()
        {
            // Loading in various LongSpear images            
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Weapon/Thrust/LongSpear/", "longSpear", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="LongSpear"/> object
        /// </summary>
        public LongSpear()
        {
            // Setting up LongSpear
            base.directionalSpriteSheet = directionalSpriteSheet;
        }

        /// <summary>
        /// Draw subprogram for <see cref="LongSpear"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="rectangle">The rectangle to draw the <see cref="LongSpear"/> in</param>
        /// <param name="direction">The direction the <see cref="LongSpear"/> is pointed at</param>
        /// <param name="currentFrame">The current frame of the <see cref="LongSpear"/> animation</param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Direction direction, int currentFrame)
        {
            // Adjusting rectangle and drawing LongSpear
            adjustedRectangle.X = rectangle.X - rectangle.Width;
            adjustedRectangle.Y = rectangle.Y - rectangle.Height;
            directionalSpriteSheet.Draw(spriteBatch, direction, currentFrame, adjustedRectangle);
        }
    }
}
