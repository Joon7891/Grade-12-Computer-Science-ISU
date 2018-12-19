// Author: Joon Song, Steven Ung
// File Name: Armour.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Armour object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Armour : Item
    {
        protected Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Subprogram to load in a set of Movement Images
        /// </summary>
        /// <param name="basePath">The base file path</param>
        /// <param name="movementType">The type of movement</param>
        /// <param name="armourName">The name of the armour</param>
        /// <param name="numFrames">The number of frames</param>
        /// <returns>The images for the MovementType</returns>
        protected static Texture2D[,] LoadMovementImages(string basePath, MovementType movementType, string armourName, byte numFrames)
        {
            // Initializing 2D array to hold loaded images
            Texture2D[,] loadedImages = new Texture2D[4, numFrames];

            // Loading in images for each direction and frame
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (int i = 0; i < numFrames; ++i)
                {
                    loadedImages[(int)direction, i] = Main.Content.Load<Texture2D>
                        (basePath + $"{movementType.ToString()}/{direction.ToString()}/{armourName}{movementType.ToString()}{direction.ToString()}{i}");
                }
            }

            // Returning loaded images
            return loadedImages;
        }
    }
}
