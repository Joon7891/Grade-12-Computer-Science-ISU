// Author: Joon Song
// File Name: RobeSkirt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold RobeSkirt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RobeSkirt : Pants
    {
        // RobeSkirt specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 3;
        private const int MAX_DEFENSE = 5;
        private const int MIN_DURABILITY = 15;
        private const int MAX_DURABILITY = 25;

        /// <summary>
        /// Static constructor to setup various <see cref="RobeSkirt"/> components
        /// </summary>
        static RobeSkirt()
        {
            // Loading in various RobeSkirt images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Pants/RobeSkirt/", "robeSkirt");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/robeSkirtIcon");
        }

        /// <summary>
        /// Constructor for <see cref="RobeSkirt"/> object
        /// </summary>
        public RobeSkirt()
        {
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            SetArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
