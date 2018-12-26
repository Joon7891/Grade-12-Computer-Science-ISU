// Author: Joon Song
// File Name: Background.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 09/11/2018
// Modified Date: 09/11/2018
// Description: Class to hold Background object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey 
{
    public sealed class Background : Graphic
    {
        /// <summary>
        /// Rectangle for <see cref="Background"/>
        /// </summary>
        public override Rectangle Rectangle => backgroundRectangle;
        private Rectangle backgroundRectangle = new Rectangle(0, 0, SharedData.SCREEN_WIDTH, SharedData.SCREEN_HEIGHT);

        /// <summary>
        /// Constructor for <see cref="Background"/> object
        /// </summary>
        /// <param name="image">The background image</param>
        public Background(Texture2D image) : base(image)
        {
            // Empty
        }
    }
}
