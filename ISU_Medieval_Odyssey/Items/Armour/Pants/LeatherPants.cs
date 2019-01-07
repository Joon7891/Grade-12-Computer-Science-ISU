// Author: Joon Song
// File Name: LeatherPants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeathePants object

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherPants : Pants
    {
        // LeatherPants specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int MIN_DEFENSE = 2;
        private const int MAX_DEFENSE = 4;
        private const int MIN_DURABILITY = 10;
        private const int MAX_DURABILITY = 20;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherPants"/> components
        /// </summary>
        static LeatherPants()
        {
            // Loading in various LeatherPants images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Pants/LeatherPants/", "leatherPants");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherPantsIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherPants"/> object
        /// </summary>
        public LeatherPants() : base(MIN_DEFENSE, MAX_DEFENSE, MIN_DURABILITY, MAX_DURABILITY, movementImages, iconImage) { }
    }
}
