// Author: Joon Song
// File Name: Head.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Head object

namespace ISU_Medieval_Odyssey
{
    public abstract class Head : Armour
    {
        /// <summary>
        /// Subprogram to generate a random <see cref="Head"/>
        /// </summary>
        /// <returns>The random <see cref="Head"/></returns>
        public static Head RandomHead()
        {
            // Randomly picking a Head type
            int randomHeadType = SharedData.RNG.Next(5);

            // Returning new instace of the random Head
            switch (randomHeadType)
            {
                case 0:
                    return new ChainHelmet();

                case 1:
                    return new ChainHood();

                case 2:
                    return new LeatherHat();

                case 3:
                    return new MetalHelmet();

                default:
                    return new RobeHood();
            }
        }
    }
}
