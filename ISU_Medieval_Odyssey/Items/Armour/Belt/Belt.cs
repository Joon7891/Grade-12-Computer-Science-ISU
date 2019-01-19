// Author: Joon Song, Steven Ung
// File Name: Belt.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Belt object

namespace ISU_Medieval_Odyssey
{
    public abstract class Belt : Armour
    {
        /// <summary>
        /// Subprogram to generate a random <see cref="Belt"/>
        /// </summary>
        /// <returns>The random <see cref="Belt"/></returns>
        public static Belt RandomBelt()
        {
            // Randomly picking a Belt type
            int randomBeltType = SharedData.RNG.Next(2);

            // Returning new instace of the random Belt
            switch (randomBeltType)
            {
                case 0:
                    return new LeatherBelt();

                default:
                    return new RopeBelt();
            }            
        }
    }
}
