// Author: Joon Song
// File Name: MetalShoulders.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold MetalShoulders object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MetalShoulders : Shoulders
    {
        // MetalShoulders specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 2;
        private const int MAX_DEFENSE = 4;
        private const int MIN_DURABILITY = 15;
        private const int MAX_DURABILITY = 20;

        /// <summary>
        /// Static constructor to setup various <see cref="MetalShoulders"/> components
        /// </summary>
        static MetalShoulders()
        {
            // Loading in various MetalShoulders images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Shoulders/MetalShoulders/", "metalShoulders");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/metalShouldersIcon");
        }

        /// <summary>
        /// Constructor for <see cref="MetalShoulders"/> object
        /// </summary>
        public MetalShoulders()
        {
            // Setting up MetalShoulders
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            SetArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
