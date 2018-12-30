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

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 2;
        private const int DEFENSE_MAX = 4;

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
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
