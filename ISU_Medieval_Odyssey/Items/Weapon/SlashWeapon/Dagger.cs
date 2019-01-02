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
        private new static Texture2D[,] directionalImages;

        static Dagger()
        {
            // Temporary strings to help with file paths
            string basePath = "Images/Sprites/Weapon/Slash/Dagger/";
            string weaponTypeName = "dagger";
            directionalImages = EntityHelper.LoadDirectionalImages(basePath, weaponTypeName, SharedData.MovementNumFrames[MovementType.Slash]);
        }

        public Dagger()
        {
            base.directionalImages = directionalImages;
        }
    }
}
