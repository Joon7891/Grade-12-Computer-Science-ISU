// Author: Joon Song, Steven Ung
// File Name: LeatherBelt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold LeatherBelt object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherBelt : Belt
    {
        // Dictionary to map MovementTypes to the appropriate images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to setup various LeatherBelt components
        /// </summary>
        static LeatherBelt()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Armour/Belt/LeatherBelt/";
            string armourName = "leatherBelt";

            // Loading in movement images for each Movement Type
            movementImages.Add(MovementType.Walk, SharedData.LoadMovementImages(basePath, MovementType.Walk, armourName, SharedData.NUM_WALK_FRAMES));
            movementImages.Add(MovementType.Slash, SharedData.LoadMovementImages(basePath, MovementType.Slash, armourName, SharedData.NUM_SLASH_FRAMES));
            movementImages.Add(MovementType.Shoot, SharedData.LoadMovementImages(basePath, MovementType.Shoot, armourName, SharedData.NUM_SHOOT_FRAMES));
            movementImages.Add(MovementType.Thrust, SharedData.LoadMovementImages(basePath, MovementType.Thrust, armourName, SharedData.NUM_THRUST_FRAMES));
        }
    }
}
