// Author: Joon Song
// File Name: MainMenuScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold MainMenuScreen object, implements IScreen

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class MainMenuScreen : IScreen
    {
        // Various main menu screen components
        private Song backgroundMusic;
        private Background background;
        private Button[] optionButtons = new Button[3];
        
        /// <summary>
        /// Subprogram to load main menu screen content
        /// </summary>
        public void LoadContent()
        {
            // Setting up background components
            backgroundMusic = Main.Content.Load<Song>("Audio/Music/mainMenuBackgroundMusic");
            background = new Background(Main.Content.Load<Texture2D>("Images/Backgrounds/mainMenuBackgroundImage"));

            // Setting up option buttons
            optionButtons[0] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/newGameButton"), new Rectangle(SharedData.SCREEN_WIDTH / 2 - 150, 400, 300, 100),() =>
            {
                MediaPlayer.Stop();
                Main.CurrentScreen = ScreenMode.Game;
            });
            optionButtons[1] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/loadGameButton"), new Rectangle(SharedData.SCREEN_WIDTH / 2 - 150, 510, 300, 100), () =>
            {
                MediaPlayer.Stop();
                Main.CurrentScreen = ScreenMode.Game;
            });
            optionButtons[2] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/settingsButton"), new Rectangle(SharedData.SCREEN_WIDTH / 2 - 150, 620, 300, 100), () =>
            {
                MediaPlayer.Stop();
                Main.CurrentScreen = ScreenMode.Settings;
            });
        }
        
        /// <summary>
        /// Update subprogram for MainMenuScreen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating various main menu buttons
            for (int i = 0; i < optionButtons.Length; ++i)
            {
                optionButtons[i].Update(gameTime);
            }
            
            // Playing background music
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(backgroundMusic);
            }
        }

        /// <summary>
        /// Draw subprogram for MainMenuScreen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Beginning sprite batch
            spriteBatch.Begin();
            
            // Drawing background
            background.Draw(spriteBatch);

            // Drawing various main menu buttons
            for (int i = 0; i < optionButtons.Length; ++i)
            {
                optionButtons[i].Draw(spriteBatch);
            }

            // Ending sprite batch
            spriteBatch.End();
        }
    }
}
