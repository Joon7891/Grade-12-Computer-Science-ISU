// Author: Joon Song
// File Name: MetalPants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold MetalPants object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MetalPants : Pants
    {
        // MetalPants specific images
        private new static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private new static Texture2D iconImage;

        // Constants dictating minimum and maximum values of armour attributes
        private const int DEFENCE_MIN = 4;
        private const int DEFENSE_MAX = 6;

        /// <summary>
        /// Static constructor to setup various <see cref="MetalPants"/> components
        /// </summary>
        static MetalPants()
        {
            // Loading in various MetalPants images
            movementImages = EntityHelper.LoadMovementImages("Images/Sprites/Armour/Pants/MetalPants/", "metalPants");
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/metalPantsIcon");
        }

        /// <summary>
        /// Constructor for <see cref="MetalPants"/> object
        /// </summary>
        public MetalPants()
        {
            // Setting up armour attributes and images
            base.movementImages = movementImages;
            base.iconImage = iconImage;
            defence = SharedData.RNG.Next(DEFENCE_MIN, DEFENSE_MAX + 1);
        }
    }
}
