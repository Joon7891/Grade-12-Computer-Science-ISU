// Author: Joon Song
// File Name: LeatherShoulders.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeatherShoulders object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherShoulders : Shoulders
    {
        // LeatherShoulders specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 1;
        private const int MAX_DEFENSE = 2;
        private const int MIN_DURABILITY = 5;
        private const int MAX_DURABILITY = 10;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherShoulders"/> components
        /// </summary>
        static LeatherShoulders()
        {
            // Loading in various LeatherShoulders images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Shoulders/LeatherShoulders/", "leatherShoulders");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherShouldersIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherShoulders"/> object
        /// </summary>
        public LeatherShoulders()
        {
            // Setting up LeatherShoulders
            itemName = "Leather Shoulders";
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
