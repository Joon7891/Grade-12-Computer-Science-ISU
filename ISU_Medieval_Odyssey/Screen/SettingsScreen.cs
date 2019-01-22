// Author: Joon Song
// File Name: SettingsScreen.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold SettingsScreen object, implements IScreen

using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace ISU_Medieval_Odyssey
{
    public sealed class SettingsScreen : IScreen
    {
        /// <summary>
        /// Instance of <see cref="SettingsScreen"/> - see singleton
        /// </summary>
        public static SettingsScreen Instance { get; set; }

        /// <summary>
        /// The Music volume level
        /// </summary>
        public float MusicVolume => (float)Math.Round(volumeSliders[0].Value, 2);

        /// <summary>
        /// The SoundEffect volume level
        /// </summary>
        public float SoundEffectVolume => (float)Math.Round(volumeSliders[1].Value, 2);

        /// <summary>
        /// The keybinding for the move Up functionality
        /// </summary>
        public Keys Up => keyBindings[0].Key;

        /// <summary>
        /// The keybinding for the move Down functionality 
        /// </summary>
        public Keys Right => keyBindings[1].Key;

        /// <summary>
        /// The keybinding for the move Down functionality
        /// </summary>
        public Keys Down => keyBindings[2].Key;

        /// <summary>
        /// The keybinding for the move Left functionality
        /// </summary>
        public Keys Left => keyBindings[3].Key;

        /// <summary>
        /// The keybinding for the Interaction functionality
        /// </summary>
        public Keys Interact => keyBindings[4].Key;

        /// <summary>
        /// The keybinding to pickup an item
        /// </summary>
        public Keys Pickup => keyBindings[5].Key;

        /// <summary>
        /// The keybinding to open inventory
        /// </summary>
        public Keys Inventory => keyBindings[6].Key;

        /// <summary>
        /// The keybinding for Pause
        /// </summary>
        public Keys Pause => keyBindings[7].Key;

        /// <summary>
        /// The keybinding for Statistics
        /// </summary>
        public Keys Statistics => keyBindings[8].Key;

        /// <summary>
        /// The keybindings for quick HotBar access
        /// </summary>
        public Keys[] HotbarShortcut => keyBindings.SubArray(9, 9).Select(keyBinding => keyBinding.Key).ToArray();

        // Settings screen background components
        private Song backgroundMusic;
        private Background background;
        private Button backButton;
        private Vector2 titleLocation;

        // Graphical user interface components for volume sliders
        private Slider[] volumeSliders = new Slider[2];
        private string[] volumeText = 
        {
            "Music Volume",
            "Sound Effect Volume"
        };
        private Vector2[] textLocations = new Vector2[5];
        private SpriteFont[] textFonts = new SpriteFont[2];

        // Variables required for keybindings functionality
        private KeyBinding[] keyBindings = new KeyBinding[18];
        private int selectedKeyBinding = -1;
        private readonly string[] keyBindingsText =
        {
            "Up",
            "Right",
            "Down",
            "Left",
            "Interact",
            "Pickup",
            "Inventory",
            "Pause",
            "Statistics",
            "Inventory 1",
            "Inventory 2",
            "Inventory 3",
            "Inventory 4",
            "Inventory 5",
            "Inventory 6",
            "Inventory 7",
            "Inventory 8",
            "Inventory 9"
        };

        // Various settings application buttons
        private readonly SettingsData defaultSettingsData;
        private Button[] settingOptionButtons = new Button[2];

        /// <summary>
        /// Constructor for <see cref="SettingsScreen"/>
        /// </summary>
        public SettingsScreen()
        {
            // Loading settings data
            SettingsData settingsData;
            settingsData = IO.LoadSettingsData();

            // Setting up singleton
            Instance = this;

            // Setting up settings screen background components
            for (byte i = 0; i < textFonts.Length; ++i)
            {
                textFonts[i] = Main.Content.Load<SpriteFont>($"Fonts/SettingsFont{i}");
            }
            titleLocation = new Vector2((SharedData.SCREEN_WIDTH - textFonts[0].MeasureString("Settings").X) / 2, 10);
            background = new Background(Main.Content.Load<Texture2D>("Images/Backgrounds/settingsBackground"));
            backgroundMusic = Main.Content.Load<Song>("Audio/Music/settingsBackgroundMusic");
            backButton = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/backButton"), new Rectangle(10, 10, 65, 65), () =>
            {
                Main.CurrentScreen = ScreenMode.MainMenu;
                MediaPlayer.Stop();
            });

            // Setting up volume level and keybindings graphical interface
            for (byte i = 0; i < volumeSliders.Length; ++i)
            {
                volumeSliders[i] = new Slider(new Rectangle(200, 180 + 125 * i, 600, 40), Color.White, Color.Black * 0.8f, i == 0 ? settingsData.MusicVolume : settingsData.SoundEffectVolume);
                textLocations[2 * i] = new Vector2((SharedData.SCREEN_WIDTH - textFonts[1].MeasureString(volumeText[i]).X) / 2, 130 + 125 * i);
                textLocations[2 * i + 1] = new Vector2(825, 175 + 125 * i);
            }
            textLocations[4] = new Vector2((SharedData.SCREEN_WIDTH - textFonts[1].MeasureString("KeyBindings").X) / 2, 380);
            for (byte i = 0; i < keyBindings.Length; ++i)
            {
                keyBindings[i] = new KeyBinding(settingsData.KeyBindings[i], keyBindingsText[i], new Rectangle(18 + 168 * (i % 6), 470 + 85 * (i / 6), 130, 35));
            }

            // Setting up various settings buttons
            settingOptionButtons[0] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/defaultButton"), new Rectangle(10, SharedData.SCREEN_HEIGHT - 90, 240, 80), () =>
            {
                volumeSliders[0].Value = defaultSettingsData.MusicVolume;
                volumeSliders[1].Value = defaultSettingsData.SoundEffectVolume;
                for (byte i = 0; i < keyBindings.Length; ++i)
                {
                    keyBindings[i].Key = defaultSettingsData.KeyBindings[i];
                }
            });
            settingOptionButtons[1] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/saveButton"), new Rectangle(SharedData.SCREEN_WIDTH - 250, SharedData.SCREEN_HEIGHT - 90, 240, 80), () =>
            {
                IO.SaveSettingsData(MusicVolume, SoundEffectVolume, keyBindings.Select(keyBinding => keyBinding.Key).ToArray());
            });
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

            // Updating volume sliders
            for (byte i = 0; i < volumeSliders.Length; ++i)
            {
                volumeSliders[i].Update(gameTime);
            }

            // Updating volume levels if sliders are selected
            for (byte i = 0; i < volumeSliders.Length; ++i)
            {
                if (volumeSliders[i].IsSelected)
                {
                    ApplyVolumeChanges();
                }
            }

            // Determining selected keybinding
            if (MouseHelper.NewLeftClick())
            {
                selectedKeyBinding = -1;

                for (byte i = 0; i < keyBindings.Length; ++i)
                {
                    if (MouseHelper.IsRectangleLeftClicked(keyBindings[i].Rectangle))
                    {
                        selectedKeyBinding = i;
                        break;
                    }
                }
            }

            // Setting new keybinding if possible
            if (selectedKeyBinding != -1)
            {
                if (KeyboardHelper.SelectedKeyFromSet(KeyBinding.AllowedBindings, KeyBinding.DisallowedBindings) != Keys.None)
                {
                    keyBindings[selectedKeyBinding].Key = KeyboardHelper.SelectedKeyFromSet(KeyBinding.AllowedBindings, KeyBinding.DisallowedBindings);
                    selectedKeyBinding = -1;
                }
            }

            // Updating setting option buttons
            for (byte i = 0; i < settingOptionButtons.Length; ++i)
            {
                settingOptionButtons[i].Update(gameTime);
            }

            // Updating back button
            backButton.Update(gameTime);
        }

        /// <summary>
        /// Subprogram to apply various volume changes
        /// </summary>
        public void ApplyVolumeChanges()
        {
            // Setting changes for music and soundeffects 
            MediaPlayer.Volume = volumeSliders[0].Value;
            SoundEffect.MasterVolume = volumeSliders[1].Value;
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
            for (byte i = 0; i < volumeSliders.Length; ++i)
            {
                volumeSliders[i].Draw(spriteBatch);
                spriteBatch.DrawString(textFonts[1], volumeText[i], textLocations[2 * i], Color.White);
                spriteBatch.DrawString(textFonts[1], $"{Math.Round(100 * volumeSliders[i].Value, 2)}%", textLocations[2 * i + 1], Color.White);
            }

            // Drawing keybindings and corresponding graphics
            spriteBatch.DrawString(textFonts[1], "KeyBindings", textLocations[4], Color.White);
            DrawKeys(spriteBatch);

            // Drawing setting options buttons
            for (byte i = 0; i < settingOptionButtons.Length; ++i)
            {
                settingOptionButtons[i].Draw(spriteBatch);
            }

            // Drawing back button
            backButton.Draw(spriteBatch);

            // Ending sprite batch
            spriteBatch.End();
        }

        /// <summary>
        /// Subprogram to draw the variosu keys
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawKeys(SpriteBatch spriteBatch)
        {
            // Drawing keys
            for (byte i = 0; i < keyBindings.Length; ++i)
            {
                keyBindings[i].Draw(spriteBatch, i == selectedKeyBinding);
            }
        }
    }
}
