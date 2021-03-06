﻿// Author: Joon Song
// File Name: ChainTorso.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold ChainTorso object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ChainTorso : Torso
    {
        // ChainTorso specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 5;
        private const int MAX_DEFENSE = 8;
        private const int MIN_DURABILITY = 25;
        private const int MAX_DURABILITY = 40;

        /// <summary>
        /// Static constructor to setup various <see cref="ChainTorso"/> components
        /// </summary>
        static ChainTorso()
        {
            // Loading in various ChainTorso images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Torso/ChainTorso/", "chainTorso");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/chainTorsoIcon");
        }

        /// <summary>
        /// Constructor for <see cref="ChainTorso"/> object
        /// </summary>
        public ChainTorso()
        {
            // Setting up ChainTorso
            itemName = "Chain Torso";
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
