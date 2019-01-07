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

        protected Texture2D iconImage;

        /// <summary>
        /// Constructor for <see cref="Item"/> object
        /// </summary>
        /// <param name="iconImage">The icon image of this <see cref="Item"/></param>
        protected Item(Texture2D iconImage)
        {
            // Assigning icon image
            this.iconImage = iconImage;
        }

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
    }
}
