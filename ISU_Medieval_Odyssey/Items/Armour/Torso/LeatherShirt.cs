// Author: Joon Song
// File Name: LeatherShirt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold LeatherShirt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LeatherShirt : Torso
    {
        // LeatherShirt specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 1;
        private const int DEFENSE_MAX = 3;

        /// <summary>
        /// Static constructor to setup various <see cref="LeatherShirt"/> components
        /// </summary>
        static LeatherShirt()
        {
            // Loading in various LeatherShirt images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Torso/LeatherShirt/", "leatherShirt");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/leatherShirtIcon");
        }

        /// <summary>
        /// Constructor for <see cref="LeatherShirt"/> object
        /// </summary>
        public LeatherShirt()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
