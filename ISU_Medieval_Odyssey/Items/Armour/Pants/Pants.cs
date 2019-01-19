// Author: Joon Song
// File Name: Pants.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/27/2018
// Modified Date: 12/27/2018
// Description: Class to hold Pants object

namespace ISU_Medieval_Odyssey
{
    public abstract class Pants : Armour
    {
        /// <summary>
        /// Subprogram to generate a random <see cref="Pants"/>
        /// </summary>
        /// <returns>The random <see cref="Pants"/></returns>
        public static Pants RandomPants()
        {
            // Randomly picking a Pants type
            int randomPantsType = SharedData.RNG.Next(3);

            // Returning new instace of Pants
            switch (randomPantsType)
            {
                // Type-0 -> return leather pants
                case 0:
                    return new LeatherPants();

                // Type-1 -> return metal pants
                case 1:
                    return new MetalPants();

                // Otherwise return a robe skirt
                default:
                    return new RobeSkirt();
            }
        }
    }
}
