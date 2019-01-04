// Author: Joon Song
// File Name: LeatherShoulders.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeatherShoulders object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherShoulders : Shoulders
    {
        // LeatherShoulders specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 1;
        private const int DEFENSE_MAX = 2;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherShoulders"/> components
        /// </summary>
        static LeatherShoulders()
        {
            // Loading in various LeatherShoulders images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Shoulders/LeatherShoulders/", "leatherShoulders");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherShouldersIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherShoulders"/> object
        /// </summary>
        public LeatherShoulders()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
