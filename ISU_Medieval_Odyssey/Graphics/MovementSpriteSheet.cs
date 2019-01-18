// Author: Joon Song
// File Name: MovementSpriteSheet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/09/2019
// Modified Date: 01/19/2019
// Description: Class to hold MovementSpriteSheet object

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public struct MovementSpriteSheet
    {
        // The directional images for each movement type and the number of frames per movement 
        private static Dictionary<MovementType, int> movementNumFrames;
        private Dictionary<MovementType, DirectionalSpriteSheet> movementImages;

        /// <summary>
        /// Static constructor for <see cref="MovementSpriteSheet"/> struct
        /// </summary>
        static MovementSpriteSheet()
        {
            // Setting up movement type frame number dictionary
            movementNumFrames = new Dictionary<MovementType, int>();
            movementNumFrames.Add(MovementType.Walk, Player.NUM_WALK_FRAMES);
            movementNumFrames.Add(MovementType.Slash, SlashWeapon.NUM_FRAMES);
            movementNumFrames.Add(MovementType.Shoot, Bow.NUM_FRAMES);
            movementNumFrames.Add(MovementType.Thrust, ThrustWeapon.NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="MovementSpriteSheet"/>
        /// </summary>
        /// <param name="filePath">The file path of the <see cref="MovementSpriteSheet"/></param>
        /// <param name="spriteName">The name of the sprite</param>
        public MovementSpriteSheet(string filePath, string spriteName)
        {
            // Loading and setting up graphics for movement sprite sheet 
            movementImages = new Dictionary<MovementType, DirectionalSpriteSheet>();
            for (MovementType movementType = MovementType.Walk; movementType <= MovementType.Thrust; ++movementType)
            {
                movementImages.Add(movementType, new DirectionalSpriteSheet($"{filePath}/{movementType.ToString()}",
                    $"{spriteName}{movementType.ToString()}", movementNumFrames[movementType]));
            }
        }

        /// <summary>
        /// Draw subprogram for <see cref="MovementSpriteSheet"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="movementType">The <see cref="MovementType"/> of the image to be drawn</param>
        /// <param name="direction">The <see cref="Direction"/> in which to draw the image</param>
        /// <param name="frameNumber">The frame number of the image</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> to draw the image in</param>
        public void Draw(SpriteBatch spriteBatch, MovementType movementType, Direction direction, int frameNumber, Rectangle rectangle)
        {
            // Drawing corresponding image
            movementImages[movementType].Draw(spriteBatch, direction, frameNumber, rectangle);
        }
    }
}
