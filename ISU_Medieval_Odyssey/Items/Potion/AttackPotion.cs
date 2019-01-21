// Author: Joon Song
// File Name: AttackPotion.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/15/2019
// Modified Date: 01/15/2019
// Description: Class to hold AttackPotion object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class AttackPotion : Potion
    {
        // AttackPotion specific constants
        private new static Texture2D iconImage;
        private const int TIME_AMOUNT = 20;
        private const int VALUE = 40;
        public const float BOOST_AMOUNT = 0.3f;

        /// <summary>
        /// Static constructor for <see cref="AttackPotion"/> object
        /// </summary>
        static AttackPotion()
        {
            // Importing attack potion image
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/attackPotionIcon");
        }

        /// <summary>
        /// Constructor for <see cref="AttackPotion"/>
        /// </summary>
        public AttackPotion()
        {
            // Setting up attack potion
            itemName = "Attack Potion";
            base.iconImage = iconImage;
            Value = VALUE;
        }

        /// <summary>
        /// Subprogram to use the <see cref="AttackPotion"/> <see cref="Item"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using the <see cref="AttackPotion"/></param>
        public override void Use(Player player)
        {
            // Increasing player's attack boost time and calling base use subprogram
            player.AttackBoostTime += TIME_AMOUNT;
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
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"Boost Amount: 30%", iconRectangle.Location.ToVector2() + cornerBuffer + 2 * verticalBuffer, Color.Black);
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"Boost Time: {TIME_AMOUNT}", iconRectangle.Location.ToVector2() + cornerBuffer + 3 * verticalBuffer, Color.Black);
        }

    }
}
