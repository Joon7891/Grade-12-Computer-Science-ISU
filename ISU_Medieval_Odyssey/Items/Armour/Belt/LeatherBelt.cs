// Author: Joon Song, Steven Ung
// File Name: LeatherBelt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold LeatherBelt object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISU_Medieval_Odyssey.Helpers;
using Microsoft.Xna.Framework.Graphics;
using ISU_Medieval_Odyssey.Data_Structures;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherBelt : Belt
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various LeatherBelt components
        /// </summary>
        static LeatherBelt()
        {
            // Setting up movement images dictionary
            string basePath = "Images/Sprites/Armour/Belt/LeatherBelt/";
            string armourTypeName = "leatherBelt";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }
    }
}
