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
using ISU_Medieval_Odyssey.Helpers;
using ISU_Medieval_Odyssey.Data_Structures;
using ISU_Medieval_Odyssey.Items.Armour.Shoes;
using ISU_Medieval_Odyssey.Items.Armour.Helmet;
using ISU_Medieval_Odyssey.Items.Armour.Belt;

namespace ISU_Medieval_Odyssey
{
    public sealed class Player
    {
        // Graphics-related data
        private static Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();
        private MovementType movementType = MovementType.Walk;

        // Movement-related data
        private Vector2 playerCenter;
        private double playerMouseRotation;
        private Rectangle rectangle;
        private Vector2 nonRoundedLocation;

        private Direction direction = Direction.Down;
        private const int SPEED = 120;


        private int currentFrame = 0;
        private int counter = 0;



        /// <summary>
        /// Static constructor to setup various Player components
        /// </summary>
        static Player()
        {
            // Loading in player movement images
            string basePath = "Images/Sprites/Player/";
            string entityTypeName = "player";
            movementImages = EntityHelper.LoadMovementImages(basePath, entityTypeName);
        }

        /// <summary>
        /// Constructor for <see cref="Player"/> object
        /// </summary>
        public Player()
        {
            // Setting up player rectangle
            rectangle = new Rectangle(0, 0, 128, 128);
            nonRoundedLocation = new Vector2();
            nonRoundedLocation.X = rectangle.X;
            nonRoundedLocation.Y = rectangle.Y;
        }

        /// <summary>
        /// Update subprogram for <see cref="Player"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Calling subprograms to update movement and direction
            UpdateMovement(gameTime);
            UpdateDirection(gameTime);
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
                nonRoundedLocation.Y -= SPEED * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (KeyboardHelper.IsKeyDown(Keys.S))
            {
                nonRoundedLocation.Y += SPEED * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (KeyboardHelper.IsKeyDown(Keys.A))
            {
                nonRoundedLocation.X -= SPEED * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (KeyboardHelper.IsKeyDown(Keys.D))
            {
                nonRoundedLocation.X += SPEED * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }

            // Updating player rectangle and player center
            rectangle.X = (int)(nonRoundedLocation.X + 0.5);
            rectangle.Y = (int)(nonRoundedLocation.Y + 0.5);
            playerCenter.X = rectangle.X + rectangle.Width / 2;
            playerCenter.Y = rectangle.Y + rectangle.Height / 2;
        }

        /// <summary>
        /// Subprogram to update the player's direction
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        private void UpdateDirection(GameTime gameTime)
        {
            playerMouseRotation = (Math.Atan2(MouseHelper.Location.Y - playerCenter.Y, MouseHelper.Location.X - playerCenter.X) + 2.75 * Math.PI) % (2 * Math.PI);
            direction = (Direction)(2 * playerMouseRotation / Math.PI);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing player and its corresponding armour
            spriteBatch.Draw(movementImages[movementType][(byte)direction, currentFrame], rectangle, Color.White);
        }
    }
}
