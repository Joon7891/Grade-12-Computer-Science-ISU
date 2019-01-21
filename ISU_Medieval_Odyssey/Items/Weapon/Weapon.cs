// Author: Joon Song
// File Name: Weapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Weapon object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Weapon : Item
    {
        /// <summary>
        /// The value of the item - the price at which it will be purchased at
        /// </summary>
        public override int Value => damage + durabilityBar.CurrentValue / 2;

        // The directional spritesheet for a weapon
        protected DirectionalSpriteSheet directionalSpriteSheet;

        // Note: Only used for weapons with 196 x 196 - Long Spear, Sword and Rapier
        protected Rectangle adjustedRectangle = new Rectangle(0, 0, 300, 300);

        // Damage, durability, and breaking sound effect
        protected int damage;
        private ProgressBar durabilityBar;
        private static SoundEffect breakSoundEffect;

        /// <summary>
        /// Static constructor for <see cref="Weapon"/>
        /// </summary>
        static Weapon()
        {
            // Importing breaking sound effect
            breakSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/armourBreakSoundEffect");
        }

        /// <summary>
        /// Subprogram to initialize various components of <see cref="Weapon"/>
        /// </summary>
        /// <param name="minDamage">The min damage of this <see cref="Weapon"/></param>
        /// <param name="maxDamage">The max damage of this <see cref="Weapon"/></param>
        /// <param name="minDurability">The min durability of this <see cref="Weapon"/></param>
        /// <param name="maxDurability">The max durability of this <see cref="Weapon"/></param>
        protected void Initialize(int minDamage, int maxDamage, int minDurability, int maxDurability)
        {
            // Setting up various weapon statistics
            short durability = (short)SharedData.RNG.Next(minDurability, maxDurability + 1);
            damage = SharedData.RNG.Next(minDamage, maxDamage + 1);
            durabilityBar = new ProgressBar(new Rectangle(0, 0, 50, 5), durability, durability, Color.White * 0.6f, Color.Green * 0.6f);
        }

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
        /// Subprogram to draw the icon of this <see cref="Weapon"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> to draw this icon in</param>
        public override void DrawIcon(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            // Calling base draw subprogram
            base.DrawIcon(spriteBatch, rectangle);

            // Drawing durability bar
            durabilityBar.X = rectangle.X + 5;
            durabilityBar.Y = rectangle.Y + 47;
            durabilityBar.Draw(spriteBatch);
        }

        /// <summary>
        /// Use subprogram for <see cref="Weapon"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using this <see cref="Weapon"/></param>
        public override void Use(Player player)
        {
            // Updating and breaking weapon if durability drops
            if (--durabilityBar.CurrentValue == 0)
            {
                Valid = false;
                breakSoundEffect.CreateInstance().Play();
            }
        }

        /// <summary>
        /// Subprogram to generate and return a random <see cref="Weapon"/>
        /// </summary>
        /// <returns>The random <see cref="Weapon"/></returns>
        public static Weapon RandomWeapon()
        {
            // Randomly picking a weapon type
            int randomWeaponType = SharedData.RNG.Next(3);

            // Returning new instance of the random weapon
            switch (randomWeaponType)
            {
                case 0:
                    return new Bow();

                case 1:
                    return SlashWeapon.RandomSlashWeapon();

                default:
                    return ThrustWeapon.RandomThrustWeapon();
            }
        }
    }
}
