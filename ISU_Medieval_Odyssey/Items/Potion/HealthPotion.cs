// Author: Joon Song
// File Name: HealthPotion.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/04/2018
// Modified Date: 01/04/2018
// Description: Class to hold HealthPotion object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class HealthPotion : Potion
    {
        // HealthPotion icon image
        private new static Texture2D iconImage;

        // Variables related to health increase of the potion
        private const int HEALTH_INCREASE_MIN = 10;
        private const int HEALTH_INCREASE_MAX = 100;
        private int healthIncrease;

        /// <summary>
        /// Static constructor for <see cref="HealthPotion"/>
        /// </summary>
        static HealthPotion()
        {
            // Importing HealthPotion image
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/healthPotionIcon");
        }

        /// <summary>
        /// Constructor for <see cref="HealthPotion"/> object
        /// </summary>
        public HealthPotion()
        {
            // Assigning health potion components
            base.iconImage = iconImage;
            healthIncrease = SharedData.RNG.Next(HEALTH_INCREASE_MIN, HEALTH_INCREASE_MAX + 1);
            Value = healthIncrease;
        }

        /// <summary>
        /// Subprogram to use the <see cref="HealthPotion"/> <see cref="Item"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using the <see cref="HealthPotion"/></param>
        public override void Use(Player player)
        {
            // Incrementing player health and calling base use subprogram
            player.Health += healthIncrease;
            base.Use(player);
        }
    }
}
