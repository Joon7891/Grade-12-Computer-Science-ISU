// Author: Joon Song, Steven Ung
// File Name: ArmourShoes.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold ArmourShoes object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ArmourShoes : Shoes
    {
        private new static Dictionary<MovementType, Texture2D[,]> images = new Dictionary<MovementType, Texture2D[,]>();

        /// <summary>
        /// Static constructor to set up various ArmourShoes components
        /// </summary>
        static ArmourShoes()
        {
            // Array of directional images for each type of movement
            string basePath = "Images/Sprites/Armour/Shoes/ArmourShoes/";
            Texture2D[,] walkImages = new Texture2D[4, 9];
            Texture2D[,] slashImages = new Texture2D[4, 6];
            Texture2D[,] shootImages = new Texture2D[4, 13];
            Texture2D[,] thrustImages = new Texture2D[4, 8];

            // Loading in walk images
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (byte i = 0; i < walkImages.GetLength(1); ++i)
                {
                    walkImages[(int)direction, i] = Main.Content.Load<Texture2D>(basePath + $"Walk/{direction.ToString()}/armourShoesWalk{direction.ToString()}{i}");
                }
            }
            images.Add(MovementType.Walk, walkImages);

            // Loading in slash images
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                for (byte i = 0; i < slashImages.GetLength(1); ++i)
                {
                    slashImages[(int)direction, i] = Main.Content.Load<Texture2D>(basePath + $"Slash/{direction.ToString()}/armourShoesSlash{direction.ToString()}{i}");
                }
            }
            images.Add(MovementType.Slash, slashImages);
        }
    }
}
