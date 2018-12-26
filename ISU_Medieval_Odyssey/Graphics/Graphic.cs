// Author: Joon Song
// File Name: Graphic.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/25/2018
// Modified Date: 12/25/2018
// Description: Class to hold Graphic object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public abstract class Graphic
    {
        /// <summary>
        /// The rectangle of the graphic
        /// </summary>
        public virtual Rectangle Rectangle { get; set; }

        /// <summary>
        /// The image of the graphic
        /// </summary>
        public Texture2D Image { get;  }

        /// <summary>
        /// Constructor for <see cref="Graphic"/> object
        /// </summary>
        /// <param name="image">The image of the Graphic</param>
        protected Graphic(Texture2D image)
        {
            // Assinging image as object property
            Image = image;
        }

        /// <summary>
        /// Draw subprogram for <see cref="Graphic"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing graphic
            spriteBatch.Draw(Image, Rectangle, Color.White);
        }
    }
}
