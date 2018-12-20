// Author: Joon Song, Steven Ung
// File Name: Main.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/17/2018
// Modified Date: 01/20/2019
// Description: Driver/Main class for Medieval Odyssey game

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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Instance of ContentManager; used for loading various content
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

        Player player;

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
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player = new Player();
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

            player.Update(gameTime);

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

            // Beginning spriteBatch
            spriteBatch.Begin();

            player.Draw(spriteBatch);

            // Ending spriteBatch
            spriteBatch.End();

            // Drawing base game
            base.Draw(gameTime);
        }
    }
}
