// Author: Joon Song
// File Name: Weapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Weapon object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Weapon : Item
    {
        // Base attack damage of the weapon, before modifiers
        public int BaseDamage { get; protected set; }

        // Base attack speed of the weapon, measured in attacks per second
        public double AttackSpeed { get; protected set; }

        protected Texture2D[,] directionalImages;

        // Note: Only used for weapons with 196 x 196 - Long Spear, Long Sword and Rapier
        protected Rectangle adjustedRectangle = new Rectangle(0, 0, 300, 300);

        /// <summary>
        /// Subprogram to draw <see cref="Weapon"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="playerRectangle">The corresponding player's rectangle</param>
        /// <param name="movementType">The movement type</param>
        /// <param name="direction">The current direction</param>
        /// <param name="currentFrame">The current frame number</param>
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, Direction direction, int currentFrame)
        {
            spriteBatch.Draw(directionalImages[(int)direction, currentFrame], playerRectangle, Color.White);
        }
    }
}
