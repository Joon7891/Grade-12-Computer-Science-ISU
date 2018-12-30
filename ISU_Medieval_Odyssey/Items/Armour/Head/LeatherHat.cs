// Author: Joon Song
// File Name: LeatherHat.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold LeatherHat object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherHat : Head
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 1;
        private const int DEFENSE_MAX = 2;

        /// <summary>
        /// Static constructor to setup various LeatherHat components
        /// </summary>
        static LeatherHat()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Head/LeatherHat/";
            string armourTypeName = "leatherHat";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public LeatherHat()
        {
            base.movementImages = movementImages;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
