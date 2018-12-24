// Author: Joon Song, Steven Ung
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
        /// Static instance of Main class - used to access Main class properties
        /// </summary>
        public static Main Instance { get; private set; }

        /// <summary>
        /// The mouse state of the mouse 1 frame back
        /// </summary>
        public MouseState OldMouse { get; private set; }

        /// <summary>
        /// The mouse state of the mouse currently
        /// </summary>
        public MouseState NewMouse { get; private set; }

        /// <summary>
        /// The keyboard state of the keyboard 1 frame back
        /// </summary>
        public KeyboardState OldKeyboard { get; private set; }

        /// <summary>
        /// The keyboard state of the keyboard currently
        /// </summary>
        public KeyboardState NewKeyboard { get; private set; }

        // Current screen mode and dictionary to map screen mode to a IScreen
        private ScreenMode currentScreen = ScreenMode.MainMenu;
        private Dictionary<ScreenMode, IScreen> screenDictionary = new Dictionary<ScreenMode, IScreen>();

        private Player test;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Instance = this;
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
            screenDictionary.Add(ScreenMode.Game, new GameScreen());
            screenDictionary.Add(ScreenMode.Settings, new SettingsScreen());
            foreach (ScreenMode screenMode in Enum.GetValues(typeof(ScreenMode)))
            {
                screenDictionary[screenMode].LoadContent();
            }

            test = new Player();
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

            // Updating current screen
            screenDictionary[currentScreen].Update(gameTime);

            test.Update(gameTime);

            // Updating base game
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // Drawing current screen
            screenDictionary[currentScreen].Draw(spriteBatch);

            spriteBatch.Begin();

            test.Draw(spriteBatch);

            spriteBatch.End();


            // Drawing base game
            base.Draw(gameTime);
        }
    }
}
