// Author: Joon Song
// File Name: RobeSkirt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold RobeSkirt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RobeSkirt : Pants
    {
        // RobeSkirt specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 3;
        private const int DEFENSE_MAX = 5;

        /// <summary>
        /// Static constructor to setup various <see cref="RobeSkirt"/> components
        /// </summary>
        static RobeSkirt()
        {
            // Loading in various RobeSkirt images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Pants/RobeSkirt/", "robeSkirt");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/robeSkirtIcon");
        }

        /// <summary>
        /// Constructor for <see cref="RobeSkirt"/> object
        /// </summary>
        public RobeSkirt()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
