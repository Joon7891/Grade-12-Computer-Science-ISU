// Author: Joon Song, Steven Ung
// File Name: Shoes.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Shoes object

namespace ISU_Medieval_Odyssey
{
    public abstract class Shoes : Armour
    {
        /// <summary>
        /// Subprogram to generate a random <see cref="Shoes"/>
        /// </summary>
        /// <returns>The random <see cref="Shoes"/></returns>
        public static Shoes RandomShoes()
        {
            // Randomly picking a Shoes type
            int randomShoesType = SharedData.RNG.Next(2);

            // Returning new instace of the random Shoes
            switch (randomShoesType)
            {
                case 0:
                    return new LeatherShoes();

                default:
                    return new MetalShoes();
            }
        }
    }
}
