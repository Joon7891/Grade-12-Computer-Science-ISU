// Author: Joon Song, Steven Ung
// File Name: Item.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/18/2018
// Modified Date: 12/18/2018
// Description: Class to hold Item object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Item
    {
        /// <summary>
        /// The value of the item - the price at which it will be purchased at
        /// </summary>
        public virtual int Value { get; protected set; }

        /// <summary>
        /// Whether the item is valid or not - does it still exist
        /// </summary>
        public bool Valid { get; protected set; } = true;

        /// <summary>
        /// Whether the item is the <see cref="Player"/>'s item
        /// </summary>
        public bool IsPlayerItem { get; set; } = true;

        // The image the item's icon
        protected Texture2D iconImage;

        /// <summary>
        /// Subprogram to use this <see cref="Item"/>
        /// </summary>
        /// <param name="player">The player using this particular item</param>
        public virtual void Use(Player player) { }

        /// <summary>
        /// Subprogram to draw the <see cref="Item"/>'s icon
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        /// <param name="rectangle">The rectangle to draw the item's icon in</param>
        public virtual void DrawIcon(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            // Drawing icon
            if (iconImage != null) spriteBatch.Draw(iconImage, rectangle, Color.White);
        }

        /// <summary>
        /// Subprogram to generate and return a random <see cref="Item"/>
        /// </summary>
        /// <returns>The random <see cref="Item"/></returns>
        public static Item RandomItem()
        {
            // Randomly picking an item type
            int randomItemType = SharedData.RNG.Next(3);

            // Returning new instance of item
            switch (randomItemType)
            {
                // Type-0 -< returning a potion 
                case 0:
                    return Potion.RandomPotion();

                // Type-1 -> returning a weapon
                case 1:
                    return Weapon.RandomWeapon();

                // Otherwise, returning an armour
                default:
                    return Armour.RandomArmour();
            }
        }
    }
}
