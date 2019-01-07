// Author: Joon Song
// File Name: Head.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Head object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Head : Armour
    {
        /// <summary>
        /// Constructor for <see cref="Head"/> object
        /// </summary>
        /// <param name="minDefense">The minimum defense of this <see cref="Head"/></param>
        /// <param name="maxDefense">The maximum defense of this <see cref="Head"/></param>
        /// <param name="minDurability">The minimum durability of this <see cref="Head"/></param>
        /// <param name="maxDurability">The maximum durability of this <see cref="Head"/></param>
        /// <param name="movementImages">The images corresponding to this <see cref="Head"/>'s movement</param>
        /// <param name="iconImage">The <see cref="Head"/> icon image</param>
        protected Head(int minDefense, int maxDefense, int minDurability, int maxDurability,
            Dictionary<MovementType, Texture2D[,]> movementImages, Texture2D iconImage) : base
            (minDefense, maxDefense, minDurability, maxDurability, movementImages, iconImage) { }
    }
}
