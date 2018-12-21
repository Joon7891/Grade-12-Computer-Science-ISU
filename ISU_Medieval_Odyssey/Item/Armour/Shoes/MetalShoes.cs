// Author: Joon Song, Steven Ung
// File Name: MetalShoes.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold MetalShoes object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ISU_Medieval_Odyssey.Utility;

namespace ISU_Medieval_Odyssey.Item.Armour.Shoes
{
    public sealed class MetalShoes : Shoes
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to set up various MetalShoes components
        /// </summary>
        static MetalShoes()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Shoes/MetalShoes/";
            string armourTypeName = "metalShoes";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }
    }
}
