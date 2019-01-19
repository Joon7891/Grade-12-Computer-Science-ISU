// Author: Joon Song
// File Name: NewGameScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/08/2018
// Modified Date: 01/08/2018
// Description: Class to hold NewGameScreen object, implements IScreen

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class HowToPlayScreen : IScreen
    {
        /// <summary>
        /// Static singleton instance of <see cref="IScreen"/>
        /// </summary>
        public static HowToPlayScreen Instance { get; set; }

        /// <summary>
        /// Constructor for <see cref="HowToPlayScreen"/>
        /// </summary>
        public HowToPlayScreen()
        {
            // Setting up singleton
            Instance = this;
        }

        /// <summary>
        /// Update subprogram for <see cref="MainMenuScreen"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw subprogram for <see cref="MainMenuScreen"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
