// Author: Joon Song
// File Name: DirectionalSpriteSheet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/09/2019
// Modified Date: 01/09/2019
// Description: Class to hold DirectionalSpriteSheet object

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class DirectionalSpriteSheet
    {
        // The directional, animational images
        private Dictionary<Direction, AnimationSpriteSheet> directionalImages;

        /// <summary>
        /// Constructor for <see cref="DirectionalSpriteSheet"/>
        /// </summary>
        /// <param name="filePath">The file path of the <see cref="DirectionalSpriteSheet"/></param>
        /// <param name="spriteName">The sprite name</param>
        /// <param name="numFrames">The number of frames in the <see cref="DirectionalSpriteSheet"/></param>
        public DirectionalSpriteSheet(string filePath, string spriteName, int numFrames)
        {
            // Loading and setting up directional images
            directionalImages = new Dictionary<Direction, AnimationSpriteSheet>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                directionalImages.Add(direction, new AnimationSpriteSheet($"{filePath}/{direction.ToString()}/{spriteName}{direction.ToString()}", numFrames));
            }
        }

        /// <summary>
        /// Draw subprogarm for <see cref="DirectionalSpriteSheet"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="direction">The direction in which to draw the image</param>
        /// <param name="frameNumber">The frame number of the image</param>
        /// <param name="rectangle">The rectangle to draw the image in</param>
        public void Draw(SpriteBatch spriteBatch, Direction direction, int frameNumber, Rectangle rectangle)
        {
            // Drawing corresponding image
            directionalImages[direction].Draw(spriteBatch, frameNumber, rectangle);
        }
    }
}
