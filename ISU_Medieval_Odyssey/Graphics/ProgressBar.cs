// Author: Joon Song
// File Name: ProgressBar.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/25/2018
// Modified Date: 12/25/2018
// Description: Class to hold ProgressBar object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class ProgressBar
    {
        //public int CurrentValue { get; set => UpdateData(value, MaxValue, Location); }

        public int MaxValue { get; set; }

        public Vector2 Location
        {
            get => backRectangle.Location.ToVector2();
        }

        private Rectangle backRectangle;
        private Rectangle progressRectangle;

        private readonly Color backColor;
        private readonly Color progressColor;
        private readonly Color progressTextColor;

        private string progressText;
        private Vector2 progressTextLocation;
        private readonly SpriteFont progressFont;

        public ProgressBar()
        {

        }

        public void UpdateData(int currentValue, int maxValue, Vector2 location)
        {
            //progressRectangle.Width = (int)(backRectangle.Width * ((double)CurrentValue / MaxValue) + 0.5);
        }

        /// <summary>
        /// Draw subprogram for <see cref="ProgressBar"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing progress bar and text
            spriteBatch.Draw(SharedData.WhiteImage, backRectangle, backColor);
            spriteBatch.Draw(SharedData.WhiteImage, progressRectangle, progressColor);
            spriteBatch.DrawString(progressFont, progressText, progressTextLocation, progressTextColor);
        }
    }
}
