// Author: Steven Ung, Joon Song
// File Name: Enemy.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 1/15/2018
// Modified Date: 1/15/2018
// Description: Class to hold Enemy object


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        // Various coordinates needed for the advance enemy's movement
        private readonly static Vector2Int[] adjacentMoves =
        {
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
        };
        private readonly static Vector2Int[] diagonalMoves =
        {
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1)
        };
        private static Dictionary<Vector2Int, Vector2Int[]> diagonalMoveSequence;


        /// <summary>
        /// Possible loot drops for this <see cref="Enemy"/>
        /// </summary>
        public HashSet<Item> LootTable { get; protected set; } = new HashSet<Item>();

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

        private float rotationTimer = 0;
        private const float ROTATION_SPEED = 0.10f;

        private float timeToAttack = 0;

        protected int scanRange;

        protected bool isInside; 

        // Graphics-realted data
        protected int numFrames;
        protected int currentFrame = 0;
        protected int counterMax;
        protected int frameCounter = 0;
        protected DirectionalSpriteSheet directionalSpriteSheet;

        private static SoundEffect hitSoundEffect;

        // Constants related to random enemy generation
        private const int SKELETON_CHANCE_MAX = 25;
        private const int GOBLIN_CHANCE_MAX = 50;
        private const int ZOMBIE_CHANCE_MAX = 65;
        private const int WIZARD_CHANCE_MAX = 75;
        private const int KNIGHT_CHANCE_MAX = 85;
        private const int WITCH_CHANCE_MAX = 95;
        private const int DRAGON_CHANCE_MAX = 100;

        /// <summary>
        /// Static constructor for <see cref="Enemy"/> object
        /// </summary>
        static Enemy()
        {
            // Setting up a diagonal move sequence dictionary
            diagonalMoveSequence = new Dictionary<Vector2Int, Vector2Int[]>();
            diagonalMoveSequence.Add(new Vector2Int(1, 1), new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(1, 0) });
            diagonalMoveSequence.Add(new Vector2Int(-1, -1), new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(-1, 0) });
            diagonalMoveSequence.Add(new Vector2Int(1, -1), new Vector2Int[] { new Vector2Int(1, -1), new Vector2Int(1, 0) });
            diagonalMoveSequence.Add(new Vector2Int(-1, 1), new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0) });
            hitSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/enemyHit");
        }

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
        /// <param name="attackSpeed">The speed at which to attack for this <see cref="Enemy"/></param>
        protected void InitializeStatistics(byte scanRange, int minHealth, int maxHealth, int minDamage, int maxDamage, float speed, float attackSpeed)
        {
            // Setting up various statistics for enemy 
            short health = (short) SharedData.RNG.Next(minHealth, maxHealth + 1);
            this.scanRange = scanRange;
            damageAmount = (short) SharedData.RNG.Next(minDamage, maxDamage + 1);
            healthBarBufferX = (HEALTH_BAR_WIDTH - rectangle.Width) >> 1;
            healthBar = new NumberBar(new Rectangle(rectangle.X + healthBarBufferX, rectangle.Y - HEALTH_BAR_BUFFER_Y, HEALTH_BAR_WIDTH, HEALTH_BAR_HEIGHT), health, health, Color.White * 0.5f,
                Color.Red * 0.6f, SharedData.InformationFonts[4], Color.Black);
            Speed = speed;
            this.attackSpeed = attackSpeed;
            Experience = (short)(damageAmount + health + 10 * speed + 0.5);
            miniIcon = new Circle(new Vector2Int(), MINI_ICON_RADIUS, Color.Black);
        }

        /// <summary>
        /// Update subprogram for <see cref="Enemy"/> objecy
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public virtual void Update(GameTime gameTime)
        {
            // Updating animation/frame counter
            frameCounter = (frameCounter == counterMax) ? 0 : (frameCounter + 1);
            if (frameCounter == 0)
            {
                currentFrame = (currentFrame + 1) % numFrames;
            }

            // Scanning for a path to player
            timeToScan += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            timeToAttack += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (timeToScan >= MAX_SCAN_INTERVAL && (CurrentTile - Player.Instance.CurrentTile).ManhattanLength <= scanRange)
            {
                timeToScan = 0;
                pathToPlayer = FindPathToPlayer();

                if (pathToPlayer != null && pathToPlayer.Count > 0)
                {
                    currentTarget = pathToPlayer.Dequeue();
                }
            }

            // Calling subprogram to update movement if enemy is not on top of player
            if (CurrentTile != Player.Instance.CurrentTile)
            {
                UpdateMovement(gameTime);
            }
            else
            {
                // Rotating enemy every 1.5 seconds
                rotationTimer += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                if (rotationTimer >= ROTATION_SPEED)
                {
                    rotationTimer = 0;
                    Direction = (Direction)(((int)Direction + 1) % Enum.GetValues(typeof(Direction)).Length);
                }

                // Attacking player at every attack time
                timeToAttack += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                if (timeToAttack > attackSpeed)
                {
                    timeToAttack = 0;
                    hitSoundEffect.CreateInstance().Play();
                    Player.Instance.InflictDamage(damageAmount);
                }
            }
        }

        /// <summary>
        /// Subprogram to update this <see cref="Enemy"/>'s movement
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected virtual void UpdateMovement(GameTime gameTime)
        {            
            // Moving towards next target if current target has been reached
            if (pathToPlayer != null && currentTarget == CurrentTile && pathToPlayer.Count > 0)
            {
                currentTarget = pathToPlayer.Dequeue();

                // Determining direction
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
        }

        /// <summary>
        /// Subprogram to calculate this <see cref="Enemy"/>'s locations
        /// </summary>
        protected void CalculateLocations()
        {
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
        protected float GetPixelSpeed(GameTime gameTime) => Speed * Tile.SPACING * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

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

        /// <summary>
        /// Subprogram to determine the <see cref="AdvancedEnemy"/>'s path to the <see cref="Player"/>, if it exists
        /// </summary>
        /// <returns>The path to <see cref="Player"/>, if it exists</returns>
        public Queue<Vector2Int> FindPathToPlayer()
        {
            // Various data structures and variables to hold current tile data, visited tiles, and tiles to visit
            Queue<TileNode> tilesToEvaluate = new Queue<TileNode>();
            HashSet<Vector2Int> visitied = new HashSet<Vector2Int>();
            Vector2Int newCoordinate;
            TileNode currentTile;

            // Adding the enemy's initial location
            tilesToEvaluate.Enqueue(new TileNode(CurrentTile, 0));

            // Continuing to evaluate tiles while they exist
            while (tilesToEvaluate.Count > 0)
            {
                // Obtaining current node
                currentTile = tilesToEvaluate.Dequeue();

                // If a path is found, return the path, if the enemy's scan range is exceeded, return null
                if (currentTile.Coordinate == Player.Instance.CurrentTile)
                {
                    return BuildPath(currentTile);
                }
                else if (currentTile.Distance > scanRange)
                {
                    return null;
                }

                // Adding all of the tiles adjacent to the current tile, if appropriate
                for (byte i = 0; i < adjacentMoves.Length; ++i)
                {
                    newCoordinate = currentTile.Coordinate + adjacentMoves[i];
                    if (!visitied.Contains(newCoordinate) && ValidTile(newCoordinate))
                    {
                        tilesToEvaluate.Enqueue(new TileNode(newCoordinate, currentTile.Distance + 1, currentTile));
                        visitied.Add(newCoordinate);
                    }
                }

                // Adding all of the tiles diagonal to the current tile, if appropriate
                for (byte i = 0; i < diagonalMoves.Length; ++i)
                {
                    newCoordinate = currentTile.Coordinate + diagonalMoves[i];
                    if (!visitied.Contains(newCoordinate) && ValidTile(newCoordinate))
                    {
                        tilesToEvaluate.Enqueue(new TileNode(newCoordinate, currentTile.Distance + 2, currentTile));
                        visitied.Add(newCoordinate);
                    }
                }
            }

            // Otherwise returning null
            return null;
        }

        /// <summary>
        /// Subprogram to build the path from the <see cref="AdvancedEnemy"/>'s current <see cref="Tile"/> to the specified end <see cref="Tile"/>
        /// </summary>
        /// <param name="endTile">The <see cref="Tile"/> representing the end of the path</param>
        /// <returns>A queue holding the in order <see cref="Tile"/> to traverse</returns>
        private Queue<Vector2Int> BuildPath(TileNode endTile)
        {
            // The path, and various objects regarding the current/previous tile
            Queue<Vector2Int> pathTiles = new Queue<Vector2Int>();
            TileNode currentTile = endTile;
            Vector2Int tileDelta;

            // Continuing the add tiles as long as the current tile is not the start tile
            while (currentTile.PreviousNode != null)
            {
                // Calculating the difference between the current and previous path
                tileDelta = currentTile.Coordinate - currentTile.PreviousNode.Coordinate; //what you add to p -> c

                // If the move is non-diagonal, add the path
                if (tileDelta.ManhattanLength == 1)
                {
                    pathTiles.Enqueue(currentTile.Coordinate);
                }
                else
                {
                    // Otherwise, add the diagonal move sequences for the diagonal move
                    foreach (Vector2Int move in diagonalMoveSequence[tileDelta])
                    {
                        pathTiles.Enqueue(currentTile.PreviousNode.Coordinate + move);
                    }
                }

                // Traversing to next tile in path
                currentTile = currentTile.PreviousNode;
            }

            // Adding the enemy's current tile and returning the path tiles (in reverse)
            pathTiles.Enqueue(CurrentTile);
            pathTiles = new Queue<Vector2Int>(pathTiles.Reverse());
            return pathTiles;
        }

        /// <summary>
        /// Subprogram to determine if a <see cref="Tile"/> is valid to traverse to
        /// </summary>
        /// <param name="tileCoordinate">The <see cref="Tile"/> coordinate</param>
        /// <returns>Whether a <see cref="Tile"/> is a valid <see cref="Tile"/></returns>
        private bool ValidTile(Vector2Int tileCoordinate)
        {
            // If the tile in question is not valid, return false
            if (World.Instance.GetTileAt(tileCoordinate).OutsideObstructState)
            {
                return false;
            }

            if(isInside && World.Instance.GetTileAt(tileCoordinate).InsideObstructState)
            {
                return false;
            }

            // If any of the adjacent tiles aren't valid, return false
            for (byte i = 0; i < adjacentMoves.Length; ++i)
            {
                if (World.Instance.IsTileObstructed(tileCoordinate + adjacentMoves[i], true))
                {
                    return false;
                }
            }

            // Otherwise return true
            return true;
        }

        /// <summary>
        /// Subprogram to generate a random <see cref="Enemy"/>
        /// </summary>
        /// <param name="tileCoordinate">The <see cref="Tile"/> coordinate at which to generate the enemy</param>
        /// <param name="isInside"> If the enemy is inside a building or not >
        /// <returns>The random <see cref="Enemy"/></returns>
        public static Enemy RandomEnemy(Vector2Int tileCoordinate, bool isInside)
        {
            // Determing the type of enemy
            int randomEnemyValue = SharedData.RNG.Next(100);

            // Generating and returning appropriate random enemy type
            if (randomEnemyValue < SKELETON_CHANCE_MAX)
            {
                return new Skeleton(tileCoordinate, isInside);
            }
            else if (randomEnemyValue < GOBLIN_CHANCE_MAX)
            {
                return new Goblin(tileCoordinate, isInside);
            }
            else if (randomEnemyValue < ZOMBIE_CHANCE_MAX)
            {
                return new Zombie(tileCoordinate, isInside);
            }
            else if (randomEnemyValue < WIZARD_CHANCE_MAX)
            {
                return new Wizard(tileCoordinate, isInside);
            }
            else if (randomEnemyValue < KNIGHT_CHANCE_MAX)
            {
                return new Knight(tileCoordinate, isInside);
            }
            else if (randomEnemyValue < WITCH_CHANCE_MAX)
            {
                return new Witch(tileCoordinate, isInside);
            }
            else
            {
                return new Dragon(tileCoordinate, isInside);
            }
        }
    }
}