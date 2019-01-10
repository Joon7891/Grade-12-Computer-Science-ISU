// Author: Joon Song
// File Name: Potion.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/04/2019
// Modified Date: 01/04/2019
// Description: Class to hold Potion object

using Microsoft.Xna.Framework.Audio;

namespace ISU_Medieval_Odyssey
{
    public abstract class Potion : Item
    {
        // Various Potion specific components
        private static SoundEffect potionSoundEffect;

        /// <summary>
        /// Static constructor to setup various <see cref="Potion"/> components
        /// </summary>
        static Potion()
        {
            // Loading in various potion components
            potionSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/potionSoundEffect");
        }

        /// <summary>
        /// Subprogram to use a <see cref="Potion"/> <see cref="Item"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using the item</param>
        public override void Use(Player player)
        {
            // Playing potion sound effect and marking item as used
            Valid = false;
            potionSoundEffect.CreateInstance().Play();
        }
    }
}
