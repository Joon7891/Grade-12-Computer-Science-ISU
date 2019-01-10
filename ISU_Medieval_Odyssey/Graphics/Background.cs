// Author: Joon Song
// File Name: Background.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 09/11/2018
// Modified Date: 01/11/2018
// Description: Class to hold Background object - implements IGraphic

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey 
{
    public sealed class Background : IGraphic
    {
        // Rectangle and image for Background
        private Texture2D image;
        private static Rectangle backgroundRectangle = new Rectangle(0, 0, SharedData.SCREEN_WIDTH, SharedData.SCREEN_HEIGHT);

        /// <summary>
        /// Constructor for <see cref="Background"/> object
        /// </summary>
        /// <param name="image">The background image</param>
        public Background(Texture2D image)
        {
            // Assinging background image
            this.image = image;
        }

        /// <summary>
        /// Draw subprogram for <see cref="Background"/>
        /// </summary>
        /// <param name="spriteBatch">SpritBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing rectangles
            spriteBatch.Draw(image, backgroundRectangle, Color.White);
        }
    }
}
