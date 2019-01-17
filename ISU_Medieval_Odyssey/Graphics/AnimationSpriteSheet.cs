// Author: Joon Song
// File Name: AnimationSpriteSheet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/09/2019
// Modified Date: 01/09/2019
// Description: Class to hold AnimationSpriteSheet object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public struct AnimationSpriteSheet
    {
        /// <summary>
        /// The number of frames in the <see cref="AnimationSpriteSheet"/>
        /// </summary>
        public int NumFrames { get; }

        // The animation images
        private Texture2D[] animationImages;

        /// <summary>
        /// Constructor for <see cref="AnimationSpriteSheet"/>
        /// </summary>
        /// <param name="filePath">File path for <see cref="AnimationSpriteSheet"/></param>
        /// <param name="numFrames">The number of frames in the <see cref="AnimationSpriteSheet"/></param>
        public AnimationSpriteSheet(string filePath, int numFrames)
        {
            // Loading and setting up animation images
            NumFrames = numFrames;
            animationImages = new Texture2D[numFrames];
            for (int i = 0; i < numFrames; ++i)
            {
                animationImages[i] = Main.Content.Load<Texture2D>($"{filePath}{i}");
            }
        }

        /// <summary>
        /// Draw subprogram for <see cref="AnimationSpriteSheet"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="frameNumber">The frame number of the image</param>
        /// <param name="rectangle">The rectangle to draw the image in</param>
        public void Draw(SpriteBatch spriteBatch, int frameNumber, Rectangle rectangle)
        {
            // Drawing correspding image
            spriteBatch.Draw(animationImages[frameNumber], rectangle, Color.White);
        }
    }
}
