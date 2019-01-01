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
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 4;
        private const int DEFENSE_MAX = 6;

        /// <summary>
        /// Static constructor to setup various <see cref="ChainHood"/> components
        /// </summary>
        static ChainHood()
        {
            // Setting up movement images dictionary
            string basePath = "Images/Sprites/Armour/Head/ChainHood/";
            string armourTypeName = "chainHood";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        /// <summary>
        /// Constructor for <see cref="ChainHood"/> object
        /// </summary>
        public ChainHood()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
