// Author: Joon Song
// File Name: Player.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Player object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Player : Entity
    {
        // Graphics-related data
        private Rectangle rectangle;
        private const byte PIXEL_SIZE = 100;
        private MovementType movementType = MovementType.Walk;
        private static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        // TEST
        Armour test;

        // Movement-related data
        private float rotation;
        private Vector2 nonRoundedLocation;

        int counter;
        int frameNo;

        // Statistics-related variables
        private readonly Vector2[] statisticsLocs =
        {
            new Vector2(15, 15),
            new Vector2(10, 40),
            new Vector2(110, 40),
            new Vector2(60, 60),
            new Vector2(80, 115),
            new Vector2(64, 170)
        };

        /// <summary>
        /// Static constructor to setup various Player components
        /// </summary>
        static Player()
        {
            // Loading in various graphics
            string basePath = "Images/Sprites/Player/";
            string entityTypeName = "player";
            movementImages = EntityHelper.LoadMovementImages(basePath, entityTypeName);
            //Speed = 200;
        }

        /// <summary>
        /// Constructor for <see cref="Player"/> object
        /// </summary>
        public Player(string name)
        {
            // Setting up player rectangle and camera components
            rectangle = new Rectangle(0, 0, PIXEL_SIZE, PIXEL_SIZE);
            colisionRectangle = new Rectangle(PIXEL_SIZE >> 2, PIXEL_SIZE / 5, PIXEL_SIZE >> 1, 4 * PIXEL_SIZE / 5);
            nonRoundedLocation = rectangle.Location.ToVector2();

            // Constructing world coordinate variables
            Center = Vector2Int.Zero;
            CurrentTile = Vector2Int.Zero;
            CurrentChunk = Vector2Int.Zero;

            // Setting up name and other attributes
            Name = name;
            Level = 1;
            statisticsLocs[0].X = 100 - SharedData.InformationFonts[0].MeasureString(name).X / 2;
            experienceBar = new ProgressBar(new Rectangle(10, 80, 200, 28), 200, 40, Color.White * 0.5f, 
                Color.SkyBlue * 0.6f, SharedData.InformationFonts[0], Color.Black);
            healthBar = new ProgressBar(new Rectangle(10, 135, 200, 28), 200, 100, Color.White * 0.5f,
                Color.Red * 0.6f, SharedData.InformationFonts[0], Color.Black);


            test = new RobeSkirt();
        }

        /// <summary>
        /// Update subprogram for <see cref="Player"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="cameraCenter">The center of the camera that is currenetly pointed at the Player</param>
        public void Update(GameTime gameTime, Vector2 cameraCenter)
        {
            ++counter;
            if (counter == 5)
            {
                counter = 0;
                frameNo = (frameNo + 1) % SharedData.MovementNumFrames[movementType];
            }

            // Calling subprograms to update movement and direction
            UpdateMovement(gameTime);
            UpdateDirection(gameTime, cameraCenter);

            // Updating current tile and chunk coordinates
            CurrentTile = new Vector2Int(Center.X / Tile.HORIZONTAL_SPACING, Center.Y / Tile.VERTICAL_SPACING);
            CurrentChunk = CurrentTile / Chunk.SIZE;

            // Updating status bars
            statisticsLocs[1].X = 60 - SharedData.InformationFonts[0].MeasureString($"Level {Level}").X / 2;
            statisticsLocs[2].X = 160 - SharedData.InformationFonts[0].MeasureString($"{Gold} Gold").X / 2;
            experienceBar.Update(gameTime);
            healthBar.Update(gameTime);
        }

        /// <summary>
        /// Subprogram to update the player's movement/location
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        private void UpdateMovement(GameTime gameTime)
        {
            // Updating player location (non-rounded) given appropraite keystroke
            if (KeyboardHelper.IsKeyDown(Keys.W))
            {
                nonRoundedLocation.Y -= Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (KeyboardHelper.IsKeyDown(Keys.S))
            {
                nonRoundedLocation.Y += Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (KeyboardHelper.IsKeyDown(Keys.A))
            {
                nonRoundedLocation.X -= Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (KeyboardHelper.IsKeyDown(Keys.D))
            {
                nonRoundedLocation.X += Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }

            // Updating player coordinate-related variable
            rectangle.X = (int)(nonRoundedLocation.X + 0.5);
            rectangle.Y = (int)(nonRoundedLocation.Y + 0.5);
            colisionRectangle.X = rectangle.X + (PIXEL_SIZE >> 2);
            colisionRectangle.Y = rectangle.Y + PIXEL_SIZE / 5;
            Center = rectangle.Location.ToVector2Int() + (PIXEL_SIZE >> 1);
        }

        /// <summary>
        /// Subprogram to update the player's direction
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="cameraCenter">The center of the camera that is currenetly pointed at the Player</param>
        private void UpdateDirection(GameTime gameTime, Vector2 cameraCenter)
        {
            // Updating player mouse rotation and direction
            rotation = (float)((Math.Atan2(MouseHelper.Location.Y - (Center.Y - cameraCenter.Y), MouseHelper.Location.X - (Center.X - cameraCenter.X)) + 2.75 * Math.PI) % (2 * Math.PI));
            Direction = (Direction)(2 * rotation / Math.PI % 4);
        }

        /// <summary>
        /// Draw subprogram for <see cref="Player"/> object - draw's the player and his armour only
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing player and its corresponding armour
            spriteBatch.Draw(movementImages[movementType][(byte)Direction, frameNo], rectangle, Color.White);
            test.Draw(spriteBatch, rectangle, movementType, Direction, frameNo);
        }

        /// <summary>
        /// Draw subprogram for the <see cref="Player"/>'s heads up display
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void DrawHUD(SpriteBatch spriteBatch)
        {
            // Drawing primitive player properties
            spriteBatch.DrawString(SharedData.InformationFonts[1], Name, statisticsLocs[0], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"Level {Level}", statisticsLocs[1], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], $"{Gold} Gold", statisticsLocs[2], Color.White);
            spriteBatch.DrawString(SharedData.InformationFonts[0], "Experience", statisticsLocs[3], Color.SkyBlue);
            experienceBar.Draw(spriteBatch);
            spriteBatch.DrawString(SharedData.InformationFonts[0], "Health", statisticsLocs[4], Color.Red);
            healthBar.Draw(spriteBatch);
        }
    }
}