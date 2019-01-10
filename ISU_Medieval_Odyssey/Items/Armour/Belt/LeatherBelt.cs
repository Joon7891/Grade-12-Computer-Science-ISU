// Author: Joon Song, Steven Ung
// File Name: LeatherBelt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold LeatherBelt object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherBelt : Belt
    {
        // LeatherBelt specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 1;
        private const int MAX_DEFENSE = 2;
        private const int MIN_DURABILITY = 5;
        private const int MAX_DURABILITY = 10;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherBelt"/> components
        /// </summary>
        static LeatherBelt()
        {
            // Loading in various LeatherBelt images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Belt/LeatherBelt/", "leatherBelt");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherBeltIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherBelt"/> object
        /// </summary>
        public LeatherBelt()
        {
            // Setting up LeatherBelt
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            SetArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
