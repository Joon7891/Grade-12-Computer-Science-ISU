// Author: Joon Song
// File Name: LeatherTorso.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeatherTorso object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherTorso : Torso
    {
        // LeatherTorso specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 2;
        private const int MAX_DEFENSE = 3;
        private const int MIN_DURABILITY = 10;
        private const int MAX_DURABILITY = 15;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherTorso"/> components
        /// </summary>
        static LeatherTorso()
        {
            // Loading in various LeatherTorso images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Torso/LeatherTorso/", "leatherTorso");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherTorsoIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherTorso"/> object
        /// </summary>
        public LeatherTorso()
        {
            // Setting up LeatherTorso
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
