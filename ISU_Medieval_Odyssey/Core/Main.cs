﻿// Author: Joon Song, Steven Ung
// File Name: Main.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 01/20/2019
// Description: Driver/Main class for Medieval Odyssey game

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ISU_Medieval_Odyssey
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        // Instances of graphics classes for in-game graphics
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Instance of <see cref="ContentManager"/> - used for loading
        /// </summary>
        public new static ContentManager Content { get; private set; }

        /// <summary>
        /// The mouse state of the mouse 1 frame back
        /// </summary>
        public static MouseState OldMouse { get; private set; }

        /// <summary>
        /// The mouse state of the mouse currently
        /// </summary>
        public static MouseState NewMouse { get; private set; }

        /// <summary>
        /// The keyboard state of the keyboard 1 frame back
        /// </summary>
        public static KeyboardState OldKeyboard { get; private set; }

        /// <summary>
        /// The keyboard state of the keyboard currently
        /// </summary>
        public static KeyboardState NewKeyboard { get; private set; }

        /// <summary>
        /// The number of frames per second
        /// </summary>
        public static float FPS { get; private set; }

        /// <summary>
        /// The current screen of the game
        /// </summary>
        public static ScreenMode CurrentScreen { get; set; } = ScreenMode.MainMenu;

        // Current screen mode and dictionary to map screen mode to a IScreen
        private Dictionary<ScreenMode, IScreen> screenDictionary = new Dictionary<ScreenMode, IScreen>();

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content = base.Content;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initializing graphics window size and mouse as visible
            graphics.PreferredBackBufferHeight = SharedData.SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SharedData.SCREEN_WIDTH;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            // Initializing base game
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Setting up screen-related components
            screenDictionary.Add(ScreenMode.MainMenu, new MainMenuScreen());
            screenDictionary.Add(ScreenMode.NewGame, new NewGameScreen());
            screenDictionary.Add(ScreenMode.Game, new GameScreen());
            screenDictionary.Add(ScreenMode.Settings, new SettingsScreen());
            screenDictionary.Add(ScreenMode.HowToPlay, new HowToPlayScreen());

            // Applying volume changes
            SettingsScreen.Instance.ApplyVolumeChanges();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Updating old and new keyboard and mouse states
            OldKeyboard = NewKeyboard;
            OldMouse = NewMouse;
            NewKeyboard = Keyboard.GetState();
            NewMouse = Mouse.GetState();

            // Updating frames per second
            FPS = (float) Math.Round(1000.0f / gameTime.ElapsedGameTime.TotalMilliseconds, 2);

            // Updating current screen
            screenDictionary[CurrentScreen].Update(gameTime);

            // Updating base game
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clearing graphical background to black
            GraphicsDevice.Clear(Color.Black);
            
            // Drawing current screen
            screenDictionary[CurrentScreen].Draw(spriteBatch);

            // Drawing base game
            base.Draw(gameTime);
        }
    }
}
