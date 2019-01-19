// Author: Joon Song
// File Name: Torso.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/25/2018
// Modified Date: 12/25/2018
// Description: Class to hold Torso object

namespace ISU_Medieval_Odyssey
{
    public abstract class Torso : Armour
    {
        /// <summary>
        /// Subprogram to generate a random <see cref="Torso"/>
        /// </summary>
        /// <returns>The random <see cref="Torso"/></returns>
        public static Torso RandomTorso()
        {
            // Randomly picking a Shoulders type
            int randomTorsoType = SharedData.RNG.Next(6);

            // Returning new instace of Shoulders
            switch (randomTorsoType)
            {
                // Type-0 returning chain jacket
                case 0:
                    return new ChainJacket();

                // Type-0 returning chain torso
                case 1:
                    return new ChainTorso();

                // Type-0 returning leather shirt
                case 2:
                    return new LeatherShirt();

                // Type-0 returning leather torso
                case 3:
                    return new LeatherTorso();

                // Type-0 returning metal torso
                case 4:
                    return new MetalTorso();

                // Otherwise returning a robe shirt
                default:
                    return new RobeShirt();

            }
        }
    }
}
