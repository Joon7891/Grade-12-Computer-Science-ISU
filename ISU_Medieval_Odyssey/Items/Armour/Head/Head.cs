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

            // Returning new instace of Head
            switch (randomHeadType)
            {
                // Type-0 -> return new chain helmet
                case 0:
                    return new ChainHelmet();

                // Type-1 -> return new chain hood
                case 1:
                    return new ChainHood();

                // Type-2 -> return new leather hat
                case 2:
                    return new LeatherHat();

                // Type-3 -> return new metal helmet
                case 3:
                    return new MetalHelmet();

                // Type-4 -> return new robe hood
                default:
                    return new RobeHood();
            }
        }
    }
}
