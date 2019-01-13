// Author: Joon Song
// File Name: SettingsScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold SettingsScreen object, implements IScreen

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    public sealed class SettingsScreen : IScreen
    {
        /// <summary>
        /// Instance of <see cref="SettingsScreen"/> - see singleton
        /// </summary>
        public static SettingsScreen Instance { get; set; }

        /// <summary>
        /// The keybinding for the move Up functionality
        /// </summary>
        public Keys Up { get; private set; } = Keys.W;

        /// <summary>
        /// The keybinding for the move Down functionality 
        /// </summary>
        public Keys Right { get; private set; } = Keys.D;

        /// <summary>
        /// The keybinding for the move Down functionality
        /// </summary>
        public Keys Down { get; private set; } = Keys.S;

        /// <summary>
        /// The keybinding for the move Left functionality
        /// </summary>
        public Keys Left { get; private set; } = Keys.A;

        /// <summary>
        /// The keybinding for the Interaction functionality
        /// </summary>
        public Keys Interact { get; private set; } = Keys.E;

        /// <summary>
        /// The keybindings for quick HotBar access
        /// </summary>
        public Keys[] HotbarShortcut { get; private set; } = new Keys[10];

        /// <summary>
        /// The keybinding for Debug
        /// </summary>
        public Keys Debug { get; private set; } = Keys.F12;

        // Background graphics, audio, and fonts/text locations
        private Song backgroundMusic;
        private Background background;
        private Button backButton;
        private SpriteFont[] textFonts = new SpriteFont[1];
        private Vector2 titleLocation;

        /// <summary>
        /// The Music volume level
        /// </summary>
        public float MusicVolume => (float) Math.Round(musicVolumeSlider.Value, 2);
        private Slider musicVolumeSlider;

        /// <summary>
        /// The SoundEffect volume level
        /// </summary>
        public float SoundEffectVolume => (float) Math.Round(soundEffectVolumeSlider.Value, 2);
        private Slider soundEffectVolumeSlider;
        
        /// <summary>
        /// Constructor for <see cref="SettingsScreen"/>
        /// </summary>
        public SettingsScreen()
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
            musicVolumeSlider = new Slider(new Rectangle(300, 200, 400, 30), Color.White, Color.Black * 0.8f);
            soundEffectVolumeSlider = new Slider(new Rectangle(300, 400, 400, 30), Color.White, Color.Black * 0.8f);

            // Loading in various fonts and text locations
            for (byte i = 0; i < textFonts.Length; ++i)
            {
                textFonts[i] = Main.Content.Load<SpriteFont>($"Fonts/SettingsFont{i}");
            }
            titleLocation = new Vector2((SharedData.SCREEN_WIDTH - textFonts[0].MeasureString("Settings").X) / 2, 10);

            HotbarShortcut[0] = Keys.D1;
            HotbarShortcut[1] = Keys.D2;
            HotbarShortcut[2] = Keys.D3;
            HotbarShortcut[3] = Keys.D4;
            HotbarShortcut[4] = Keys.D5;
            HotbarShortcut[5] = Keys.D6;
            HotbarShortcut[6] = Keys.D7;
            HotbarShortcut[7] = Keys.D8;
            HotbarShortcut[8] = Keys.D9;
            HotbarShortcut[9] = Keys.D0;
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

            // Drawing background and "Settings" text
            background.Draw(spriteBatch);
            spriteBatch.DrawString(textFonts[0], "Settings", titleLocation, Color.White);

            // Drawing volunme sliders and corresponding text
            musicVolumeSlider.Draw(spriteBatch);
            soundEffectVolumeSlider.Draw(spriteBatch);

            // Drawing keybindings and corresponding graphics

            // Drawing back button
            backButton.Draw(spriteBatch);

            // Ending sprite batch
            spriteBatch.End();
        }
    }
}
