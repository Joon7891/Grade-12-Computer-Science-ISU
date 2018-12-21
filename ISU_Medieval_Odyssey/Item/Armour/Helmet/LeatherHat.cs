// Author: Joon Song
// File Name: LeatherHat.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold LeatherHat object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey.Item.Armour.Helmet
{
    public sealed class LeatherHat : Helmet
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various LeatherHat components
        /// </summary>
        static LeatherHat()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Helmet/LeatherHat/";
            string armourTypeName = "leatherHat";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public LeatherHat()
        {
            base.movementImages = movementImages;
        }
    }
}
