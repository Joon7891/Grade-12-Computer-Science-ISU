using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class AttackPotion : Potion
    {
        // AttackPotion icon image
        private new static Texture2D iconImage;

        /// <summary>
        /// Static constructor for <see cref="AttackPotion"/> object
        /// </summary>
        static AttackPotion()
        {
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/attackPotionIcon");
        }

        public AttackPotion()
        {
            base.iconImage = iconImage;
        }
    }
}
