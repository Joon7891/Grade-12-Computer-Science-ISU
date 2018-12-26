// Author: Joon Song
// File Name: Sprite.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 09/11/2018
// Modified Date: 09/11/2018
// Description: Class to hold Sprite object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Sprite : Graphic
    {
        /// <summary>
        /// Constructor for <see cref="Sprite"/> object
        /// </summary>
        /// <param name="image">The image of the sprite</param>
        /// <param name="rectangle">The rectangle of the sprite</param>
        public Sprite(Texture2D image, Rectangle rectangle) : base(image)
        {
            // Assinging rectangle as property
            Rectangle = rectangle;
        }
    }
}
