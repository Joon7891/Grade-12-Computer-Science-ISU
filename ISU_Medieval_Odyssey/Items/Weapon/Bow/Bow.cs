// Author: Joon Song
// File Name: Bow.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Bow object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Bow : Weapon
    {
        public const int NUM_FRAMES = 13;

        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private static DirectionalSpriteSheet arrowSpriteSheet;

        static Bow()
        {
            string basePath = "Images/Sprites/Weapon/Shoot/";
            string weaponTypeName = "bow";
            directionalSpriteSheet = new DirectionalSpriteSheet($"{basePath}Bow/", weaponTypeName, NUM_FRAMES);
            directionalSpriteSheet = new DirectionalSpriteSheet($"{basePath}Arrow/", "arrow", NUM_FRAMES);
        }

        public Bow()
        {
            base.directionalSpriteSheet = directionalSpriteSheet;
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, Direction direction, int currentFrame)
        {
            base.Draw(spriteBatch, playerRectangle, direction, currentFrame);
            arrowSpriteSheet.Draw(spriteBatch, direction, currentFrame, playerRectangle);
        }
    }
}