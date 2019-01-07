// Author: Joon Song, Steven Ung
// File Name: Armour.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Armour object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        protected ProgressBar durabilityBar; // Encalsuated Max and Current durability

        // Dictionary to map MovementTypes to the appropriate images
        protected readonly Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

        // Various sound effects
        private static SoundEffect breakSoundEffect;

        /// <summary>
        /// Static constructor for <see cref="Armour"/> object
        /// </summary>
        static Armour()
        {
            breakSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/armourBreakSoundEffect");
        }

        /// <summary>
        /// Constructor for <see cref="Armour"/> object
        /// </summary>
        /// <param name="minDefense">The minimum defense of this <see cref="Armour"/></param>
        /// <param name="maxDefense">The maximum defense of this <see cref="Armour"/></param>
        /// <param name="minDurability">The minimum durability of this <see cref="Armour"/></param>
        /// <param name="maxDurability">The maximum durability of this <see cref="Armour"/></param>
        /// <param name="movementImages">The images corresponding to this <see cref="Armour"/>'s movement</param>
        /// <param name="iconImage">The <see cref="Item"/> icon image</param>
        protected Armour(int minDefense, int maxDefense, int minDurability, int maxDurability,
            Dictionary<MovementType, Texture2D[,]> movementImages, Texture2D iconImage) : base(iconImage)
        {
            // Assigning various attributes and images
            int durability = SharedData.RNG.Next(minDurability, maxDurability + 1);
            durabilityBar = new ProgressBar(new Rectangle(0, 0, 50, 5), durability, durability, Color.White * 0.6f, Color.Green * 0.6f);
            durabilityBar.Update();
            defence = SharedData.RNG.Next(minDefense, maxDefense + 1);
            this.movementImages = movementImages;
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
            durabilityBar.Update();

            // Returning the adjusted damage amount - player must take at least 1 HP in damage
            return Math.Max(1, damageAmount - defence);
        }

        /// <summary>
        /// Draw subprogram for Armour object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="playerRectangle">The corresponding player's rectangle</param>
        /// <param name="movementType">The movement type</param>
        /// <param name="direction">The current direction</param>
        /// <param name="currentFrame">The current frame number</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, MovementType movementType, Direction direction, int currentFrame)
        {
            // Drawing armour
            spriteBatch.Draw(movementImages[movementType][(byte)direction, currentFrame] , playerRectangle, Color.White);
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
            durabilityBar.Location = rectangle.Location.ToVector2Int() + new Vector2Int(5, 47);
            durabilityBar.Draw(spriteBatch);
        }
    }
}
