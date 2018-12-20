// Author: Joon Song
// File Name: EntityHeper.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold various subprograms to help with Entity setup

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public static class EntityHelper
    {
        /// <summary>
        /// Subprogram to load movement images for a given entity
        /// </summary>
        /// <param name="basePath">The base file path</param>
        /// <param name="entityTypeName">The entity type name</param>
        /// <returns>A dictionary mapping movemement types a 2D array of directional images</returns>
        public static Dictionary<MovementType, Texture2D[,]> LoadMovementImages(string basePath, string entityTypeName)
        {
            // Dictionary to hold movement images
            Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

            // Loading directionary images for each movement type
            for (MovementType movementType = MovementType.Walk; movementType <= MovementType.Thrust; ++movementType)
            {
                movementImages.Add(movementType, LoadDirectionalImages(basePath, entityTypeName, movementType));
            }

            // Returning movement images
            return movementImages;
        }
        
        /// <summary>
        /// Subprogram to load a 2D array of directional images
        /// </summary>
        /// <param name="basePath">The base file path</param>
        /// <param name="entityTypeName">The entity type name</param>
        /// <param name="movementType">The movement type</param>
        /// <returns>A 2D array containing the directional images</returns>
        private static Texture2D[,] LoadDirectionalImages(string basePath, string entityTypeName, MovementType movementType)
        {
            // Various variables to help hold and setup loaded images
            byte numFrames = SharedData.MovementNumFrames[movementType];
            Texture2D[,] loadedImages = new Texture2D[Enum.GetValues(typeof(Direction)).Length, numFrames];

            // Loading in images for each direction and frame
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (byte i = 0; i < numFrames; ++i)
                {
                    loadedImages[(byte)direction, i] = Main.Content.Load<Texture2D>
                        (basePath + $"{movementType.ToString()}/{direction.ToString()}/{entityTypeName}{movementType.ToString()}{direction.ToString()}{i}");
                }
            }

            // Returning loaded images
            return loadedImages;
        }
    }
}
