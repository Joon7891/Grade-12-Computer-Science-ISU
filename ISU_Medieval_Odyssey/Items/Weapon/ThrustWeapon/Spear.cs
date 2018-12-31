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
        private new static Texture2D[,] directionalImages;

        static Spear()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Weapon/Thrust/Spear/";
            string weaponTypeName = "spear";
            directionalImages = EntityHelper.LoadDirectionalImages(basePath, weaponTypeName, SharedData.MovementNumFrames[MovementType.Thrust]);
        }

        public Spear()
        {
            base.directionalImages = directionalImages;
        }
    }
}
