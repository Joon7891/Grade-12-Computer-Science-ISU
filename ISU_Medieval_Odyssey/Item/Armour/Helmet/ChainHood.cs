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

namespace ISU_Medieval_Odyssey.Item.Armour.Helmet
{
    public sealed class ChainHood : Helmet
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various ChainHood components
        /// </summary>
        static ChainHood()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Helmet/ChainHood/";
            string armourTypeName = "chainHood";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }
    }
}
