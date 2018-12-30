// Author: Joon Song
// File Name: LeatherPants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeathePants object

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherPants : Pants
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 2;
        private const int DEFENSE_MAX = 4;

        /// <summary>
        /// Static constructor to setup various LeatherPants components
        /// </summary>
        static LeatherPants()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Pants/LeatherPants/";
            string armourTypeName = "leatherPants";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        public LeatherPants()
        {
            base.movementImages = movementImages;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
