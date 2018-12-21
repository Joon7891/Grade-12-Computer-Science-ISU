// Author: Joon Song
// File Name: IScreen/cs
// Project Name: ISU_Medieval_Odyseey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: IScreen interface, a interface for various game screens

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey.Screen
{
    public interface IScreen
    {
        /// <summary>
        /// Update subprogram for screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw subprogram for screen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        void Draw(SpriteBatch spriteBatch);
    }
}
