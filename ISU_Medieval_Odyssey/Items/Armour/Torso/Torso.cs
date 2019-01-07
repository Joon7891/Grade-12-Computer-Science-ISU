// Author: Joon Song
// File Name: Torso.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/25/2018
// Modified Date: 12/25/2018
// Description: Class to hold Torso object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Torso : Armour
    {
        /// <summary>
        /// Constructor for <see cref="Torso"/> object
        /// </summary>
        /// <param name="minDefense">The minimum defense of this <see cref="Torso"/></param>
        /// <param name="maxDefense">The maximum defense of this <see cref="Torso"/></param>
        /// <param name="minDurability">The minimum durability of this <see cref="Torso"/></param>
        /// <param name="maxDurability">The maximum durability of this <see cref="Torso"/></param>
        /// <param name="movementImages">The images corresponding to this <see cref="Torso"/>'s movement</param>
        /// <param name="iconImage">The <see cref="Torso"/> icon image</param>
        protected Torso(int minDefense, int maxDefense, int minDurability, int maxDurability,
            Dictionary<MovementType, Texture2D[,]> movementImages, Texture2D iconImage) : base
            (minDefense, maxDefense, minDurability, maxDurability, movementImages, iconImage)
        {

        }
    }
}
