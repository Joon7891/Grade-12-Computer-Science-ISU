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
        /// <summary>
        /// The maximum possible value for the progress bar
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// The current value on the progress bar
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// The location of the progress bar
        /// </summary>
        public Vector2Int Location
        {
            get => backRectangle.Location.ToVector2Int();
            set => backRectangle.Location = value.ToPoint();
        }

        // Variables required to draw back and progress rectangles
        private readonly Color backColor;
        private readonly Color progressColor;
        private Rectangle backRectangle;
        private Rectangle progressRectangle;

        // Variables required to draw the progress bar text
        private string text = string.Empty;
        private Vector2 textLocation;
        private readonly Color textColor;
        private readonly SpriteFont progressFont;

        /// <summary>
        /// Constructor for <see cref="ProgressBar"/> object
        /// </summary>
        /// <param name="rectangle">The bounding rectangle of the <see cref="ProgressBar"/></param>
        /// <param name="maxValue">The maximum value of the <see cref="ProgressBar"/></param>
        /// <param name="currentValue">The current value of the <see cref="ProgressBar"/></param>
        /// <param name="backColor">The back rectangle color</param>
        /// <param name="progressColor">The progress rectangle color</param>
        /// <param name="progressFont">The progress text font</param>
        /// <param name="textColor">The progress text color</param>
        /// <param name="textPrefix">The progress text prefix</param>
        public ProgressBar(Rectangle rectangle, int maxValue, int currentValue, Color backColor, 
            Color progressColor, SpriteFont progressFont, Color textColor)
        {
            // Assigning constructor parameters as object properties and fields
            backRectangle = rectangle;
            progressRectangle = rectangle;
            MaxValue = maxValue;
            CurrentValue = currentValue;
            this.backColor = backColor;
            this.progressColor = progressColor;
            this.progressFont = progressFont;
            this.textColor = textColor;
        }

        /// <summary>
        /// Update subprogram for <see cref="ProgressBar"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating various progress bar components to ensure smooth drawing
            progressRectangle.Location = backRectangle.Location;
            progressRectangle.Width = (int)(backRectangle.Width * (((float)CurrentValue) / MaxValue) + 0.5);
            text = $"{CurrentValue} / {MaxValue}";
            textLocation.X = backRectangle.X + (backRectangle.Width - progressFont.MeasureString(text).X) / 2;
            textLocation.Y = backRectangle.Y + (backRectangle.Height - progressFont.MeasureString(text).Y) / 2 + 5;
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
            spriteBatch.DrawString(progressFont, text, textLocation, textColor);
        }
    }
}
