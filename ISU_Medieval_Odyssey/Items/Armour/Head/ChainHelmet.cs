// Author: Joon Song
// File Name: ChainHelmet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold ChainHelmet object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ChainHelmet : Head
    {
        // ChainHelmet specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 5;
        private const int MAX_DEFENSE = 7;
        private const int MIN_DURABILITY = 25;
        private const int MAX_DURABILITY = 35;

        /// <summary>
        /// Static constructor to setup various <see cref="ChainHelmet"/> components
        /// </summary>
        static ChainHelmet()
        {
            // Loading in various ChainHelmet images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Head/ChainHelmet/", "chainHelmet");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/chainHelmetIcon");
        }

        /// <summary>
        /// Constructor for <see cref="ChainHelmet"/> object
        /// </summary>
        public ChainHelmet()
        {
            // Setting up ChainHelmet
            itemName = "Chain Helmet";
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
