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
        public override int Value => 3 * defence + durability;
            
        /// <summary>
        /// Whether the <see cref="Armour"/> is broken
        /// </summary>
        public bool IsBroken { get; private set; }

        // Armour functionality related data
        protected int defence;
        protected int durability;

        // Dictionary to map MovementTypes to the appropriate images
        protected Dictionary<MovementType, Texture2D[,]> movementImages = new Dictionary<MovementType, Texture2D[,]>();

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
        /// Subprogram to 'use' the armour
        /// </summary>
        /// <param name="damageAmount">The original damage amount</param>
        /// <returns>The adjusted damage amount</returns>
        public int Defend(int damageAmount)
        {
            // Decrementing durability and making appropraite updates if armour breaks
            if (--durability == 0)
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
        /// <param name="playerRectangle">The corresponding player's rectangle</param>
        /// <param name="movementType">The movement type</param>
        /// <param name="direction">The current direction</param>
        /// <param name="currentFrame">The current frame number</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, MovementType movementType, Direction direction, int currentFrame)
        {
            // Drawing armour
            spriteBatch.Draw(movementImages[movementType][(byte)direction, currentFrame] , playerRectangle, Color.White);
        }
    }
}
