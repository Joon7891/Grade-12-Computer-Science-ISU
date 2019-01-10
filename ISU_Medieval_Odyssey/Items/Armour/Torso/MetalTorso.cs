// Author: Joon Song
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
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 6;
        private const int MAX_DEFENSE = 10;
        private const int MIN_DURABILITY = 30;
        private const int MAX_DURABILITY = 50;

        /// <summary>
        /// Static constructor to setup various <see cref="MetalTorso"/> components
        /// </summary>
        static MetalTorso()
        {
            // Loading in various MetalTorso images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Torso/MetalTorso/", "metalTorso");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/metalTorsoIcon");
        }

        /// <summary>
        /// Constructor for <see cref="MetalTorso"/> object
        /// </summary>
        public MetalTorso()
        {
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            SetArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
