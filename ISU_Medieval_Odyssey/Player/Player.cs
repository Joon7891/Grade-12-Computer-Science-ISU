// Author: Joon Song
// File Name: Player.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Player object

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Player : Entity
    {
        /// <summary>
        /// The size of the player, in pixels
        /// </summary>
        public static byte PIXEL_SIZE = 100;

        /// <summary>
        /// The number of frames in player walk animation
        /// </summary>
        public static int NUM_WALK_FRAMES = 9;

        // Graphics-related data
        private Rectangle rectangle;
        private MovementType movementType = MovementType.Walk;
        private static MovementSpriteSheet movementSpriteSheet;

        // Animation & movement related data
        private float rotation;
        private int frameNumber;
        private int animationCounter;
        private Vector2 nonRoundedLocation;
        private Queue<MovementImageData> imagesToAnimate = new Queue<MovementImageData>();

        // Player item related variables
        private int hotbarSelectionIndex = 0;
        private Weapon currentWeapon;
        private Armour hair = new Hair();
        private ItemSlot[] armourItems = new ItemSlot[6];
        private ItemSlot[] hotbarItems = new ItemSlot[10];
        private static Type[] armourTypeIndexer = { typeof(Shoes), typeof(Pants), typeof(Belt), typeof(Torso), typeof(Shoulders), typeof(Head) };

        // List to hold current projectiles that belong to the player, TODO: THERES PROBABLY A BETTER WAY TO DO THIS
        private List<Projectile> projectiles = new List<Projectile>();

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
            // Loading in movement graphics
            movementSpriteSheet = new MovementSpriteSheet("Images/Sprites/Player/", "player");
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
            experienceBar = new NumberBar(new Rectangle(10, 80, 200, 28), 200, 40, Color.White * 0.5f, 
                Color.SkyBlue * 0.6f, SharedData.InformationFonts[0], Color.Black);
            healthBar = new NumberBar(new Rectangle(10, 135, 200, 28), 200, 100, Color.White * 0.5f,
                Color.Red * 0.6f, SharedData.InformationFonts[0], Color.Black);

            // Constructing player inventory
            for (int i = 0; i < hotbarItems.Length; ++i)
            {
                hotbarItems[i] = new ItemSlot(SharedData.SCREEN_WIDTH / 2 - 5 + (i - 5) * 70, 700);
            }
            for (int i = 0; i < armourItems.Length; ++i)
            {
                armourItems[i] = new ItemSlot(SharedData.SCREEN_WIDTH - 70, 630 - 70 * i);
            }

            // To Remove Later
            hotbarItems[0].Item = new LongSpear();
            hotbarItems[1].Item = new Sword();
            hotbarItems[2].Item = new Bow();
            hotbarItems[3].Item = new HealthPotion();
            armourItems[0].Item = new MetalShoes();
            armourItems[1].Item = new MetalPants();
            armourItems[2].Item = new LeatherBelt();
            armourItems[3].Item = new MetalTorso();
            armourItems[4].Item = new MetalShoulders();
            armourItems[5].Item = new MetalHelmet();
        }

        /// <summary>
        /// Update subprogram for <see cref="Player"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="cameraCenter">The center of the camera that is currenetly pointed at the Player</param>
        public void Update(GameTime gameTime, Vector2 cameraCenter)
        {
            // Calling subprograms to update movement and direction
            UpdateMovement(gameTime);
            UpdateDirection(gameTime, cameraCenter);

            // Updating current tile and chunk coordinates
            CurrentTile = new Vector2Int(Center.X / Tile.HORIZONTAL_SPACING, Center.Y / Tile.VERTICAL_SPACING);
            CurrentChunk = CurrentTile / Chunk.SIZE;

            // Updating status bars
            statisticsLocs[1].X = 60 - SharedData.InformationFonts[0].MeasureString($"Level {Level}").X / 2;
            statisticsLocs[2].X = 160 - SharedData.InformationFonts[0].MeasureString($"{Gold} Gold").X / 2;
            experienceBar.Update();
            healthBar.Update();

            // Calling subprogram to update hotbar
            UpdateInventory(gameTime);

            foreach(Projectile projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
        }

        /// <summary>
        /// Subprogram to update the <see cref="Player"/>'s inventory
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        private void UpdateInventory(GameTime gameTime)
        {
            // Updating hotbar selection if user clicks a hotbar item
            for (byte i = 0; i < hotbarItems.Length; ++i)
            {
                if (MouseHelper.IsRectangleClicked(hotbarItems[i].Rectangle) || KeyboardHelper.IsKeyDown(KeyBindings.HotbarShortcut[i]))
                {
                    hotbarSelectionIndex = i;
                    return;
                }

                // Removing item if it is no longer valid
                if (hotbarItems[i].Item != null && !hotbarItems[i].Item.Valid)
                {
                    hotbarItems[i].Item = null;
                }
            }

            // Updating hotbar selection via scroll
            hotbarSelectionIndex = ((hotbarSelectionIndex - MouseHelper.ScrollAmount()) % (hotbarItems.Length) +
                hotbarItems.Length) % hotbarItems.Length;

            // Using item if user clicks to use it
            if (MouseHelper.NewClick() && hotbarItems[hotbarSelectionIndex].HasItem && imagesToAnimate.Count == 0 && currentWeapon == null)
            {
                UseItem(hotbarItems[hotbarSelectionIndex].Item, gameTime);
            }
        }

        /// <summary>
        /// Subprogram to "use" a certain item
        /// </summary>
        /// <param name="item">Item to be used</param>
        private void UseItem(Item item, GameTime gameTime)
        {
            // Adding appropraite animation frames if player is using a weapon
            if (item is Weapon)
            {
                if (item is SlashWeapon)
                {
                    // Adding slash movement images
                    for (byte i = 0; i < SlashWeapon.NUM_FRAMES; ++i)
                    {
                        imagesToAnimate.Enqueue(new MovementImageData(MovementType.Slash, i));
                    }
                }
                else if (item is ThrustWeapon)
                {
                    // Adding thrust movement iamges
                    for (byte i = 0; i < ThrustWeapon.NUM_FRAMES; ++i)
                    {
                        imagesToAnimate.Enqueue(new MovementImageData(MovementType.Thrust, i));
                    }
                }
                else
                {
                    // Adding shooting movement images
                    for (byte i = 0; i < Bow.NUM_FRAMES; ++i)
                    {
                        imagesToAnimate.Enqueue(new MovementImageData(MovementType.Shoot, i));
                    }

                    projectiles.Add(new Projectile(GetAngle(), 2, 0, this.Center));
                       
                }
                currentWeapon = (Weapon)item;
                frameNumber = 0;
                animationCounter = 0;
            }

            // Using item
            item.Use(this);
        }

        /// <summary>
        /// Subprogram to update the player's movement/location
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        private void UpdateMovement(GameTime gameTime)
        {
            // If weapon is still being used, proceed with weapon animation logic
            if (imagesToAnimate.Count != 0 || currentWeapon != null)
            {
                ++animationCounter;

                // Moving onto next frame every 5 updates
                if (animationCounter % 5 == 0)
                {
                    animationCounter = 0;

                    // If there are no more images left to animate, switch to walking graphics, otherwise animate weapon
                    if (imagesToAnimate.Count == 0)
                    {
                        movementType = MovementType.Walk;
                        currentWeapon = null;
                        frameNumber = 0;
                    }
                    else
                    {
                        movementType = imagesToAnimate.Peek().MovementType;
                        frameNumber = imagesToAnimate.Dequeue().FrameNumber;
                    }
                }
            }

            // Updating player movement if any of the movement keys are down
            if (KeyboardHelper.IsAnyKeyDown(KeyBindings.Up, KeyBindings.Right, KeyBindings.Down, KeyBindings.Left) && currentWeapon == null)
            {
                // Moving player in appropraite direction given movement keystroke
                if (KeyboardHelper.IsKeyDown(KeyBindings.Up))
                {
                    nonRoundedLocation.Y -= Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                }
                if (KeyboardHelper.IsKeyDown(KeyBindings.Down))
                {
                    nonRoundedLocation.Y += Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                }
                if (KeyboardHelper.IsKeyDown(KeyBindings.Left))
                {
                    nonRoundedLocation.X -= Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                }
                if (KeyboardHelper.IsKeyDown(KeyBindings.Right))
                {
                    nonRoundedLocation.X += Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                }

                // Animating movement frames if weapon is not being used
                if (imagesToAnimate.Count == 0 && currentWeapon == null)
                {
                    ++animationCounter;
                    if (animationCounter == 3)
                    {
                        animationCounter = 0;
                        frameNumber = (frameNumber + 1) % NUM_WALK_FRAMES;
                    }
                }
            }
            else if (imagesToAnimate.Count == 0 && currentWeapon == null)
            {
                // Resetting animation and frame counters
                animationCounter = 0;
                frameNumber = 0;
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
            if (currentWeapon == null)
            {
                Direction = (Direction)(2 * rotation / Math.PI % 4);
            }
        }

        /// <summary>
        /// Draw subprogram for <see cref="Player"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="camera">The camaera currently pointed at the player</param>
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            // Drawing player and its corresponding armour and weapon in appropraite sprite batch
            spriteBatch.Begin(transformMatrix: camera.ViewMatrix, samplerState: SamplerState.PointClamp);
            movementSpriteSheet.Draw(spriteBatch, movementType, Direction, frameNumber, rectangle);
            for (int i = 0; i < armourItems.Length; ++i)
            {
                ((Armour)armourItems[i].Item)?.Draw(spriteBatch, movementType, Direction, frameNumber, rectangle);
            }
            if (armourItems[5].Item == null)
            {
                hair.Draw(spriteBatch, movementType, Direction, frameNumber, rectangle);
            }
            currentWeapon?.Draw(spriteBatch, rectangle, Direction, frameNumber);

            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

            spriteBatch.End();

            // Beginning regular sprite batch
            spriteBatch.Begin();

            // Drawing HUD and hobar
            DrawHUD(spriteBatch);
            DrawInventory(spriteBatch);

            // Ending regular sprite batch
            spriteBatch.End();
        }

        /// <summary>
        /// Subprogram to draw the <see cref="Player"/>'s inventory
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        private void DrawInventory(SpriteBatch spriteBatch)
        {
            // Drawing the hotbar
            for (int i = 0; i < hotbarItems.Length; ++i)
            {
                hotbarItems[i].Draw(spriteBatch, i == hotbarSelectionIndex);
            }

            // Drawing the armour items
            for (int i = 0; i < armourItems.Length; ++i)
            {
                armourItems[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Subprogram to inflict damage to the player
        /// </summary>
        /// <param name="damageAmount">The amount of damage to inflict</param>
        public void InflictDamage(int damageAmount)
        {
            // Calculating final damage amount and inflicting it on user
            int finalDamageAmount = damageAmount;
            for (int i = 0; i < armourItems.Length; ++i)
            {
                if (armourItems[i].Item != null)
                {
                    finalDamageAmount = ((Armour)armourItems[i].Item).Defend(finalDamageAmount);
                }
            }
            Health -= finalDamageAmount;

            // Removing broken armour
            for (int i = 0; i < armourItems.Length; ++i)
            {
                if (armourItems[i].Item != null && ((Armour)armourItems[i].Item).IsBroken)
                {
                    armourItems[i].Item = null;
                }
            }
        }

        /// <summary>
        /// Draw subprogram for the <see cref="Player"/>'s heads up display
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        private void DrawHUD(SpriteBatch spriteBatch)
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

        /// <summary>
        /// Calculates angle from player to the mouse
        /// </summary>
        /// <returns> An angle in degrees. </returns>
        private double GetAngle()
        {
            double deltaY = MouseHelper.Location.Y - this.rectangle.Y;
            double deltaX = MouseHelper.Location.X - this.rectangle.X;

            return Math.Atan2(deltaY, deltaX);
        }
    }
}