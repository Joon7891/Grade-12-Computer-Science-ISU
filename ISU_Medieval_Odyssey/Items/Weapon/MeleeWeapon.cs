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
        // The horizontal and vertical hitboxes of using the weapon
        protected Rectangle verticalHitBox;
        protected Rectangle horizontalHitBox;

        /// <summary>
        /// Subprogram to use a <see cref="MeleeWeapon"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> using the weapon</param>
        public override void Use(Player player)
        {
            // The hit box of the melee weapon
            Rectangle adjustedRectangle = new Rectangle(player.Center.X, player.Center.Y, 0, 0);

            // Adjusting hitbox depending on direction
            switch (player.Direction)
            {
                case Direction.Up:
                    adjustedRectangle.Width = verticalHitBox.Width;
                    adjustedRectangle.Height = verticalHitBox.Height;
                    adjustedRectangle.Y -= adjustedRectangle.Height;
                    adjustedRectangle.X -= adjustedRectangle.Width / 2;
                    break;

                case Direction.Right:
                    adjustedRectangle.Width = horizontalHitBox.Width;
                    adjustedRectangle.Height = horizontalHitBox.Height;
                    adjustedRectangle.Y -= adjustedRectangle.Height / 2;
                    break;

                case Direction.Down:
                    adjustedRectangle.Width = verticalHitBox.Width;
                    adjustedRectangle.Height = verticalHitBox.Height;
                    adjustedRectangle.X -= adjustedRectangle.Width / 2;
                    break;

                case Direction.Left:
                    adjustedRectangle.Width = horizontalHitBox.Width;
                    adjustedRectangle.Height = horizontalHitBox.Height;
                    adjustedRectangle.X -= adjustedRectangle.Width;
                    adjustedRectangle.Y -= adjustedRectangle.Height / 2;
                    break;
            }

            // Sending attack action to world
            World.Instance.InflictMeleeDamage(adjustedRectangle, damage * (int)(0.5 + player.AttackBoostTime > 0 ? 1 + AttackPotion.BOOST_AMOUNT : 1), player.Direction);
            
            // Calling base use subprogram
            base.Use(player);
        }
    }
}
