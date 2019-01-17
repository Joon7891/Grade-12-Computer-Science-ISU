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
        /// <summary>
        /// Instance of <see cref="MainMenuScreen"/> - see singleton
        /// </summary>
        public static MainMenuScreen Instance { get; set; }
        
        // Various main menu screen components
        private Song backgroundMusic;
        private Background background;
        private SpriteFont titleFont;
        private string[] titleText = { "Medieval", "Odyssey" };
        private Vector2[] titleLocation = new Vector2[2];
        private Button[] optionButtons = new Button[3];
        
        /// <summary>
        /// Constructor for <see cref="MainMenuScreen"/>
        /// </summary>
        public MainMenuScreen()
        {
            // Setting up singleton
            Instance = this;

            // Setting up background components
            backgroundMusic = Main.Content.Load<Song>("Audio/Music/mainMenuBackgroundMusic");
            background = new Background(Main.Content.Load<Texture2D>("Images/Backgrounds/mainMenuBackgroundImage"));
            titleFont = Main.Content.Load<SpriteFont>("Fonts/TitleFont");
            for (byte i = 0; i < titleLocation.Length; ++i)
            {
                titleLocation[i] = new Vector2((SharedData.SCREEN_WIDTH - titleFont.MeasureString(titleText[i]).X) / 2, 30 + 150 * i);
            }

            // Setting up option buttons
            optionButtons[0] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/newGameButton"), new Rectangle(SharedData.SCREEN_WIDTH / 2 - 150, 400, 300, 100), () =>
            {
                Main.CurrentScreen = ScreenMode.NewGame;
                MediaPlayer.Stop();
            });
            optionButtons[1] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/loadGameButton"), new Rectangle(SharedData.SCREEN_WIDTH / 2 - 150, 510, 300, 100), () =>
            {
                Main.CurrentScreen = ScreenMode.Game;
                MediaPlayer.Stop();
            });
            optionButtons[2] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/settingsButton"), new Rectangle(SharedData.SCREEN_WIDTH / 2 - 150, 620, 300, 100), () =>
            {
                Main.CurrentScreen = ScreenMode.Settings;
                MediaPlayer.Stop();
            });
        }
        
        /// <summary>
        /// Update subprogram for <see cref="MainMenuScreen"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Playing background music
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(backgroundMusic);
            }

            // Updating various main menu buttons
            for (int i = 0; i < optionButtons.Length; ++i)
            {
                optionButtons[i].Update(gameTime);
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
            
            // Drawing background
            background.Draw(spriteBatch);

            // Drawing game title
            for (int i = 0; i < titleText.Length; ++i)
            {
                spriteBatch.DrawString(titleFont, titleText[i], titleLocation[i], Color.SaddleBrown * 0.8f);
            }

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
