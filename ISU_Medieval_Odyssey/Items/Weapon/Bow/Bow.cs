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
        private new static Texture2D[,] directionalImages;
        private static Texture2D[,] arrowBowImages;

        static Bow()
        {
            string basePath = "Images/Sprites/Weapon/Shoot/";
            string weaponTypeName = "bow";
            directionalImages = EntityHelper.LoadDirectionalImages($"{basePath}Bow/", weaponTypeName, SharedData.MovementNumFrames[MovementType.Shoot]);
            arrowBowImages = EntityHelper.LoadDirectionalImages($"{basePath}Arrow/", "arrow", SharedData.MovementNumFrames[MovementType.Shoot]);
        }

        public Bow()
        {
            base.directionalImages = directionalImages;
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, Direction direction, int currentFrame)
        {
            base.Draw(spriteBatch, playerRectangle, direction, currentFrame);
            spriteBatch.Draw(arrowBowImages[(int)direction, currentFrame], playerRectangle, Color.White);

        }
    }
}