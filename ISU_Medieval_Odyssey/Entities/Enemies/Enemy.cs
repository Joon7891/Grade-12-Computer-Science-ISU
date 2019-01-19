// Author: Steven Ung, Joon Song
// File Name: Enemy.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 1/15/2018
// Modified Date: 1/15/2018
// Description: Class to hold Enemy object


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public abstract class Enemy : Entity
    {       

        /// <summary>
        /// Possible loot drops for this <see cref="Enemy"/>
        /// </summary>
        public List<Item> LootTable { get; }

        /// <summary>
        /// The amount of experience this <see cref="Enemy"/> drops/gives to <see cref="Player"/>
        /// </summary>
        public short Experience { get; private set; }

        protected float attackSpeed;
        protected short damageAmount;

        protected int collisionBufferVertical;
        protected int collisionBUfferHorizontal;

        private Vector2Int currentTarget;
        private Queue<Vector2Int> pathToPlayer = new Queue<Vector2Int>();

        private float timeToScan = 0;
        private const float MAX_SCAN_INTERVAL = 1;

        protected int scanRange;

        // Graphics-realted data
        protected int numFrames;
        protected int currentFrame = 0;
        protected int animationCounter = 0;
        protected int maxAnimationCounter;
        protected DirectionalSpriteSheet directionalSpriteSheet;

        protected void Initialize(Vector2Int tileCoordinate, int width, int height, int hitBoxBufferX, int hitBoxBufferY, 
            int minHealth, int maxHealth, int minDamage, int maxDamage, byte numFrames, byte maxAnimationCounter, byte scanRange)
        {
            rectangle = new Rectangle(0, 0, width, height);
            rectangle.X = tileCoordinate.X * Tile.SPACING - width / 2;
            rectangle.Y = tileCoordinate.Y * Tile.SPACING - height / 2;
            unroundedLocation = rectangle.Location.ToVector2();
            groundCoordinate.X = rectangle.X + rectangle.Width;
            groundCoordinate.Y = rectangle.Bottom - 1;

            // Hitbox shit...
            //Health = SharedData.RNG.Next(minHealth, maxHealth);
            damageAmount = (short) SharedData.RNG.Next(minDamage, maxDamage);
            this.numFrames = numFrames;
            this.maxAnimationCounter = maxAnimationCounter;
            this.scanRange = scanRange;
        }

        /// <summary>
        /// Update subprogram for <see cref="Enemy"/> objecy
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            timeToScan += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (timeToScan >= MAX_SCAN_INTERVAL)
            {
                timeToScan = 0;
                pathToPlayer = FindPathToPlayer();

                if (pathToPlayer != null)
                {
                    currentTarget = pathToPlayer.Dequeue();
                }
            }

            Speed = 1;

            // Calling subprogram to update movement
            UpdateMovement(gameTime);

            // Calculating the enemy's locations
            rectangle.X = (int)(unroundedLocation.X + 0.5);
            rectangle.Y = (int)(unroundedLocation.Y + 0.5);
            center.X = rectangle.X + (rectangle.Width >> 1);
            center.Y = rectangle.Y + (rectangle.Height >> 1);
            groundCoordinate.X = center.X;
            groundCoordinate.Y = rectangle.Bottom - 1;
        }

        protected virtual Queue<Vector2Int> FindPathToPlayer()
        {
            return null;
        }

        /// <summary>
        /// Subprogram to update this <see cref="Enemy"/>'s movement
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected virtual void UpdateMovement(GameTime gameTime)
        {
            // Updating animation/frame counter
            animationCounter = (animationCounter == maxAnimationCounter) ? 0 : (animationCounter + 1);
            if (animationCounter == 0)
            {
                currentFrame = (currentFrame + 1) % numFrames;
            }

            if (pathToPlayer != null && currentTarget == CurrentTile && pathToPlayer.Count > 0)
            {
                currentTarget = pathToPlayer.Dequeue();

                if (currentTarget.X - CurrentTile.X == 1)
                {
                    Direction = Direction.Right;
                }
                else if (currentTarget.X - CurrentTile.X == -1)
                {
                    Direction = Direction.Left;
                }
                else if (currentTarget.Y - CurrentTile.Y == -1)
                {
                    Direction = Direction.Up;
                }
                else if (currentTarget.Y - CurrentTile.Y == 1)
                {
                    Direction = Direction.Down;
                }
            }            
            
            // Moving enemy in appropriate direction
            switch (Direction)
            {
                case Direction.Up:
                    unroundedLocation.Y -= GetPixelSpeed(gameTime);

                    break;
                case Direction.Right:
                    unroundedLocation.X += GetPixelSpeed(gameTime);

                    break;

                case Direction.Down:
                    unroundedLocation.Y += GetPixelSpeed(gameTime);

                    break;

                case Direction.Left:
                    unroundedLocation.X -= GetPixelSpeed(gameTime);

                    break;
            }

        }

        private float GetPixelSpeed(GameTime gameTime) => Speed * Tile.SPACING * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

        /// <summary>
        /// Draw subprogram for <see cref="Enemy"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing enemy
            directionalSpriteSheet.Draw(spriteBatch, Direction, currentFrame, rectangle);
        }
    }
}