// Author: Joon Song, Steven Ung
// File Name: LeatherShoes.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold LeatherShoes object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherShoes : Shoes
    {
        // LeatherShoes specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 1;
        private const int DEFENSE_MAX = 2;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherShoes"/> components
        /// </summary>
        static LeatherShoes()
        {
            // Loading in various LeatherShoes images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Shoes/LeatherShoes/", "leatherShoes");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherShoesIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherShoes"/> object
        /// </summary>
        public LeatherShoes()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
