// Author: Joon Song
// File Name: Hair.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold Hair object - default for no helmet

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Hair : Head
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various <see cref="Hair"/> components
        /// </summary>
        static Hair()
        {
            // Setting up movement images dictionary
            string basePath = "Images/Sprites/Armour/Head/Hair/";
            string armourTypeName = "hair";
            movementImages = EntityHelper.LoadMovementImages(basePath, armourTypeName);
        }

        /// <summary>
        /// Constructor for <see cref="Hair"/> object
        /// </summary>
        public Hair()
        {
            // Setting up "armour" attributes and images
            base.movementImages = movementImages;
            defence = 0;
            durability = int.MaxValue;
        }
    }
}
