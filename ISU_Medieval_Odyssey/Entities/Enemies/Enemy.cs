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

        /// <summary>
        /// The amount of gold this <see cref="Enemy"/> drops
        /// </summary>
        public override int Gold => Experience / 10;

        protected float attackSpeed;
        protected short damageAmount;

        protected int collisionBufferVertical;
        protected int collisionBufferHorizontal;

        private Vector2Int currentTarget;
        private Queue<Vector2Int> pathToPlayer = new Queue<Vector2Int>();

        private float timeToScan = 0;
        private const float MAX_SCAN_INTERVAL = 0.25f; 

        private const int HEALTH_BAR_BUFFER_Y = 25;
        private const int HEALTH_BAR_WIDTH = 90;
        private const int HEALTH_BAR_HEIGHT = 20;
        private int healthBarBufferX;

        protected int scanRange;

        // Graphics-realted data
        protected int numFrames;
        protected int currentFrame = 0;
        protected int counterMax;
        protected int frameCounter = 0;
        protected DirectionalSpriteSheet directionalSpriteSheet;

        // Constants related to random enemy generation
        private const int SKELETON_CHANCE_MAX = 25;
        private const int GOBLIN_CHANCE_MAX = 50;
        private const int ZOMBIE_CHANCE_MAX = 65;
        private const int WIZARD_CHANCE_MAX = 75;
        private const int KNIGHT_CHANCE_MAX = 85;
        private const int WITCH_CHANCE_MAX = 95;
        private const int DRAGON_CHANCE_MAX = 100;

        /// <summary>
        /// Subprogram to initialize various graphical components of this <see cref="Enemy"/>
        /// </summary>
        /// <param name="tileCoordinate">The coordinate of the <see cref="Tile"/> this <see cref="Enemy"/> is to be created at</param>
        /// <param name="width">The width of this <see cref="Enemy"/>, in pixels</param>
        /// <param name="height">The height of this <see cref="Enemy"/>, in pixels</param>
        /// <param name="hitBoxBufferX">The horizontal buffer between this <see cref="Enemy"/>'s rectangle and its hitbox</param>
        /// <param name="hitBoxBufferY">The vertical buffer between this <see cref="Enemy"/>'s rectangle and its hitbox</param>
        /// <param name="numFrames">The number of frames in this <see cref="Enemy"/>'s animation</param>
        /// <param name="counterMax">The maximum value allowed for </param>
        protected void InitializeGraphics(Vector2Int tileCoordinate, int width, int height, int hitBoxBufferX, int hitBoxBufferY, int numFrames, int counterMax)
        {
            // Setting up various rectangle and coordinates
            rectangle = new Rectangle(tileCoordinate.X * Tile.SPACING - width / 2, tileCoordinate.Y * Tile.SPACING - height / 2, width, height);
            hitBox = new Rectangle(rectangle.X + hitBoxBufferX, rectangle.Y + hitBoxBufferY, rectangle.Width - (hitBoxBufferX << 1), rectangle.Height - hitBoxBufferY);
            unroundedLocation = rectangle.Location.ToVector2();
            groundCoordinate.X = rectangle.X + rectangle.Width;
            groundCoordinate.Y = rectangle.Bottom - 1;
            Direction = (Direction)SharedData.RNG.Next(Enum.GetValues(typeof(Direction)).Length);

            // Setting animation-related statistics
            this.numFrames = numFrames;
            this.counterMax = counterMax;
        }

        /// <summary>
        /// Subprogram to set various statistics for this <see cref="Enemy"/>
        /// </summary>
        /// <param name="scanRange">The scan range of this <see cref="Enemy"/></param>
        /// <param name="minHealth">The min health for this <see cref="Enemy"/></param>
        /// <param name="maxHealth">The max health for this <see cref="Enemy"/></param>
        /// <param name="minDamage">The min damage for this <see cref="Enemy"/></param>
        /// <param name="maxDamage">The max damage for this <see cref="Enemy"/></param>
        /// <param name="speed">The speed of this <see cref="Enemy"/></param>
        protected void InitializeStatistics(byte scanRange, int minHealth, int maxHealth, int minDamage, int maxDamage, float speed)
        {
            // Setting up various statistics for enemy 
            short health = (short) SharedData.RNG.Next(minHealth, maxHealth + 1);
            this.scanRange = scanRange;
            damageAmount = (short) SharedData.RNG.Next(minDamage, maxDamage + 1);
            healthBarBufferX = (HEALTH_BAR_WIDTH - rectangle.Width) >> 1;
            healthBar = new NumberBar(new Rectangle(rectangle.X + healthBarBufferX, rectangle.Y - HEALTH_BAR_BUFFER_Y, HEALTH_BAR_WIDTH, HEALTH_BAR_HEIGHT), health, health, Color.White * 0.5f,
                Color.Red * 0.6f, SharedData.InformationFonts[4], Color.Black);
            Speed = speed;
            Experience = (short)(damageAmount + health + 10 * speed + 0.5);
            miniIcon = new Circle(new Vector2Int(), MINI_ICON_RADIUS, Color.Black);
        }

        /// <summary>
        /// Update subprogram for <see cref="Enemy"/> objecy
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public virtual void Update(GameTime gameTime)
        {
            // Scanning for a path to player
            timeToScan += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (timeToScan >= MAX_SCAN_INTERVAL && (CurrentTile - Player.Instance.CurrentTile).ManhattanLength <= scanRange)
            {
                timeToScan = 0;
                pathToPlayer = FindPathToPlayer();

                if (pathToPlayer != null && pathToPlayer.Count > 0)
                {
                    currentTarget = pathToPlayer.Dequeue();
                }
            }

            // Calling subprogram to update movement
            UpdateMovement(gameTime);
        }

        /// <summary>
        /// Subprogram to determine the <see cref="AdvancedEnemy"/>'s path to the <see cref="Player"/>, if it exists
        /// </summary>
        /// <returns>The path to <see cref="Player"/>, if it exists</returns>
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
            // The future location vector of the enemmy
            Vector2Int futurePixelLocation = new Vector2Int();
            Vector2Int futureTileLocation;
            
            // Updating animation/frame counter
            frameCounter = (frameCounter == counterMax) ? 0 : (frameCounter + 1);
            if (frameCounter == 0)
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

                    // Calculating the enemy's future location if it goes up
                    futurePixelLocation.X = center.X;
                    futurePixelLocation.Y = (int)(hitBox.Top - GetPixelSpeed(gameTime) + 0.5);
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Only moving up if there are on barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState)
                    {
                        unroundedLocation.Y -= GetPixelSpeed(gameTime);
                    }
                    break;

                case Direction.Right:

                    // Calculating the enemy's future location if it goes right
                    futurePixelLocation.X = (int)(hitBox.Right + GetPixelSpeed(gameTime) + 0.5);
                    futurePixelLocation.Y = center.Y;
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Only moving right if there are on barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState)
                    {
                        unroundedLocation.Y += GetPixelSpeed(gameTime);
                    }
                    break;

                case Direction.Down:

                    // Calculating the enemy's future location if it goes down
                    futurePixelLocation.X = center.X;
                    futurePixelLocation.Y = (int)(hitBox.Top + GetPixelSpeed(gameTime) + 0.5);
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Only moving down if there are on barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState)
                    {
                        unroundedLocation.Y += GetPixelSpeed(gameTime);
                    }
                    break;

                case Direction.Left:

                    // Calculating the enemy's future location if it goes left
                    futurePixelLocation.X = (int)(hitBox.Right - GetPixelSpeed(gameTime) + 0.5);
                    futurePixelLocation.Y = center.Y;
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Only moving left if there are on barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState)
                    {
                        unroundedLocation.X -= GetPixelSpeed(gameTime);
                    }
                    break;

            }

            // Calculating the enemy's locations
            rectangle.X = (int)(unroundedLocation.X + 0.5);
            rectangle.Y = (int)(unroundedLocation.Y + 0.5);
            hitBox.X = rectangle.X + collisionBufferHorizontal;
            hitBox.Y = rectangle.Y + collisionBufferVertical;
            healthBar.X = rectangle.X - healthBarBufferX;
            healthBar.Y = rectangle.Y - HEALTH_BAR_BUFFER_Y;
            center.X = rectangle.X + (rectangle.Width >> 1);
            center.Y = rectangle.Y + (rectangle.Height >> 1);
            groundCoordinate.X = center.X;
            groundCoordinate.Y = rectangle.Bottom - 1;
            miniIcon.X = center.X;
            miniIcon.Y = center.Y;
        }

        /// <summary>
        /// Subprogram to get the speed of this <see cref="Enemy"/>, in pixels
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <returns>The pixel speed of this enemy</returns>
        private float GetPixelSpeed(GameTime gameTime) => Speed * Tile.SPACING * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

        /// <summary>
        /// Draw subprogram for <see cref="Enemy"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing enemy and healthbar
            directionalSpriteSheet.Draw(spriteBatch, Direction, currentFrame, rectangle);
            healthBar.Draw(spriteBatch);
        }

        public static Enemy RandomEnemy(Vector2Int tileCoordinate)
        {
            // Determing the type of enemy
            int randomEnemyValue = SharedData.RNG.Next(100);

            // Generating and returning appropriate random enemy type
            if (randomEnemyValue < SKELETON_CHANCE_MAX)
            {
                return new Skeleton(tileCoordinate);
            }
            else if (randomEnemyValue < GOBLIN_CHANCE_MAX)
            {
                return new Goblin(tileCoordinate);
            }
            else if (randomEnemyValue < ZOMBIE_CHANCE_MAX)
            {
                return new Zombie(tileCoordinate);
            }
            else if (randomEnemyValue < WIZARD_CHANCE_MAX)
            {
                return new Wizard(tileCoordinate);
            }
            else if (randomEnemyValue < KNIGHT_CHANCE_MAX)
            {
                return new Knight(tileCoordinate);
            }
            else if (randomEnemyValue < WITCH_CHANCE_MAX)
            {
                return new Witch(tileCoordinate);
            }
            else
            {
                return new Dragon(tileCoordinate);
            }
        }
    }
}