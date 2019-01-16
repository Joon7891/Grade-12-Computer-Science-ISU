using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class DefensePotion : Potion
    {
        // DefensePotion icon image
        private new static Texture2D iconImage;


        /// <summary>
        /// Static constructor for <see cref="DefensePotion"/> item
        /// </summary>
        static DefensePotion()
        {
            // Importing DefensePotion image
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/defensePotionIcon");
        }

        public DefensePotion()
        {
            base.iconImage = iconImage;
        }
    }
}
