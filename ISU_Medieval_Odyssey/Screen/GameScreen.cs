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
        private Player player = new Player("Joon7891");
        private World world = new World();

        // Camera-realted variables
        private Camera camera = new Camera();
        private Vector2 cameraOffset;

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
            player.Update(gameTime, camera.Position);
            world.Update(gameTime, player.CurrentChunk);

            // Updating the offset of the camera and moving camera if appropraite
            cameraOffset = player.Center.ToVector2() - camera.Center;
            if (Math.Abs(cameraOffset.X) > SharedData.SCREEN_WIDTH / 5)
            {
                camera.Position += (cameraOffset.X > 0 ? 1 : -1) * new Vector2(Math.Abs(cameraOffset.X) - SharedData.SCREEN_WIDTH / 5, 0);
            }
            if (Math.Abs(cameraOffset.Y) > SharedData.SCREEN_HEIGHT / 5)
            {
                camera.Position += (cameraOffset.Y > 0 ? 1 : -1) * new Vector2(0, Math.Abs(cameraOffset.Y) - SharedData.SCREEN_HEIGHT / 5);
            }
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

            // Drawing player HUD and other fixed graphics in regular camera
            spriteBatch.Begin();
            player.DrawHUD(spriteBatch);
            spriteBatch.End();
        }
    }
}
