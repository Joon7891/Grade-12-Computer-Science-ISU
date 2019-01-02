// Author: Joon Song
// File Name: Sword.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Sword object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Sword : SlashWeapon
    {
        private new static Texture2D[,] directionalImages;

        static Sword()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Weapon/Slash/Sword/";
            string weaponTypeName = "sword";
            directionalImages = EntityHelper.LoadDirectionalImages(basePath, weaponTypeName, SharedData.MovementNumFrames[MovementType.Slash]);
        }

        public Sword()
        {
            base.directionalImages = directionalImages;
        }

        /// <summary>
        /// Draw subprogram for <see cref="Sword"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="playerRectangle"></param>
        /// <param name="direction"></param>
        /// <param name="currentFrame"></param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, Direction direction, int currentFrame)
        {
            adjustedRectangle.X = playerRectangle.X - playerRectangle.Width;
            adjustedRectangle.Y = playerRectangle.Y - playerRectangle.Height;
            spriteBatch.Draw(directionalImages[(int)direction, currentFrame], adjustedRectangle, Color.White);
        }
    }
}
