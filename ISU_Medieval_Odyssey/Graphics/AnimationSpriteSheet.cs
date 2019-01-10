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
    public sealed class AnimationSpriteSheet
    {
        public int NumFrames { get; }

        private Texture2D[] animationImages;

        public AnimationSpriteSheet(string filePath, int numFrames)
        {
            NumFrames = numFrames;
            animationImages = new Texture2D[numFrames];
            for (int i = 0; i < numFrames; ++i)
            {
                animationImages[i] = Main.Content.Load<Texture2D>($"{filePath}{i}");
            }
        }

        public void Draw(SpriteBatch spriteBatch, int frameNumber, Rectangle rectangle)
        {
            spriteBatch.Draw(animationImages[frameNumber], rectangle, Color.White);
        }
    }
}
