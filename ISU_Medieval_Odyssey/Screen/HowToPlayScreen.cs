// Author: Joon Song
// File Name: NewGameScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/08/2018
// Modified Date: 01/08/2018
// Description: Class to hold NewGameScreen object, implements IScreen

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace ISU_Medieval_Odyssey
{
    public sealed class HowToPlayScreen : IScreen
    {
        /// <summary>
        /// Static singleton instance of <see cref="IScreen"/>
        /// </summary>
        public static HowToPlayScreen Instance { get; set; }

        // Background imgaes and audio
        private Background[] backgrounds = new Background[3];
        private int currentBackground = 0;
        private Song backgroundMusic;

        // Buttons to interact with this screen
        private Button backButton;
        private Button previousButton;
        private Button nextButton;

        /// <summary>
        /// Constructor for <see cref="HowToPlayScreen"/>
        /// </summary>
        public HowToPlayScreen()
        {
            // Setting up singleton
            Instance = this;

            // Importing various background images and audio
            backgroundMusic = Main.Content.Load<Song>("Audio/Music/howToPlayBackgroundMusic");
            for (byte i = 0; i < backgrounds.Length; ++i)
            {
                backgrounds[i] = new Background(Main.Content.Load<Texture2D>($"Images/Backgrounds/howToPlayBackgroundImage{i}"));
            }

            // Setting up various buttons
            backButton = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/backButton"), new Rectangle(10, 10, 65, 65), () =>
            {
                Main.CurrentScreen = ScreenMode.MainMenu;
                MediaPlayer.Stop();
            });
            previousButton = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/previousButton"), new Rectangle(10, SharedData.SCREEN_HEIGHT - 90, 240, 80), () => --currentBackground);
            nextButton = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/nextButton"), new Rectangle(SharedData.SCREEN_WIDTH - 250, SharedData.SCREEN_HEIGHT - 90, 240, 80), () => ++currentBackground);
        }

        /// <summary>
        /// Update subprogram for <see cref="MainMenuScreen"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Playing music
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(backgroundMusic);
            }

            // Updating appropriate buttons, when applicable
            backButton.Update(gameTime);
            if (currentBackground > 0)
            {
                previousButton.Update(gameTime);
            }
            if (currentBackground < backgrounds.Length - 1)
            {
                nextButton.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw subprogram for <see cref="MainMenuScreen"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Beginning sprite batch
            spriteBatch.Begin();

            // Drawing current background
            backgrounds[currentBackground].Draw(spriteBatch);

            // Drawing key bindings in appropraite screen
            if (currentBackground == 2)
            {
                SettingsScreen.Instance.DrawKeys(spriteBatch);
            }

            // Drawing appropriate buttons, when applicable
            backButton.Draw(spriteBatch);
            if (currentBackground > 0)
            {
                previousButton.Draw(spriteBatch);
            }
            if (currentBackground < backgrounds.Length - 1)
            {
                nextButton.Draw(spriteBatch);
            }

            // Ending sprite batch
            spriteBatch.End();
        }
    }
}
