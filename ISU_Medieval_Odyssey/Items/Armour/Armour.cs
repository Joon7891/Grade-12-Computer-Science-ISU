// Author: Joon Song, Steven Ung
// File Name: Armour.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Armour object

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Armour : Item
    {
        /// <summary>
        /// The value of the armour
        /// </summary>
        public override int Value => 3 * defence + durabilityBar.CurrentValue;
            
        /// <summary>
        /// Whether the <see cref="Armour"/> is broken
        /// </summary>
        public bool IsBroken { get; private set; }

        // Armour functionality related data
        protected int defence;
        protected ProgressBar durabilityBar;

        // The movement images for this Armour
        protected MovementSpriteSheet movementSpriteSheet;

        // Various sound effects
        private static SoundEffect breakSoundEffect;

        /// <summary>
        /// Static constructor for <see cref="Armour"/> object
        /// </summary>
        static Armour()
        {
            // Loading in various Armour audio
            breakSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/armourBreakSoundEffect");
        }

        /// <summary>
        /// Subprogram to set various <see cref="Armour"/> statistics
        /// </summary>
        /// <param name="minDefense">The minimum allowed defense on this <see cref="Armour"/></param>
        /// <param name="maxDefense">The maximum allowed defense on this <see cref="Armour"/></param>
        /// <param name="minDurability">The minimum allowed durability on this <see cref="Armour"/></param>
        /// <param name="maxDurability"><The minimum allowed durability on this <see cref="Armour"/>/param>
        protected void InitializeArmourStatistics(short minDefense, short maxDefense, short minDurability, short maxDurability)
        {
            // Calculating and assigning various armour statistics
            short durability = (short)SharedData.RNG.Next(minDurability, maxDurability + 1);
            durabilityBar = new ProgressBar(new Rectangle(0, 0, 50, 5), durability, durability, Color.White * 0.6f, Color.Green * 0.6f);
            defence = SharedData.RNG.Next(minDefense, maxDefense + 1);
        }

        /// <summary>
        /// Subprogram to 'use' the armour
        /// </summary>
        /// <param name="damageAmount">The original damage amount</param>
        /// <returns>The adjusted damage amount</returns>
        public int Defend(int damageAmount)
        {
            // Decrementing durability and making appropraite updates if armour breaks
            if (--durabilityBar.CurrentValue == 0)
            {
                breakSoundEffect.CreateInstance().Play();
                IsBroken = true;
            }

            // Returning the adjusted damage amount - player must take at least 1 HP in damage
            return Math.Max(1, damageAmount - defence);
        }

        /// <summary>
        /// Draw subprogram for Armour object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="movementType">The movement type</param>
        /// <param name="direction">The current direction</param>
        /// <param name="currentFrame">The current frame number</param>
        /// <param name="rectangle">The rectangle in which to draw the armour</param>
        public void Draw(SpriteBatch spriteBatch, MovementType movementType, Direction direction, int currentFrame, Rectangle rectangle)
        {
            // Drawing armour
            movementSpriteSheet.Draw(spriteBatch, movementType, direction, currentFrame, rectangle);
        }

        /// <summary>
        /// Subprogram to draw an <see cref="Armour"/> <see cref="Item"/>'s icon
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Sprites</param>
        /// <param name="rectangle">The rectangle to draw the item's icon in</param>
        public override void DrawIcon(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            // Calling base Item draw icon subprogram
            base.DrawIcon(spriteBatch, rectangle);

            // Drawing durability bar
            durabilityBar.X = rectangle.X + 5;
            durabilityBar.Y = rectangle.Y + 47;
            durabilityBar.Draw(spriteBatch);
        }

        /// <summary>
        /// Subprogram to draw information about this <see cref="Armour"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        /// <param name="iconRectangle">The icon's rectangle</param>
        public override void DrawInformation(SpriteBatch spriteBatch, Rectangle iconRectangle)
        {
            // Calling base, adjusting rectangle, and drawing information
            base.DrawInformation(spriteBatch, iconRectangle);
            iconRectangle.X -= 2 * iconRectangle.Width / 3;
            iconRectangle.Y -= 5 * iconRectangle.Height / 2;
            iconRectangle.Width *= 3;
            iconRectangle.Height = 2 * iconRectangle.Height;
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"Defense: {defence}", iconRectangle.Location.ToVector2() + cornerBuffer + 2 * verticalBuffer, Color.Black);
            spriteBatch.DrawString(SharedData.InformationFonts[3], $"Durability: {durabilityBar.CurrentValue}/{durabilityBar.MaxValue}", iconRectangle.Location.ToVector2() + cornerBuffer + 3 * verticalBuffer, Color.Black);
        }

        /// <summary>
        /// Subprogram to generate a random <see cref="Armour"/>
        /// </summary>
        /// <returns>The random <see cref="Armour"/></returns>
        public static Armour RandomArmour()
        {
            // Randomly picking an armour type
            int randomArmourType = SharedData.RNG.Next(6);

            // Returning new instace of the random armour
            switch (randomArmourType)
            {
                case 0:
                    return Belt.RandomBelt();

                case 1:
                    return Head.RandomHead();

                case 2:
                    return Pants.RandomPants();

                case 3:
                    return Shoes.RandomShoes();

                case 4:
                    return Shoulders.RandomShoulders();

                default:
                    return Torso.RandomTorso();
            }
        }
    }
}
