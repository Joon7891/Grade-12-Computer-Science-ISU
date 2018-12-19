// Author: Joon Song, Steven Ung
// File Name: Armour.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Armour object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Armour : Item
    {
        protected Dictionary<MovementType, Texture2D[,]> images = new Dictionary<MovementType, Texture2D[,]>();

        protected Texture2D[,] LoadImage(string basePath, MovementType movementType, string armourName, byte width)
        {
            Texture2D[,] loadedImage = new Texture2D[4, width];

            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (int i = 0; i < width; ++i)
                {
                    loadedImage[(int)direction, i] = Main.Content.Load<Texture2D>
                        (basePath + $"{movementType.ToString()}/{direction.ToString()}/{armourName}{movementType.ToString()}{direction.ToString()}{i}");
                }
            }

            return loadedImage;
        }
    }
}
