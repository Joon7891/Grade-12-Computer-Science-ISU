// Author: Joon Song
// File Name: Bow.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Bow object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Bow : Weapon
    {
        /// <summary>
        /// The number of frames in <see cref="Bow"/> weapon's animation
        /// </summary>
        public const int NUM_FRAMES = 10;

        // Bow & Arrow specific images and audio
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private static DirectionalSpriteSheet arrowSpriteSheet;
        private new static Texture2D iconImage;
        private static SoundEffect shootSoundEffect;

        // Various constants for Sword components
        private const int MIN_DAMAGE = 10;
        private const int MAX_DAMAGE = 100;
        private const int MIN_DURABILITY = 50;
        private const int MAX_DURABILITY = 300;

        /// <summary>
        /// Static constructor for <see cref="Bow"/> object
        /// </summary>
        static Bow()
        {
            // Loading in various Bow images and sound effects
            string basePath = "Images/Sprites/Weapon/Shoot/";
            string weaponTypeName = "bow";
            directionalSpriteSheet = new DirectionalSpriteSheet($"{basePath}Bow/", weaponTypeName, NUM_FRAMES);
            arrowSpriteSheet = new DirectionalSpriteSheet($"{basePath}Arrow/", "arrow", NUM_FRAMES);
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/bowIcon");
            shootSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/bowSoundEffect");
        }

        /// <summary>
        /// Constructor for <see cref="Bow"/> object
        /// </summary>
        public Bow()
        {
            // Setting up Bow
            itemName = "Bow";
            base.directionalSpriteSheet = directionalSpriteSheet;
            base.iconImage = iconImage;
            Initialize(MIN_DAMAGE, MAX_DAMAGE, MIN_DURABILITY, MAX_DURABILITY);
        }

        /// <summary>
        /// Draw subprogram for <see cref="Bow"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="rectangle">The rectangle to draw the <see cref="Bow"/> in</param>
        /// <param name="direction">The direction the <see cref="Bow"/> is pointed at</param>
        /// <param name="currentFrame">The current frame of the <see cref="Bow"/>'s animation</param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Direction direction, int currentFrame)
        {
            base.Draw(spriteBatch, rectangle, direction, currentFrame);
            arrowSpriteSheet.Draw(spriteBatch, direction, currentFrame, rectangle);
            Initialize(MIN_DAMAGE, MAX_DAMAGE, MIN_DURABILITY, MAX_DURABILITY);
        }

        /// <summary>
        /// Subprogram to use this <see cref="Bow"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using this <see cref="Bow"/></param>
        public override void Use(Player player)
        {
            // Calling base use subprogram
            base.Use(player);

            // Adding projectile to world and making sound effect
            shootSoundEffect.CreateInstance().Play();
            World.Instance.AddProjectile(new Arrow(player.Direction, player, damage * (int)(0.5 + player.AttackBoostTime > 0 ? 1 + AttackPotion.BOOST_AMOUNT : 1)));
        }
    }
}