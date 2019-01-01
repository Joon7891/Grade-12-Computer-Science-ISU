// Author: Joon Song
// File Name: ChainHelmet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold RobeHood object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RobeHood : Head
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 2;
        private const int DEFENSE_MAX = 4;

        /// <summary>
        /// Static constructor to setup various <see cref="RobeHood"/> components
        /// </summary>
        static RobeHood()
        {
            // Setting up movement images dictionary
            string basePath = "Images/Sprites/Armour/Head/RobeHood/";
            string armourTypeName = "robeHood";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        /// <summary>
        /// Constructor for <see cref="RobeHood"/> object
        /// </summary>
        public RobeHood()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
