// Author: Joon Song
// File Name: Spear.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Spear object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Spear : ThrustWeapon
    {
        // Spear specific images
        private new static DirectionalSpriteSheet directionalSpriteSheet;

        /// <summary>
        /// Static constructor for <see cref="Spear"/> object
        /// </summary>
        static Spear()
        {
            // Loading in various Spear images
            directionalSpriteSheet = new DirectionalSpriteSheet("Images/Sprites/Weapon/Thrust/Spear/", "spear", NUM_FRAMES);
        }

        /// <summary>
        /// Constructor for <see cref="Spear"/> object
        /// </summary>
        public Spear()
        {
            // Setting up Spear
            base.directionalSpriteSheet = directionalSpriteSheet;
        }
    }
}
