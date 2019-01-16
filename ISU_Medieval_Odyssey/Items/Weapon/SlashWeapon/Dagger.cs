// Author: Joon Song
// File Name: Dagger.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Dagger object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Dagger : SlashWeapon
    {
        // Dagger specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;
        private new static Texture2D iconImage;

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
        }
    }
}
