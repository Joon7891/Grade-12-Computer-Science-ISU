// Author: Joon Song
// File Name: Pants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold Pants object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Pants : Armour
    {
        /// <summary>
        /// Constructor for <see cref="Pants"/> object
        /// </summary>
        /// <param name="minDefense">The minimum defense of this <see cref="Pants"/></param>
        /// <param name="maxDefense">The maximum defense of this <see cref="Pants"/></param>
        /// <param name="minDurability">The minimum durability of this <see cref="Pants"/></param>
        /// <param name="maxDurability">The maximum durability of this <see cref="Pants"/></param>
        /// <param name="movementImages">The images corresponding to this <see cref="Pants"/>'s movement</param>
        /// <param name="iconImage">The <see cref="Pants"/> icon image</param>
        protected Pants(int minDefense, int maxDefense, int minDurability, int maxDurability,
            Dictionary<MovementType, Texture2D[,]> movementImages, Texture2D iconImage) : base
            (minDefense, maxDefense, minDurability, maxDurability, movementImages, iconImage) { }
    }
}
