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

        /// <summary>
        /// Static constructor to setup various RobeHood components
        /// </summary>
        static RobeHood()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Head/RobeHood/";
            string armourTypeName = "robeHood";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public RobeHood()
        {
            base.movementImages = movementImages;
        }
    }
}
