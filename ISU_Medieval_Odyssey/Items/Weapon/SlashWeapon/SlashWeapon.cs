// Author: Joon Song
// File Name: SlashWeapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold SlashWeapon object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class SlashWeapon : Weapon
    {
        private static SoundEffect slashSoundEffect;

        static SlashWeapon()
        {
            slashSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/slashSoundEffect");
        }


        public override void Use(Player player)
        {
            slashSoundEffect.CreateInstance().Play();
        }
    }
}
