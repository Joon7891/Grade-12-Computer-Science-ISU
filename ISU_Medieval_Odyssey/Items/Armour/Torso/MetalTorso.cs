﻿// Author: Joon Song
// File Name: MetalTorso.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold MetalTorso object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MetalTorso : Torso
    {
        // MetalTorso specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 6;
        private const int DEFENSE_MAX = 10;

        /// <summary>
        /// Static constructor to setup various <see cref="MetalTorso"/> components
        /// </summary>
        static MetalTorso()
        {
            // Loading in various MetalTorso images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Torso/MetalTorso/", "metalTorso");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/metalTorsoIcon");
        }

        /// <summary>
        /// Constructor for <see cref="MetalTorso"/> object
        /// </summary>
        public MetalTorso()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
