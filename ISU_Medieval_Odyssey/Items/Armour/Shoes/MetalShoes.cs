// Author: Joon Song, Steven Ung
// File Name: MetalShoes.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold MetalShoes object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MetalShoes : Shoes
    {
        // MetalShoes specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 3;
        private const int DEFENSE_MAX = 4;

        /// <summary>
        /// Static constructor to set up various <see cref="MetalShoes"/> components
        /// </summary>
        static MetalShoes()
        {
            // Loading in various MetalShoes images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Shoes/MetalShoes/", "metalShoes");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/metalShoesIcon");
        }

        /// <summary>
        /// Constructor for <see cref="MetalShoes"/> object
        /// </summary>
        public MetalShoes()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
