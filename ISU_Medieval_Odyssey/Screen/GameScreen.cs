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
        public Player Player { get; set; } = new Player("Test");

        /// <summary>
        /// The world of the game
        /// </summary>
        public World World { get; set; } = new World();

        // Camera-realted variables
        public Camera Camera { get; set; } = new Camera();
        private Vector2 cameraOffset;

        // Minimap-related variables
        private Camera miniMapCamera = new Camera();
        private const int MINI_MAP_SIZE = 200;
        private Vector2 adjustmentVector = new Vector2(895, 11);
        private Vector2 cameraVerticalShift = new Vector2(0, MINI_MAP_SIZE / 2);
        private Sprite miniMapBorder;

        // Statistics display-related variables
        private bool showStatistics = false;
        private Vector2[] statisticsLoc = new Vector2[5];

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
                statisticsLoc[i] = new Vector2(455, 15 + 30 * i);
            }

            // Setting up minimap
            miniMapCamera.OrthographicSize = 0.057f;
            miniMapBorder = new Sprite(Main.Content.Load<Texture2D>("Images/Sprites/miniMapBorder"), 
                new Rectangle(SharedData.SCREEN_WIDTH - MINI_MAP_SIZE - 10, 10, MINI_MAP_SIZE, MINI_MAP_SIZE));
            miniMapCamera.Position = miniMapBorder.Rectangle.Location.ToVector2();
        }
        
        /// <summary>
        /// Update subprogram for GameScreen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating player and world
            Player.Update(gameTime, Camera.Position);
            World.Update(gameTime);

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

            // Updating logic for minimap camera
            miniMapCamera.Position = - World.WorldBoundsRect.Location.ToVector2() / Chunk.SIZE + adjustmentVector;
            miniMapCamera.Origin = miniMapCamera.Position - World.WorldBoundsRect.Location.ToVector2() / Chunk.SIZE * miniMapCamera.OrthographicSize / 2.0f;
        }

        /// <summary>
        /// Draw subprogram for GameScreen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing player and world in adjusted camera
            spriteBatch.Begin(transformMatrix: Camera.ViewMatrix, samplerState: SamplerState.PointClamp);
            World.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            spriteBatch.End();

            // Drawing player minimap components
            spriteBatch.Begin(transformMatrix: miniMapCamera.ViewMatrix, samplerState: SamplerState.PointClamp);
            World.DrawMini(spriteBatch);
            Player.DrawMini(spriteBatch);
            spriteBatch.End();
              
            // Beginning regular sprite batch
            spriteBatch.Begin();

            // Drawing shop inventory, if appropriate
            if (World.Instance.CurrentBuilding is Shop && Player.Instance.InTransaction)
            {
                ((Shop)World.Instance.CurrentBuilding).DrawInventory(spriteBatch);
            }

            // Drawing statistics and player HUD
            if (showStatistics)
            {
                DrawStatistics(spriteBatch);
            }
            Player.DrawHUD(spriteBatch);
            miniMapBorder.Draw(spriteBatch);

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
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Enemies Loaded: {World.Instance.EnemiesLoaded}", statisticsLoc[3], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Total Memory Used: {Math.Round(GC.GetTotalMemory(false) / 1048576.0, 3)} MB", statisticsLoc[4], Color.White);
        }
    }
}
