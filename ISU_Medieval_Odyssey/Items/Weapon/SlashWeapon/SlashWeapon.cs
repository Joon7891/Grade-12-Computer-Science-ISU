// Author: Joon Song
// File Name: SlashWeapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 01/19/2018
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
        /// <summary>
        /// The number of frames in this <see cref="SlashWeapon"/>'s animation
        /// </summary>
        public const int NUM_FRAMES = 6;

        // Slashing sound effect
        private static SoundEffect slashSoundEffect;

        /// <summary>
        /// Static constructor for <see cref="SlashWeapon"/>
        /// </summary>
        static SlashWeapon()
        {
            // Importing various slash sound effects
            slashSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/slashSoundEffect");
        }

        /// <summary>
        /// Subprogram to use this <see cref="SlashWeapon"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using this <see cref="SlashWeapon"/></param>
        public override void Use(Player player)
        {
            // Playing slashing sound effect
            slashSoundEffect.CreateInstance().Play();
        }

        /// <summary>
        /// Subprogram to generate and return a random <see cref="SlashWeapon"/>
        /// </summary>
        /// <returns>The random <see cref="SlashWeapon"/></returns>
        public static SlashWeapon RandomSlashWeapon()
        {
            // Randomly picking a slash weapon type
            int randomThrustWeaponType = SharedData.RNG.Next(3);

            // Returning new instnace of slash thrust weapon
            switch (randomThrustWeaponType)
            {
                // Type-0 -> return a dagger
                case 0:
                    return new Dagger();

                // Type-1 -> return a sword
                case 1:
                    return new Sword();

                // Otherwise return a rapier
                default:
                    return new Rapier();
            }
        }
    }
}
