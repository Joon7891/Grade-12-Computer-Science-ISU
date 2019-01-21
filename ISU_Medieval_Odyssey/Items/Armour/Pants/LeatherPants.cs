// Author: Joon Song
// File Name: LeatherPants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeathePants object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherPants : Pants
    {
        // LeatherPants specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 2;
        private const int MAX_DEFENSE = 4;
        private const int MIN_DURABILITY = 10;
        private const int MAX_DURABILITY = 20;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherPants"/> components
        /// </summary>
        static LeatherPants()
        {
            // Loading in various LeatherPants images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Pants/LeatherPants/", "leatherPants");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherPantsIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherPants"/> object
        /// </summary>
        public LeatherPants()
        {
            // Setting up LeatherPants
            itemName = "Leather Pants";
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
