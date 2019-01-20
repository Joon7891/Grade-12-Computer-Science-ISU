// Author: Joon Song
// File Name: Staff.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 01/19/2018
// Description: Class to hold Staff object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Staff : ThrustWeapon
    {
        // Various Staff specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private new static Texture2D iconImage;
        private new static Rectangle verticalHitBox = new Rectangle(0, 0, 20, 50);
        private new static Rectangle horizontalHitBox = new Rectangle(0, 0, 50, 20);

        /// <summary>
        /// Static constructor for <see cref="Staff"/> object
        /// </summary>
        static Staff()
        {
            // Importing varioius Staff specific images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Weapon/Thrust/Staff/", "staff", NUM_FRAMES);
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/staffIcon");
        }

        /// <summary>
        /// Constructor for <see cref="Staff"/> object
        /// </summary>
        public Staff()
        {
            // Setting up Staff
            base.directionalSpriteSheet = directionalSpriteSheet;
            base.iconImage = iconImage;
            base.verticalHitBox = verticalHitBox;
            base.horizontalHitBox = horizontalHitBox;
        }
    }
}
