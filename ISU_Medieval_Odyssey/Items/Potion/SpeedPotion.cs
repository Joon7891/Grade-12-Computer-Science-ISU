using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class SpeedPotion : Potion
    {
        // SpeedPotion icon image
        private new static Texture2D iconImage;

        /// <summary>
        /// Static constructor for <see cref="SpeedPotion"/> object
        /// </summary>
        static SpeedPotion()
        {
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/speedPotionIcon");
        }

        public SpeedPotion()
        {
            base.iconImage = iconImage;
        }
    }
}
