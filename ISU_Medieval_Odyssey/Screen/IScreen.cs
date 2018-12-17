// Author: Joon Song
// File Name: IScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 12/17/2019
// Description: Interface for Screen objects

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey.Screen
{
    public interface IScreen
    {
        /// <summary>
        /// Update subprogram for Screen object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw subprogram for Screen object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        void Draw(SpriteBatch spriteBatch);
    }
}
