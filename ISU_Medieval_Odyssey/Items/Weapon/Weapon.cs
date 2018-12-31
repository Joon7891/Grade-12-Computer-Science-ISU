// Author: Joon Song
// File Name: Weapon.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/19/2018
// Modified Date: 12/19/2018
// Description: Class to hold Weapon object

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Weapon : Item
    {
        // Base attack damage of the weapon, before modifiers
        public int BaseDamage { get; protected set; }

        // Base attack speed of the weapon, measured in attacks per second
        public double AttackSpeed { get; protected set; }

        /// <summary>
        /// The images of this <see cref="Weapon"/>
        /// </summary>
        public Dictionary<MovementType, Texture2D[,]> WeaponImages { get; private set; } = new Dictionary<MovementType, Texture2D[,]>();
    }
}
