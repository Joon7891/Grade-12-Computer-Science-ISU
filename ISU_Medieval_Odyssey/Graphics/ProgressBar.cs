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
    public class ProgressBar
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
            set
            {
                backRectangle.Location = value.ToPoint();
                progressRectangle.Location = backRectangle.Location;
            }
        }

        // Variables required to draw back and progress rectangles
        private readonly Color backColor;
        private readonly Color progressColor;
        protected Rectangle backRectangle;
        private Rectangle progressRectangle;

        /// <summary>
        /// Constructor for <see cref="ProgressBar"/> object
        /// </summary>
        /// <param name="rectangle">The bounding rectangle of the <see cref="ProgressBar"/></param>
        /// <param name="maxValue">The maximum value of the <see cref="ProgressBar"/></param>
        /// <param name="currentValue">The current value of the <see cref="ProgressBar"/></param>
        /// <param name="backColor">The back rectangle color</param>
        /// <param name="progressColor">The progress rectangle color</param>
        public ProgressBar(Rectangle rectangle, int maxValue, int currentValue, Color backColor, Color progressColor)
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
        /// Update subprogram for <see cref="ProgressBar"/> object
        /// </summary>
        public virtual void Update()
        {
            // Updating various progress bar components to ensure smooth drawing
            progressRectangle.Location = backRectangle.Location;
            progressRectangle.Width = (int)(backRectangle.Width * (((float)CurrentValue) / MaxValue) + 0.5);        
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
