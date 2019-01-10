// Author: Joon Song
// File Name: ChainHood.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold ChainHood object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ChainHood : Head
    {
        // ChainHood specific images
        private new static MovementSpriteSheet movementSpriteSheet;
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 4;
        private const int MAX_DEFENSE = 6;
        private const int MIN_DURABILITY = 20;
        private const int MAX_DURABILITY = 30;

        /// <summary>
        /// Static constructor to setup various <see cref="ChainHood"/> components
        /// </summary>
        static ChainHood()
        {
            // Loading in various ChainHood images
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Armour/Head/ChainHood/", "chainHood");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/chainHoodIcon");
        }

        /// <summary>
        /// Constructor for <see cref="ChainHood"/> object
        /// </summary>
        public ChainHood()
        {
            base.iconImage = iconImage;
            base.movementSpriteSheet = movementSpriteSheet;
            SetArmourStatistics(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY);
        }
    }
}
