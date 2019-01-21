// Author: Joon Song
// File Name: MetalHelmet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold ArmourHelmet object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MetalHelmet : Head
    {
        // MetalHelmet specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 6;
        private const int MAX_DEFENSE = 8;
        private const int MIN_DURABILITY = 30;
        private const int MAX_DURABILITY = 40;

        /// <summary>
        /// Static constructor to setup various <see cref="MetalHelmet"/> components
        /// </summary>
        static MetalHelmet()
        {
            // Loading in various MetalHelmet images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Head/MetalHelmet/", "metalHelmet");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/metalHelmetIcon");
        }

        /// <summary>
        /// Constructor for <see cref="MetalHelmet"/> object
        /// </summary>
        public MetalHelmet()
        {
            // Setting up MetalHelmet
            itemName = "Metal Helmet";
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
