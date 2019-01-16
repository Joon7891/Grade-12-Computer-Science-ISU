// Author: Joon Song
// File Name: DefensePotion.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/15/2019
// Modified Date: 01/15/2019
// Description: Class to hold DefensePotion object

using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class DefensePotion : Potion
    {
        // DefensePotion specific constants
        private new static Texture2D iconImage;
        private const int TIME_AMOUNT = 30;
        private const int VALUE = 30;
        public const float BOOST_AMOUNT = 0.4f;

        /// <summary>
        /// Static constructor for <see cref="DefensePotion"/> item
        /// </summary>
        static DefensePotion()
        {
            // Importing DefensePotion image
            iconImage = Main.Content.Load<Texture2D>("Images/Sprites/IconImages/defensePotionIcon");
        }

        /// <summary>
        /// Constructor for <see cref="DefensePotion"/> object
        /// </summary>
        public DefensePotion()
        {
            // Setting up defense potion
            base.iconImage = iconImage;
            Value = VALUE;
        }

        /// <summary>
        /// Subprogarm to use a <see cref="DefensePotion"/> <see cref="Item"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using the <see cref="DefensePotion"/></param>
        public override void Use(Player player)
        {
            // Incrementing player defense boost time and calling base use subprogram
            player.DefenseBoostTime += TIME_AMOUNT;
            base.Use(player);
        }
    }
}
