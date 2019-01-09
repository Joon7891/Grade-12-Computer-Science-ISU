// Author: Joon Song
// File Name: SettingsScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold SettingsScreen object, implements IScreen

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class SettingsScreen : IScreen
    {
        /// <summary>
        /// Instance of <see cref="SettingsScreen"/> - see singleton
        /// </summary>
        public static SettingsScreen Instance { get; set; }
        
        // Background graphics and audio
        private Song backgroundMusic;
        private Background background;
        private Button backButton;

        // Settings graphical interface
        private Slider musicVolumeSlider;
        private Slider soundEffectVolumeSlider;
        
        /// <summary>
        /// Subprogram to load settings screen content
        /// </summary>
        public void LoadContent()
        {
            // Setting up singleton
            Instance = this;
            
            // Setting up settings screen background components
            background = new Background(Main.Content.Load<Texture2D>("Images/Backgrounds/settingsBackground"));
            backgroundMusic = Main.Content.Load<Song>("Audio/Music/settingsBackgroundMusic");
            backButton = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/backButton"), new Rectangle(10, 10, 65, 65), () =>
            {
                Main.CurrentScreen = ScreenMode.MainMenu;
                MediaPlayer.Stop();
            });

            // Setting up settings graphical interface
            musicVolumeSlider = new Slider(new Rectangle(300, 200, 400, 30), Color.White, Color.Green * 0.8f);
            soundEffectVolumeSlider = new Slider(new Rectangle(300, 400, 400, 30), Color.White, Color.Green * 0.8f);
        }
        
        /// <summary>
        /// Update subprogram for SettingsScreen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Playing background music
            if (MediaPlayer.State != MediaState.Playing)
            {
               MediaPlayer.Play(backgroundMusic);
            }

            // Updating graphical interface
            musicVolumeSlider.Update(gameTime);
            soundEffectVolumeSlider.Update(gameTime);

            // Updating back button
            backButton.Update(gameTime);
        }

        /// <summary>
        /// Draw subprogram for SettingsScreen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Starting sprite batch
            spriteBatch.Begin();

            // Drawing background
            background.Draw(spriteBatch);

            // Drawing graphical interface
            musicVolumeSlider.Draw(spriteBatch);
            soundEffectVolumeSlider.Draw(spriteBatch);

            // Drawing back button
            backButton.Draw(spriteBatch);

            // Ending sprite batch
            spriteBatch.End();
        }
    }
}
