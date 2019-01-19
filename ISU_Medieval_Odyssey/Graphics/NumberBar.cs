// Author: Joon Song
// File Name: NumberBar.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/06/2019
// Modified Date: 01/06/2019
// Description: Class to hold NumberBar object - ProgressBar with numbers

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class NumberBar : ProgressBar
    {
        // Variables required to draw NumberBar text
        private string text = string.Empty;
        private Vector2 textLocation;
        private readonly Color textColor;
        private readonly SpriteFont progressFont;

        /// <summary>
        /// Constructor for <see cref="NumberBar"/> object - a <see cref="ProgressBar"/> with numbering
        /// </summary>
        /// <param name="rectangle">The bounding rectangle of the <see cref="NumberBar"/></param>
        /// <param name="maxValue">The maximum value of the <see cref="NumberBar"/></param>
        /// <param name="currentValue">The current value of the <see cref="NumberBar"/></param>
        /// <param name="backColor">The back rectangle color</param>
        /// <param name="progressColor">The progress rectangle color</param>
        /// <param name="progressFont">The progress text font</param>
        /// <param name="textColor">The progress text color</param>
        public NumberBar(Rectangle rectangle, short maxValue, short currentValue, Color backColor, Color progressColor, 
            SpriteFont progressFont, Color textColor) : base(rectangle, maxValue, currentValue, backColor, progressColor)
        {
            // Setting number bar properties
            this.progressFont = progressFont;
            this.textColor = textColor;
        }

        /// <summary>
        /// Update subprogram for <see cref="NumberBar"/> object
        /// </summary>
        public override void Update()
        {
            // Calling ProgressBar update subprogram
            base.Update();

            // Updating number text and its location 
            text = $"{CurrentValue} / {MaxValue}";
            textLocation.X = backRectangle.X + (backRectangle.Width - progressFont.MeasureString(text).X) / 2;
            textLocation.Y = backRectangle.Y + (backRectangle.Height - progressFont.MeasureString(text).Y) / 2 + 3;
        }

        /// <summary>
        /// Draw subprogram for <see cref="NumberBar"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Calling ProgressBar draw subprogram
            base.Draw(spriteBatch);

            // Drawing number text
            spriteBatch.DrawString(progressFont, text, textLocation, textColor);
        }
    }
}
