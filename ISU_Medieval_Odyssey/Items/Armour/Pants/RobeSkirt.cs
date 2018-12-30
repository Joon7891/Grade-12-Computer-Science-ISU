// Author: Joon Song
// File Name: RobeSkirt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold RobeSkirt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RobeSkirt : Pants
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 3;
        private const int DEFENSE_MAX = 5;

        /// <summary>
        /// Static constructor to setup various RobeSkirt components
        /// </summary>
        static RobeSkirt()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Pants/RobeSkirt/";
            string armourTypeName = "robeSkirt";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public RobeSkirt()
        {
            base.movementImages = movementImages;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
