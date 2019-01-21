// Author: Joon Song, Steven Ung
// File Name: LeatherShoes.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold LeatherShoes object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherShoes : Shoes
    {
        // LeatherShoes specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 1;
        private const int MAX_DEFENSE = 2;
        private const int MIN_DURABILITY = 5;
        private const int MAX_DURABILITY = 10;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherShoes"/> components
        /// </summary>
        static LeatherShoes()
        {
            // Loading in various LeatherShoes images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Shoes/LeatherShoes/", "leatherShoes");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherShoesIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherShoes"/> object
        /// </summary>
        public LeatherShoes()
        {
            // Setting up leather shoes
            itemName = "Leather Shoes";
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
