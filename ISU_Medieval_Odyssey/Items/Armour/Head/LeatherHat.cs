// Author: Joon Song
// File Name: LeatherHat.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold LeatherHat object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherHat : Head
    {
        // LeatherHat specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 1;
        private const int DEFENSE_MAX = 2;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherHat"/> components
        /// </summary>
        static LeatherHat()
        {
            // Setting up movement images dictionary
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Head/LeatherHat/", "leatherHat");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leahterHatIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherHat"/> object
        /// </summary>
        public LeatherHat()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
