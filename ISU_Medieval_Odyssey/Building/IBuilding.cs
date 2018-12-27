// Author: Joon Song
// File Name: IBuilding.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Interface to hold IBuilding

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public interface IBuilding
    {
        /// <summary>
        /// Subprogram to draw the outside of the building
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        void DrawOutside(SpriteBatch spriteBatch);

        /// <summary>
        /// Subprogram to draw the inside of the building
        /// </summary>
        /// <param name="spriteBatch">SprieBatch to draw sprites</param>
        void DrawInside(SpriteBatch spriteBatch);
    }
}
