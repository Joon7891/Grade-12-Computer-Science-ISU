// Author: Joon Song
// File Name: Shoulders.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Shoulders object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Shoulders : Armour
    {
        /// <summary>
        /// Constructor for <see cref="Shoulders"/> object
        /// </summary>
        /// <param name="minDefense">The minimum defense of this <see cref="Shoulders"/></param>
        /// <param name="maxDefense">The maximum defense of this <see cref="Shoulders"/></param>
        /// <param name="minDurability">The minimum durability of this <see cref="Shoulders"/></param>
        /// <param name="maxDurability">The maximum durability of this <see cref="Shoulders"/></param>
        /// <param name="movementImages">The images corresponding to this <see cref="Shoulders"/>'s movement</param>
        /// <param name="iconImage">The <see cref="Shoulders"/> icon image</param>
        protected Shoulders(int minDefense, int maxDefense, int minDurability, int maxDurability,
            Dictionary<MovementType, Texture2D[,]> movementImages, Texture2D iconImage) : base
            (minDefense, maxDefense, minDurability, maxDurability, movementImages, iconImage) { }
    }
}
