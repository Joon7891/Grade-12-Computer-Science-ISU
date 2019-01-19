// Author: Joon Song
// File Name: ThrustWeapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold ThrustWeapon object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class ThrustWeapon : Weapon
    {
        /// <summary>
        /// The number of frames in <see cref="ThrustWeapon"/> animation
        /// </summary>
        public const int NUM_FRAMES = 8;

        /// <summary>
        /// Subprogram to generate and return a random <see cref="ThrustWeapon"/>
        /// </summary>
        /// <returns>The random <see cref="ThrustWeapon"/></returns>
        public static ThrustWeapon RandomThrustWeapon()
        {
            // Randomly picking a thrust weapon type
            int randomThrustWeaponType = SharedData.RNG.Next(3);

            // Returning new instnace of random thrust weapon
            switch (randomThrustWeaponType)
            {
                // Type-0 -> return a new staff
                case 0:
                    return new Staff();

                // Type-1 -> return a new spear
                case 1:
                    return new Spear();

                // Otherwise return a new long spear
                default:
                    return new LongSpear();
            }
        }
    }
}
