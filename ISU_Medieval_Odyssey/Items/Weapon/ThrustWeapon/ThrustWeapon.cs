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
