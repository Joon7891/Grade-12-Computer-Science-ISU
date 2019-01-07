// Author: Joon Song, Steven Ung
// File Name: Belt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Belt object

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Belt : Armour
    {
        /// <summary>
        /// Constructor for <see cref="Belt"/> object
        /// </summary>
        /// <param name="minDefense">The minimum defense of this <see cref="Belt"/></param>
        /// <param name="maxDefense">The maximum defense of this <see cref="Belt"/></param>
        /// <param name="minDurability">The minimum durability of this <see cref="Belt"/></param>
        /// <param name="maxDurability">The maximum durability of this <see cref="Belt"/></param>
        /// <param name="movementImages">The images corresponding to this <see cref="Belt"/>'s movement</param>
        /// <param name="iconImage">The <see cref="Belt"/> icon image</param>
        protected Belt(int minDefense, int maxDefense, int minDurability, int maxDurability,
            Dictionary<MovementType, Texture2D[,]> movementImages, Texture2D iconImage) : base
            (minDefense, maxDefense, minDurability, maxDurability, movementImages, iconImage)
        {

        }
    }
}
