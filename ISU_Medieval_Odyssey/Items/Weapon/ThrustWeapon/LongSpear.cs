// Author: Joon Song
// File Name: LongSpear.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LongSpear object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LongSpear : ThrustWeapon
    {
        // LongSpear specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private new static Texture2D iconImage;
        private new static Rectangle verticalHitBox = new Rectangle(0, 0, 20, 100);
        private new static Rectangle horizontalHitBox = new Rectangle(0, 0, 100, 20);

        // Various constants for LongSpear components
        private const int MIN_DAMAGE = 20;
        private const int MAX_DAMAGE = 50;
        private const int MIN_DURABILITY = 50;
        private const int MAX_DURABILITY = 200;

        /// <summary>
        /// Static constructor for <see cref="LongSpear"/> object
        /// </summary>
        static LongSpear()
        {
            // Loading in various LongSpear images            
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Weapon/Thrust/LongSpear/", "longSpear", NUM_FRAMES);
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/spearIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LongSpear"/> object
        /// </summary>
        public LongSpear()
        {
            // Setting up LongSpear
            itemName = "Long Spear";
            base.directionalSpriteSheet = directionalSpriteSheet;
            base.iconImage = iconImage;
            base.verticalHitBox = verticalHitBox;
            base.horizontalHitBox = horizontalHitBox;
            Initialize(MIN_DAMAGE, MAX_DAMAGE, MIN_DURABILITY, MAX_DURABILITY);
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
