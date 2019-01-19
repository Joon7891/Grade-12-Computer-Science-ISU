// Author: Joon Song
// File Name: Weapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Weapon object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Weapon : Item
    {
        // Base attack damage of the weapon, before modifiers
        public int BaseDamage { get; protected set; }

        // The directional spritesheet for a weapon
        protected DirectionalSpriteSheet directionalSpriteSheet;

        // Note: Only used for weapons with 196 x 196 - Long Spear, Sword and Rapier
        protected Rectangle adjustedRectangle = new Rectangle(0, 0, 300, 300);

        /// <summary>
        /// Subprogram to draw <see cref="Weapon"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="playerRectangle">The corresponding player's rectangle</param>
        /// <param name="direction">The current direction</param>
        /// <param name="currentFrame">The current frame number</param>
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle playerRectangle, Direction direction, int currentFrame)
        {
            // Drawing weapon
            directionalSpriteSheet.Draw(spriteBatch, direction, currentFrame, playerRectangle);
        }

        /// <summary>
        /// Subprogram to generate and return a random <see cref="Weapon"/>
        /// </summary>
        /// <returns>The random <see cref="Weapon"/></returns>
        public static Weapon RandomWeapon()
        {
            // Randomly picking a weapon type
            int randomWeaponType = SharedData.RNG.Next(3);

            // Returning new instance of weapon
            switch (randomWeaponType)
            {
                // Type-0 -> return a bow
                case 0:
                    return new Bow();

                // Type-1 -> return a SlashWeapon
                case 1:
                    return SlashWeapon.RandomSlashWeapon();

                // Default -> return a ThrustWeapon
                default:
                    return ThrustWeapon.RandomThrustWeapon();
            }
        }
    }
}
