// Author: Joon Song
// File Name: ChainHelmet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold ChainHelmet object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ChainHelmet : Head
    {
        // ChainHelmet specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 5;
        private const int DEFENSE_MAX = 7;

        /// <summary>
        /// Static constructor to setup various <see cref="ChainHelmet"/> components
        /// </summary>
        static ChainHelmet()
        {
            // Loading in various ChainHelmet images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Head/ChainHelmet/", "chainHelmet");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/chainHelmetIcon");
        }

        /// <summary>
        /// Constructor for <see cref="ChainHelmet"/> object
        /// </summary>
        public ChainHelmet()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
