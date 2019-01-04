// Author: Joon Song
// File Name: RobeShirt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold RobeShirt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class RobeShirt : Torso
    {
        // RobeShirt specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 2;
        private const int DEFENSE_MAX = 4;

        /// <summary>
        /// Static constructor to setup various <see cref="RobeShirt"/> components
        /// </summary>
        static RobeShirt()
        {
            // Loading in various robe shirt images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Torso/RobeShirt/", "robeShirt");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/robeShirtIcon");
        }

        /// <summary>
        /// Constructor for <see cref="RobeShirt"/> object
        /// </summary>
        public RobeShirt()
        {
            // Setting up object attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
