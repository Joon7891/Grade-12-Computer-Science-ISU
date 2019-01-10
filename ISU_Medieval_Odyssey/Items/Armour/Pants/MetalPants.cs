// Author: Joon Song
// File Name: MetalPants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold MetalPants object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MetalPants : Pants
    {
        // MetalPants specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 4;
        private const int MAX_DEFENSE = 6;
        private const int MIN_DURABILITY = 20;
        private const int MAX_DURABILITY = 30;

        /// <summary>
        /// Static constructor to setup various <see cref="MetalPants"/> components
        /// </summary>
        static MetalPants()
        {
            // Loading in various MetalPants images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Pants/MetalPants/", "metalPants");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/metalPantsIcon");
        }

        /// <summary>
        /// Constructor for <see cref="MetalPants"/> object
        /// </summary>
        public MetalPants()
        {
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            SetArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
