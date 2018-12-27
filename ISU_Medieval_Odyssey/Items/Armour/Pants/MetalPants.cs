// Author: Joon Song
// File Name: MetalPants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold MetalPants object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MetalPants : Pants
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various MetalPants components
        /// </summary>
        static MetalPants()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Pants/MetalPants/";
            string armourTypeName = "metalPants";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public MetalPants()
        {
            base.movementImages = movementImages;
        }
    }
}
