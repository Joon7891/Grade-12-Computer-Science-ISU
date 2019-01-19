// Author: Joon Song
// File Name: ItemSlot
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/30/2018
// Modified Date: 12/30/2018
// Description: Class to hold ItemSlot object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ItemSlot
    {
        /// <summary>
        /// Whether the <see cref="ItemSlot"/> currently has an <see cref="Item"/>
        /// </summary>
        public bool HasItem => Item != null;

        /// <summary>
        /// The <see cref="Item"/> that is currently contained in the ItemSlot
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// The rectangle that the item slot is drawn in
        /// </summary>
        public Rectangle Rectangle { get; private set; }

        // The item slot related data
        private Color color;
        public const int SIZE = 60;
        private static Texture2D itemSlotImage;

        /// <summary>
        /// Static constructor to setup various <see cref="ItemSlot"/> components
        /// </summary>
        static ItemSlot()
        {
            // Importing item slot graphics
            itemSlotImage = Main.Content.Load<Texture2D>("Images/Sprites/itemSlotImage");
        }

        /// <summary>
        /// Constructor for <see cref="ItemSlot"/> object
        /// </summary>
        /// <param name="x">The x-component of the item slot's location</param>
        /// <param name="y">The y-component of the item slot's </param>
        /// <param name="item">The item that is to be held in the item slot - default is null</param>
        public ItemSlot(int x, int y, Item item = null, Color? color = null)
        {
            // Setting up class attributes
            Rectangle = new Rectangle(x, y, SIZE, SIZE);
            Item = item;
            if (color == null)
            {
                color = Color.White;
            }
            this.color = (Color)color;
        }

        /// <summary>
        /// Subprogram to draw <see cref="ItemSlot"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="isSelected">Whether the item is currently selected - default is false</param>
        public void Draw(SpriteBatch spriteBatch, bool isSelected = false)
        {
            // Drawing item slot and item
            spriteBatch.Draw(itemSlotImage, Rectangle, (isSelected ? Color.SkyBlue : color) * 0.65f);
            Item?.DrawIcon(spriteBatch, Rectangle);
        }

        /// <summary>
        /// Retrives the <see cref="ItemSlot"/>'s <see cref="Item"/> - gets and and removes it
        /// </summary>
        /// <returns>The <see cref="ItemSlot"/>'s <see cref="Item"/></returns>
        public Item RetrieveItem()
        {
            // Removing item and returning it
            Item item = Item;
            Item = null;
            return item;
        }
    }
}
