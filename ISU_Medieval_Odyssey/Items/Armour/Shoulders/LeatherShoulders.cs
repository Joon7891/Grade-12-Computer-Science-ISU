// Author: Joon Song
// File Name: LeatherShoulders.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeatherShoulders object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherShoulders : Shoulders
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various MetalTorso components
        /// </summary>
        static LeatherShoulders()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Shoulders/LeatherShoulders/";
            string armourTypeName = "leatherShoulders";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public LeatherShoulders()
        {
            base.movementImages = movementImages;
        }
    }
}
