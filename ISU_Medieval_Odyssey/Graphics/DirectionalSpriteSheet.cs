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
        public Dictionary<Direction, AnimationSpriteSheet> directionalImages;

        public DirectionalSpriteSheet(string filePath, string spriteName, int numFrames)
        {
            directionalImages = new Dictionary<Direction, AnimationSpriteSheet>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                directionalImages.Add(direction, new AnimationSpriteSheet($"{filePath}/{direction.ToString()}/{spriteName}{direction.ToString()}", numFrames));
            }
        }

        public void Draw(SpriteBatch spriteBatch, Direction direction, int frameNumber, Rectangle rectangle)
        {
            directionalImages[direction].Draw(spriteBatch, frameNumber, rectangle);
        }
    }
}
