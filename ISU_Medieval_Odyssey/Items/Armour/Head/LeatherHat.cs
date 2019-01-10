// Author: Joon Song
// File Name: LeatherHat.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold LeatherHat object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherHat : Head
    {
        // LeatherHat specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 1;
        private const int MAX_DEFENSE = 2;
        private const int MIN_DURABILITY = 5;
        private const int MAX_DURABILITY = 10;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherHat"/> components
        /// </summary>
        static LeatherHat()
        {
            // Setting up movement images dictionary
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Head/LeatherHat/", "leatherHat");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leahterHatIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherHat"/> object
        /// </summary>
        public LeatherHat()
        {
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            SetArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
