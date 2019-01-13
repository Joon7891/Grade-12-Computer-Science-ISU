// Author: Joon Song
// File Name: GameScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold GameScreen object, implements IScreen

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public sealed class GameScreen : IScreen
    {
        /// <summary>
        /// Instance of <see cref="GameScreen"/> - see singleton
        /// </summary>
        public static GameScreen Instance { get; set; }

        /// <summary>
        /// The player in the world
        /// </summary>
        public Player Player { get; set; } = new Player("");

        /// <summary>
        /// The world of the game
        /// </summary>
        public World World { get; set; } = new World();

        // Camera-realted variables
        public Camera Camera { get; set; } = new Camera();
        private Vector2 cameraOffset;

        // Statistics display-related variables
        private bool showStatistics = false;
        private Vector2[] statisticsLoc = new Vector2[4];

        /// <summary>
        /// Constructor for <see cref="GameScreen"/>
        /// </summary>
        public GameScreen()
        {
            // Setting up singleton
            Instance = this;

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
            Player.Update(gameTime, Camera.Position);
            World.Update(gameTime, Player.CurrentChunk);

            // Updating the offset of the camera and moving camera if appropraite
            cameraOffset = Player.Center.ToVector2() - Camera.Center;
            if (Math.Abs(cameraOffset.X) > SharedData.SCREEN_WIDTH / 6)
            {
                Camera.Position += (cameraOffset.X > 0 ? 1 : -1) * new Vector2(Math.Abs(cameraOffset.X) - SharedData.SCREEN_WIDTH / 6, 0);
            }
            if (Math.Abs(cameraOffset.Y) > SharedData.SCREEN_HEIGHT / 6)
            {
                Camera.Position += (cameraOffset.Y > 0 ? 1 : -1) * new Vector2(0, Math.Abs(cameraOffset.Y) - SharedData.SCREEN_HEIGHT / 6);
            }

            // Showing/unshowing statistics as desired
            if (KeyboardHelper.NewKeyStroke(SettingsScreen.Instance.Statistics))
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
            World.Draw(spriteBatch, Camera);
            Player.Draw(spriteBatch, Camera);

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
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Tile Coordinate: {Player.CurrentTile}", statisticsLoc[0], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Chunk Coordinate: {Player.CurrentChunk}", statisticsLoc[1], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Frames Per Second: {Main.FPS}", statisticsLoc[2], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Total Memory Used: {Math.Round(GC.GetTotalMemory(false) / 1048576.0, 3)} MB", statisticsLoc[3], Color.White);
        }
    }
}
