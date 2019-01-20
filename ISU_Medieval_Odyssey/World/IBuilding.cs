// Author: Joon Song
// File Name: IBuilding.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Interface to hold IBuilding

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public interface IBuilding
    {
        /// <summary>
        /// The <see cref="Rectangle"/> that this building is drawn in
        /// </summary>
        Rectangle Rectangle { get; }

        /// <summary>
        /// The corner tile position of this building
        /// </summary>
        Vector2Int CornerTile { get; }

        /// <summary>
        /// Update subprogram for this <see cref="IBuilding"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        void Update(GameTime gameTime);
        
        /// <summary>
        /// Subprogram to draw the outside of the <see cref="IBuilding"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        void DrawOutside(SpriteBatch spriteBatch);

        /// <summary>
        /// Subprogram to draw the inside of the <see cref="IBuilding"/>
        /// </summary>
        /// <param name="spriteBatch">SprieBatch to draw sprites</param>
        void DrawInside(SpriteBatch spriteBatch);

        /// <summary>
        /// Subprogram to set properties of various <see cref="Tile"/> that the <see cref="IBuilding"/> affects
        /// </summary>
        void SetTiles(Vector2Int cornerTile);
    }
}
