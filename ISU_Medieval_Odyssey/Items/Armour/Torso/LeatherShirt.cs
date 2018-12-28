// Author: Joon Song
// File Name: LeatherShirt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeatherShirt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherShirt : Torso
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various MetalTorso components
        /// </summary>
        static LeatherShirt()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Torso/LeatherShirt/";
            string armourTypeName = "leatherShirt";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public LeatherShirt()
        {
            base.movementImages = movementImages;
        }
    }
}
