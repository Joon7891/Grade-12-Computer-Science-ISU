// Author: Joon Song
// File Name: DirectionalSpriteSheet.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/09/2019
// Modified Date: 01/09/2019
// Description: Class to hold DirectionalSpriteSheet object

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public struct MovementSpriteSheet
    {
        private static Dictionary<MovementType, int> movementNumFrames;
        private Dictionary<MovementType, DirectionalSpriteSheet> movementImages;

        static MovementSpriteSheet()
        {
            movementNumFrames = new Dictionary<MovementType, int>();
            movementNumFrames.Add(MovementType.Walk, Player.NUM_WALK_FRAMES);
            movementNumFrames.Add(MovementType.Slash, SlashWeapon.NUM_FRAMES);
            movementNumFrames.Add(MovementType.Shoot, Bow.NUM_FRAMES);
            movementNumFrames.Add(MovementType.Thrust, ThrustWeapon.NUM_FRAMES);
        }

        public MovementSpriteSheet(string filePath, string imageName)
        {
            movementImages = new Dictionary<MovementType, DirectionalSpriteSheet>();
            for (MovementType movementType = MovementType.Walk; movementType <= MovementType.Thrust; ++movementType)
            {
                movementImages.Add(movementType, new DirectionalSpriteSheet($"{filePath}/{movementType.ToString()}",
                    $"{imageName}{movementType.ToString()}", movementNumFrames[movementType]));
            }
        }

        public void Draw(SpriteBatch spriteBatch, MovementType movementType, Direction direction, int frameNumber, Rectangle rectangle)
        {
            movementImages[movementType].Draw(spriteBatch, direction, frameNumber, rectangle);
        }
    }
}
