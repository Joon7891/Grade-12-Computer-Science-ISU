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
using ISU_Medieval_Odyssey.Helpers;
using ISU_Medieval_Odyssey.Data_Structures;

namespace ISU_Medieval_Odyssey
{
    public sealed class ChainHelmet : Helmet
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        
        /// <summary>
        /// Static constructor to setup various ChainHelmet components
        /// </summary>
        static ChainHelmet()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Helmet/ChainHelmet/";
            string armourTypeName = "chainHelmet";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }
    }
}
