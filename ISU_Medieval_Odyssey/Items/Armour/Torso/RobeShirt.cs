// Author: Joon Song
// File Name: RobeShirt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold RobeShirt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RobeShirt : Torso
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various RobeShirt components
        /// </summary>
        static RobeShirt()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Torso/RobeShirt/";
            string armourTypeName = "robeShirt";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public RobeShirt()
        {
            base.movementImages = movementImages;
        }
    }
}
