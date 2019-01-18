// Author: Joon Song, Steven Ung
// File Name: RopeBelt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold RopeBelt object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RopeBelt : Belt
    {
        // RopeBelt specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 3;
        private const int MAX_DEFENSE = 4;
        private const int MIN_DURABILITY = 15;
        private const int MAX_DURABILITY = 20;

        /// <summary>
        /// Static constructor to setup various <see cref="RopeBelt"/> components
        /// </summary>
        static RopeBelt()
        {
            // Loading in various RopeBelt images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Belt/RopeBelt/", "ropeBelt");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/ropeBeltIcon");
        }

        /// <summary>
        /// Constructor for <see cref="RopeBelt"/> object
        /// </summary>
        public RopeBelt()
        {
            // Setting up RopeBelt
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            InitializeArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
