﻿// Author: Joon Song
// File Name: ChainTorso.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold ChainTorso object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ChainTorso : Torso
    {
        // ChainTorso specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 5;
        private const int DEFENSE_MAX = 8;

        /// <summary>
        /// Static constructor to setup various <see cref="ChainTorso"/> components
        /// </summary>
        static ChainTorso()
        {
            // Loading in various ChainTorso images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Torso/ChainTorso/", "chainTorso");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/chainTorsoIcon");
        }

        /// <summary>
        /// Constructor for <see cref="ChainTorso"/> object
        /// </summary>
        public ChainTorso()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
