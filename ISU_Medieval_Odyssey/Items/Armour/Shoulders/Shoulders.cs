// Author: Joon Song
// File Name: Shoulders.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Shoulders object

namespace ISU_Medieval_Odyssey
{
    public abstract class Shoulders : Armour
    {
        /// <summary>
        /// Subprogram to generate a random <see cref="Shoulders"/>
        /// </summary>
        /// <returns>The random <see cref="Shoulders"/></returns>
        public static Shoulders RandomShoulders()
        {
            // Randomly picking a Shoulders type
            int randomShouldersType = SharedData.RNG.Next(2);

            // Returning new instace of the random Shoulders
            switch (randomShouldersType)
            {
                case 0:
                    return new LeatherShoulders();

                default:
                    return new MetalShoulders();
            }
        }
    }
}
