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
using ISU_Medieval_Odyssey.Graphics;
using ISU_Medieval_Odyssey.Utility;

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

        private Player player;
        public Camera Camera { get; private set; }
        private CameraMovement cameraMovement;
        private World world;

        // Screen related variables to map current ScreenMode to appropriate subprograms 
        private readonly ScreenMode screenMode = ScreenMode.MainMenu;
        private delegate void UpdateMethod(GameTime gameTime);
        private delegate void DrawMethod(SpriteBatch spriteBatch);
        private readonly Dictionary<ScreenMode, UpdateMethod> updateMethodDictionary = new Dictionary<ScreenMode, UpdateMethod>();
        private readonly Dictionary<ScreenMode, DrawMethod> drawMethodDictionary = new Dictionary<ScreenMode, DrawMethod>();

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

            // The smaller the orthographic size is, the more zoomed out the camera renders the world at
            Camera = new Camera {OrthographicSize = 0.1f};
            cameraMovement = new CameraMovement();

            world = new World();

            world.AddGenerator(new TerrainWorldGenerator());
            world.Initialize(100, 100);
            Camera.Position = new Vector2((world.Width - 1) / 2 * Chunk.Size * Tile.Size,
                (world.Height - 1) / 2 * Chunk.Size * Tile.Size);

            world.Generate();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Setting up screen method dictionary
            //updateMethodDictionary.Add(ScreenMode.MainMenu, MainMenuScreen.Update);
            //updateMethodDictionary.Add(ScreenMode.Game, GameScreen.Update);
            //updateMethodDictionary.Add(ScreenMode.Settings, SettingsScreen.Update);
            //drawMethodDictionary.Add(ScreenMode.MainMenu, MainMenuScreen.Draw);
            //drawMethodDictionary.Add(ScreenMode.Game, GameScreen.Draw);
            //drawMethodDictionary.Add(ScreenMode.Settings, SettingsScreen.Draw);
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

            // print current tile under mouse
            if (MouseHelper.NewClick())
            {
                Vector2 worldPosition = Camera.ScreenToWorldPoint(NewMouse.Position.ToVector2());
                Tile tile = world.GetTileFromWorldCoordinate(worldPosition);
                if (tile != null)
                {
                    Console.WriteLine($"Tile Type: {tile.Type} ({tile.WorldPosition.X}, {tile.WorldPosition.Y})");
                }
            }

            world.Update();
            cameraMovement.Update(gameTime);
            // Updating appropriate screen
            //updateMethodDictionary[screenMode](gameTime);

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


            // Drawing appropriate screen
            //drawMethodDictionary[screenMode](spriteBatch);

            //player.Draw(spriteBatch);
            world.Draw(spriteBatch, gameTime);

            // Drawing base game
            base.Draw(gameTime);
        }
    }
}
