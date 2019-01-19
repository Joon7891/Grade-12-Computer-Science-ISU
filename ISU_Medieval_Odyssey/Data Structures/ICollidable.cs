// Author: Joon Song
// File Name: ICollidable.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/17/2019
// Modified Date: 01/17/2019
// Description: Interface to hold ICollidable, which objects that can collide must implement

using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public interface ICollidable
    {
        /// <summary>
        /// The hit box of this <see cref="ICollidable"/>
        /// </summary>
        Rectangle HitBox { get; }
    }
}
