// Author: Joon Song
// File Name: NewGameScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/08/2018
// Modified Date: 01/08/2018
// Description: Class to hold NewGameScreen object, implements IScreen

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

namespace ISU_Medieval_Odyssey
{
    public sealed class NewGameScreen : IScreen
    {
        /// <summary>
        /// Instance of <see cref="NewGameScreen"/> - see singleton
        /// </summary>
        public static NewGameScreen Instance { get; set; }

        // Background music and image
        private Song backgroundMusic;
        private Background background;

        // Various buttons to help with screen functionality
        private Button backButton;
        private Button[] advanceButtons = new Button[3];

        private string playerName = string.Empty;
        private string seedNumber = string.Empty;
        private bool onNameScreen = true;

        /// <summary>
        /// Subprogram to load various content for <see cref="NewGameScreen"/>
        /// </summary>
        public void LoadContent()
        {
            // Setting up singleton
            Instance = this;

            // Setting up new game screen background components 
            backgroundMusic = Main.Content.Load<Song>("Audio/Music/newGameScreenBackgroundMusic");
            background = new Background(Main.Content.Load<Texture2D>("Images/Backgrounds/newGameBackgroundImage"));

            // Setting up buttons
            backButton = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/backButton"), new Rectangle(10, 10, 65, 65), () =>
            {
                onNameScreen = true;
                Main.CurrentScreen = ScreenMode.MainMenu;
                MediaPlayer.Stop();
            });
            advanceButtons[0] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/newGameNextButton"), new Rectangle(SharedData.SCREEN_WIDTH - 250, SharedData.SCREEN_HEIGHT - 90, 240, 80), () => onNameScreen = false);
            advanceButtons[1] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/newGameBackButton"), new Rectangle(10, SharedData.SCREEN_HEIGHT - 90, 240, 80), () => onNameScreen = true);
            advanceButtons[2] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/createWorldButton"), new Rectangle(SharedData.SCREEN_WIDTH - 250, SharedData.SCREEN_HEIGHT - 90, 240, 80), () =>
            {
                GameScreen.Instance.Player = new Player(playerName);
                GameScreen.Instance.World = seedNumber == string.Empty ? new World() : new World(Convert.ToInt32(seedNumber));
                Main.CurrentScreen = ScreenMode.Game;
                MediaPlayer.Stop();
            });
        }

        /// <summary>
        /// Update subprogam for <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Playing background music
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(backgroundMusic);
            }
            
            // Calling appropriate update subprogram for current sub-screen
            if (onNameScreen)
            {
                UpdateNameScreen(gameTime);
            }
            else
            {
                UpdateSeedScreen(gameTime);
            }

            // Updating back button
            backButton.Update(gameTime);
        }

        /// <summary>
        /// Update subprogram for name retrieval screen of <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        private void UpdateNameScreen(GameTime gameTime)
        {
            // Updating player name text - max length of 12
            KeyboardHelper.BuildString(ref playerName, 12);

            // Updating name screen buttons
            advanceButtons[0].Update(gameTime);
        }

        /// <summary>
        /// Update subprogram for seed retreival screen of <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing value</param>
        private void UpdateSeedScreen(GameTime gameTime)
        {
            // Updating seed screen buttons
            for (int i = 1; i < advanceButtons.Length; ++i)
            {
                advanceButtons[i].Update(gameTime);
            }

            // Updating seed text
        }

        /// <summary>
        /// Draw subprogram for <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Beginning spriteBatch
            spriteBatch.Begin();

            // Drawing background
            background.Draw(spriteBatch);

            // Calling appropriate draw subprogram for current sub-screen
            if (onNameScreen)
            {
                DrawNameScreen(spriteBatch);
            }
            else
            {
                DrawSeedScreen(spriteBatch);
            }

            // Drawing back button
            backButton.Draw(spriteBatch);

            // Ending spriteBatch
            spriteBatch.End();
        }

        /// <summary>
        /// Draw subprogram for name retrieval screen of <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        private void DrawNameScreen(SpriteBatch spriteBatch)
        {
            // Drawing name screen buttons
            advanceButtons[0].Draw(spriteBatch);
        }

        /// <summary>
        /// Draw subprogram for seed retrieval screen of <see cref="NewGameScreen"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        private void DrawSeedScreen(SpriteBatch spriteBatch)
        {
            // Drawing seed screen buttons
            for (int i = 1; i < advanceButtons.Length; ++i)
            {
                advanceButtons[i].Draw(spriteBatch);
            }

            // Drawing seed text
        }
    }
}
