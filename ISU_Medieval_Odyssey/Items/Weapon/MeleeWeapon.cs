// Author: Joon Song
// File Name: MeleeWeapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/19/2018
// Modified Date: 01/19/2018
// Description: Class to hold MeleeWeapon object

using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public abstract class MeleeWeapon : Weapon
    {
        /// <summary>
        /// The vertical hit/attack box of this <see cref="MeleeWeapon"/>
        /// </summary>
        public Rectangle VerticalHitBox { get; private set; }

        /// <summary>
        /// The horizontal hit/attack box of this <see cref="MeleeWeapon"/>
        /// </summary>
        public Rectangle HorizontalHitBox { get; private set; }

        /// <summary>
        /// Subprogram to use a <see cref="MeleeWeapon"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using the weapon</param>
        public override void Use(Player player)
        {
            // Adjusting hitbox and inflicting damage into world with attack hitbox

            
            
            // Calling base use subprogram
            base.Use(player);
        }
    }
}
