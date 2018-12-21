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

namespace ISU_Medieval_Odyssey.Item.Armour.Belt
{
    public sealed class RopeBelt : Belt
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various RopeBelt components
        /// </summary>
        static RopeBelt()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Belt/RopeBelt/";
            string armourTypeName = "ropeBelt";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }
    }
}
