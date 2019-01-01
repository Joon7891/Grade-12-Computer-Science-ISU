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

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 1;
        private const int DEFENSE_MAX = 2;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherShoulders"/> components
        /// </summary>
        static LeatherShoulders()
        {
            // Setting up movement images dictionary
            string basePath = "Images/Sprites/Armour/Shoulders/LeatherShoulders/";
            string armourTypeName = "leatherShoulders";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        /// <summary>
        /// Constructor for <see cref="LeatherShoulders"/> object
        /// </summary>
        public LeatherShoulders()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
