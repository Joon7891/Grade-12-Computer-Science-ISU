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
    public sealed class NewGameScreen : IScreen
    {
        /// <summary>
        /// Instance of <see cref="NewGameScreen"/> - see singleton
        /// </summary>
        public static NewGameScreen Instance { get; set; }

        private string playerName = string.Empty;
        private bool onFirstScreen = true;

        /// <summary>
        /// Subprogram to load various content for <see cref="NewGameScreen"/>
        /// </summary>
        public void LoadContent()
        {
            // Setting up singleton
            Instance = this;
        }

        /// <summary>
        /// Update subprogam for <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw subprogram for <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
