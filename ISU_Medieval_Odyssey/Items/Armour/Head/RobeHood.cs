﻿// Author: Joon Song
// File Name: ChainHelmet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold RobeHood object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RobeHood : Head
    {
        // RopeHood specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 2;
        private const int MAX_DEFENSE = 4;
        private const int MIN_DURABILITY = 10;
        private const int MAX_DURABILITY = 20;

        /// <summary>
        /// Static constructor to setup various <see cref="RobeHood"/> components
        /// </summary>
        static RobeHood()
        {
            // Loading in various RobeHood images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Head/RobeHood/", "robeHood");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/robeHoodIcon");
        }

        /// <summary>
        /// Constructor for <see cref="RobeHood"/> object
        /// </summary>
        public RobeHood()
        {
            // Setting up RobeHood
            itemName = "Robe Hood";
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
