// Author: Joon Song
// File Name: GameScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold GameScreen object, implements IScreen

using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        // Statistics display-related variables
        private bool showStatistics = false;
        private Vector2[] statisticsLoc = new Vector2[4];

        /// <summary>
        /// Subprogram to load GameScreen content
        /// </summary>
        public void LoadContent()
        {
            // Setting up statistics locations
            for (byte i = 0; i < statisticsLoc.Length; ++i)
            {
                statisticsLoc[i] = new Vector2(720, 15 + 30 * i);
            }
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
            if (Math.Abs(cameraOffset.X) > SharedData.SCREEN_WIDTH / 6)
            {
                camera.Position += (cameraOffset.X > 0 ? 1 : -1) * new Vector2(Math.Abs(cameraOffset.X) - SharedData.SCREEN_WIDTH / 6, 0);
            }
            if (Math.Abs(cameraOffset.Y) > SharedData.SCREEN_HEIGHT / 6)
            {
                camera.Position += (cameraOffset.Y > 0 ? 1 : -1) * new Vector2(0, Math.Abs(cameraOffset.Y) - SharedData.SCREEN_HEIGHT / 6);
            }

            // Showing/unshowing statistics as desired
            if (KeyboardHelper.NewKeyStroke(Keys.F12))
            {
                showStatistics = !showStatistics;
            }            
        }

        /// <summary>
        /// Draw subprogram for GameScreen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing player and world
            world.Draw(spriteBatch, camera);
            player.Draw(spriteBatch, camera);

            // Beginning regular sprite batch
            spriteBatch.Begin();

            if (showStatistics)
            {
                DrawStatistics(spriteBatch);
            }

            // Ending regular sprite batch
            spriteBatch.End();
        }

        /// <summary>
        /// Subprogram to draw various statistics about the <see cref="GameScreen"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void DrawStatistics(SpriteBatch spriteBatch)
        {
            // Drawing various statistics
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Tile Coordinate: {player.CurrentTile}", statisticsLoc[0], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Chunk Coordinate: {player.CurrentChunk}", statisticsLoc[1], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Frames Per Second: {Main.FPS}", statisticsLoc[2], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Total Memory Used: {Math.Round(GC.GetTotalMemory(true) / 1000000.0, 3)} MB", statisticsLoc[3], Color.White);
        }
    }
}
