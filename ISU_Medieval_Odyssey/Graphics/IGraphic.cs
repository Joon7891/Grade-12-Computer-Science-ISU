// Author: Joon Song
// File Name: IGraphic.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/09/2018
// Modified Date: 01/09/2018
// Description: Interface for graphics

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public interface IGraphic
    {
        /// <summary>
        /// Draw subprogram for the <see cref="IGraphic"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        void Draw(SpriteBatch spriteBatch);
    }
}
