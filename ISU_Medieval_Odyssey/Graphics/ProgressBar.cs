// Author: Joon Song
// File Name: ProgressBar.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/25/2018
// Modified Date: 12/25/2018
// Description: Class to hold ProgressBar object - implements IGraphic

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public class ProgressBar : IGraphic
    {
        /// <summary>
        /// The maximum possible value for the progress bar
        /// </summary>
        public short MaxValue { get; set; }

        /// <summary>
        /// The current value on the progress bar
        /// </summary>
        public short CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = value;
                progressRectangle.Width = (int)(backRectangle.Width * (((float)CurrentValue) / MaxValue) + 0.5);
            }
        }
        private short currentValue;

        /// <summary>
        /// The x coordinate of the top left corner of this <see cref="ProgressBar"/>
        /// </summary>
        public virtual int X
        {
            get => backRectangle.X;
            set
            {
                backRectangle.X = value;
                progressRectangle.X = backRectangle.X;
            }
        }

        /// <summary>
        /// The y coordinate of the top left corner of this <see cref="ProgressBar"/>
        /// </summary>
        public virtual int Y
        {
            get => backRectangle.Y;
            set
            {
                backRectangle.Y = value;
                progressRectangle.Y = backRectangle.Y;
            }
        }

        // Variables required to draw back and progress rectangles
        private readonly Color backColor;
        private readonly Color progressColor;
        protected Rectangle backRectangle;
        protected Rectangle progressRectangle;

        /// <summary>
        /// Constructor for <see cref="ProgressBar"/> object
        /// </summary>
        /// <param name="rectangle">The bounding rectangle of the <see cref="ProgressBar"/></param>
        /// <param name="maxValue">The maximum value of the <see cref="ProgressBar"/></param>
        /// <param name="currentValue">The current value of the <see cref="ProgressBar"/></param>
        /// <param name="backColor">The back rectangle color</param>
        /// <param name="progressColor">The progress rectangle color</param>
        public ProgressBar(Rectangle rectangle, short maxValue, short currentValue, Color backColor, Color progressColor)
        {
            // Assigning constructor parameters as object properties and fields
            backRectangle = rectangle;
            progressRectangle = rectangle;
            MaxValue = maxValue;
            CurrentValue = currentValue;
            this.backColor = backColor;
            this.progressColor = progressColor;
        }

        /// <summary>
        /// Draw subprogram for <see cref="ProgressBar"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Drawing progress bar and text
            spriteBatch.Draw(SharedData.WhiteImage, backRectangle, backColor);
            spriteBatch.Draw(SharedData.WhiteImage, progressRectangle, progressColor);
        }
    }
}
