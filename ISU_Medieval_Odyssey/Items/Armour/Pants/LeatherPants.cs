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
        private const int DEFENCE_MIN = 2;
        private const int DEFENSE_MAX = 4;

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
        public LeatherPants()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
