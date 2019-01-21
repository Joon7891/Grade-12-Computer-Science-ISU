// Author: Joon Song
// File Name: SpeedPotion.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/15/2019
// Modified Date: 01/15/2019
// Description: Class to hold SpeedPotion object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class SpeedPotion : Potion
    {
        // SpeedPotion specific constants
        private new static Texture2D iconImage;
        private const int TIME_AMOUNT = 30;
        private const int VALUE = 25;
        public const float BOOST_AMOUNT = 0.5f;

        /// <summary>
        /// Static constructor for <see cref="SpeedPotion"/> object
        /// </summary>
        static SpeedPotion()
        {
            // Importing speed potion icon image
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/speedPotionIcon");
        }

        /// <summary>
        /// Constructor for <see cref="SpeedPotion"/> object
        /// </summary>
        public SpeedPotion()
        {
            // Setting up speed potion
            itemName = "Speed Potion";
            base.iconImage = iconImage;
            Value = VALUE;
        }

        /// <summary>
        /// Subprogram to use the <see cref="SpeedPotion"/> <see cref="Item"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using the <see cref="SpeedPotion"/></param>
        public override void Use(Player player)
        {
            // Increasing player's speed boost time and calling base use subprogram
            player.SpeedBoostTime += TIME_AMOUNT;
            base.Use(player);
        }

        /// <summary>
        /// Subprogram to draw information about this <see cref="SpeedPotion"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        /// <param name="iconRectangle">The icon's rectangle</param>
        public override void DrawInformation(SpriteBatch spriteBatch, Rectangle iconRectangle)
        {
            // Calling base, adjusting rectangle, and drawing information
            base.DrawInformation(spriteBatch, iconRectangle);
            iconRectangle.X -= 2 * iconRectangle.Width / 3;
            iconRectangle.Y -= 5 * iconRectangle.Height / 2;
            iconRectangle.Width *= 3;
            iconRectangle.Height = 2 * iconRectangle.Height;
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"Boost Amount: 50%", iconRectangle.Location.ToVector2() + cornerBuffer + 2 * verticalBuffer, Color.Black);
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"Boost Time: {TIME_AMOUNT}", iconRectangle.Location.ToVector2() + cornerBuffer + 3 * verticalBuffer, Color.Black);
        }
    }
}
