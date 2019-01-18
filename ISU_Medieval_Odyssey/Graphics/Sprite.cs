// Author: Joon Song
// File Name: Sprite.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 09/11/2018
// Modified Date: 01/17/2019
// Description: Class to hold Sprite object - implements IGraphic

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Sprite : IGraphic
    {
        /// <summary>
        /// The x coordinate of the top left corner of this <see cref="Sprite"/>
        /// </summary>
        public int X
        {
            get => rectangle.X;
            set => rectangle.X = value;
        }

        /// <summary>
        /// The y coordinate of the top left corner of this <see cref="Sprite"/>
        /// </summary>
        public int Y
        {
            get => rectangle.Y;
            set => rectangle.Y = value;
        }

        /// <summary>
        /// The width of this <see cref="Sprite"/>
        /// </summary>
        public int Width
        {
            get => rectangle.Width;
            set => rectangle.Width = value;
        }

        /// <summary>
        /// The height of this <see cref="Sprite"/>
        /// </summary>
        public int Height
        {
            get => rectangle.Height;
            set => rectangle.Height = value;
        }

        /// <summary>
        /// The <see cref="rectangle"/> for this <see cref="Sprite"/>
        /// </summary>
        public Rectangle Rectangle => rectangle;

        // Rectangle and image for Sprite
        private Rectangle rectangle;
        private Texture2D image;
        
        /// <summary>
        /// Constructor for <see cref="Sprite"/> object
        /// </summary>
        /// <param name="image">The image of the <see cref="Sprite"/></param>
        /// <param name="rectangle">The rectangle of the <see cref="Sprite"/></param>
        public Sprite(Texture2D image, Rectangle rectangle)
        {
            // Assinging sprite data to fields
            this.image = image;
            this.rectangle = rectangle;
        }

        /// <summary>
        /// Draw subprogram for <see cref="Sprite"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing sprite
            spriteBatch.Draw(image, rectangle, Color.White);
        }
    }
}
