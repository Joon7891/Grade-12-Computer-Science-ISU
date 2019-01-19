// Author: Joon Song
// File Name: Spear.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Spear object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Spear : ThrustWeapon
    {
        // Spear specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private new static Texture2D iconImage;

        /// <summary>
        /// Static constructor for <see cref="Spear"/> object
        /// </summary>
        static Spear()
        {
            // Loading in various Spear images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Weapon/Thrust/Spear/", "spear", NUM_FRAMES);
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/spearIcon");
        }

        /// <summary>
        /// Constructor for <see cref="Spear"/> object
        /// </summary>
        public Spear()
        {
            // Setting up Spear
            base.directionalSpriteSheet = directionalSpriteSheet;
            base.iconImage = iconImage;
        }
    }
}
