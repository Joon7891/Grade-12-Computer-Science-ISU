// Author: Joon Song
// File Name: Dagger.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Dagger object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Dagger : SlashWeapon
    {
        // Dagger specific images and rectangles
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private new static Texture2D iconImage;
        private new static Rectangle verticalHitBox = new Rectangle(0, 0, 25, 50);
        private new static Rectangle horizontalHitBox = new Rectangle(0, 0, 50, 25);

        /// <summary>
        /// Static constructor for <see cref="Dagger"/> object
        /// </summary>
        static Dagger()
        {
            // Loading in various Dagger graphics
            string basePath = "Images/Sprites/Weapon/Slash/Dagger/";
            string weaponTypeName = "dagger";
            directionalSpriteSheet = new DirectionalSpriteSheet(basePath, weaponTypeName, NUM_FRAMES);
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/daggerIcon");
        }

        /// <summary>
        /// Constructor for <see cref="Dagger"/> object
        /// </summary>
        public Dagger()
        {
            // Setting up Dagger
            base.directionalSpriteSheet = directionalSpriteSheet;
            base.iconImage = iconImage;
            base.verticalHitBox = verticalHitBox;
            base.horizontalHitBox = horizontalHitBox;
        }
    }
}
