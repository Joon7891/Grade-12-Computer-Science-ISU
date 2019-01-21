// Author: Joon Song
// File Name: ThrustWeapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold ThrustWeapon object

using Microsoft.Xna.Framework.Audio;

namespace ISU_Medieval_Odyssey
{
    public abstract class ThrustWeapon : MeleeWeapon
    {
        /// <summary>
        /// The number of frames in <see cref="ThrustWeapon"/> animation
        /// </summary>
        public const int NUM_FRAMES = 8;

        // Thrust sound effect
        private static SoundEffect thrustSoundEffect;

        /// <summary>
        /// Static constructor for <see cref="ThrustWeapon"/>
        /// </summary>
        static ThrustWeapon()
        {
            // Importing thrust sound effect
            thrustSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/thrustSoundEffect");
        }

        /// <summary>
        /// Subprogram to use this <see cref="ThrustWeapon"/>
        /// </summary>
        /// <param name="player"></param>
        public override void Use(Player player)
        {
            // Playing thrusting sound effect
            thrustSoundEffect.CreateInstance().Play();

            // Calling base use subprogram
            base.Use(player);
        }

        /// <summary>
        /// Subprogram to generate and return a random <see cref="ThrustWeapon"/>
        /// </summary>
        /// <returns>The random <see cref="ThrustWeapon"/></returns>
        public static ThrustWeapon RandomThrustWeapon()
        {
            // Randomly picking a thrust weapon type
            int randomThrustWeaponType = SharedData.RNG.Next(3);

            // Returning new instnace of the random thrust weapon
            switch (randomThrustWeaponType)
            {
                case 0:
                    return new Staff();

                case 1:
                    return new Spear();

                default:
                    return new LongSpear();
            }
        }
    }
}
