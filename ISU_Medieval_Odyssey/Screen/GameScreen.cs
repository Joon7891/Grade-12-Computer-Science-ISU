// Author: Joon Song
// File Name: GameScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold GameScreen object, implements IScreen

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class GameScreen : IScreen
    {
        // Instance of player and world
        private Player player = new Player();
        private World world = new World();

        private Camera camera = new Camera();
        
        /// <summary>
        /// Subprogram to load GameScreen content
        /// </summary>
        public void LoadContent()
        {

        }
        
        /// <summary>
        /// Update subprogram for GameScreen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating player and world
            player.Update(gameTime);
            world.Update(gameTime);
            camera.Position = player.CameraClamp;
        }

        /// <summary>
        /// Draw subprogram for GameScreen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing player and world - in adjusted camera
            spriteBatch.Begin(transformMatrix : camera.ViewMatrix, samplerState : SamplerState.PointClamp);
            world.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();


        }
    }
}
