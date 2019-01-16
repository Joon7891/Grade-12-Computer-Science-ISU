// Author: Joon Song
// File Name: Slider.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/09/2019
// Modified Date: 01/09/2019
// Description: Class to hold Slider object

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Slider
    {
        /// <summary>
        /// The value as indiciated by the <see cref="Slider"/> - [0, 1]
        /// </summary>
        public float Value { get; set; }

        // Various graphical for drawing the Slider
        private Color backColor;
        private Circle valueCircle;
        private readonly Rectangle rectangle;
        private readonly Circle[] bufferCircles = new Circle[2];

        /// <summary>
        /// Constructor for <see cref="Slider"/> object
        /// </summary>
        /// <param name="rectangle">The background rectangle of the <see cref="Slider"/></param>
        /// <param name="backColor">The background color of the <see cref="Slider"/></param>
        /// <param name="valueColor">The value color of the <see cref="Slider"/></param>
        /// <param name="currentValue">The current value of the slider - zero by default</param>
        public Slider(Rectangle rectangle, Color backColor, Color valueColor, float currentValue)
        {
            int circleRadius = rectangle.Height / 2;
            this.rectangle = rectangle;
            bufferCircles[0] = new Circle(new Vector2Int(rectangle.Left, rectangle.Y + circleRadius), circleRadius, backColor);
            bufferCircles[1] = new Circle(new Vector2Int(rectangle.Right, rectangle.Y + circleRadius), circleRadius, backColor);
            valueCircle = new Circle(new Vector2Int((int)(currentValue * rectangle.Width + 0.5) + rectangle.Left, rectangle.Y + circleRadius), circleRadius, valueColor);
            this.backColor = backColor;
        }

        /// <summary>
        /// Update subprogram for <see cref="Slider"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Updating progress value and circle if slider is clicked
            if (MouseHelper.IsCircleSelected(bufferCircles[0]) || MouseHelper.IsCircleSelected(bufferCircles[1]) || MouseHelper.IsRectangleSelected(rectangle))
            {
                Value = Math.Max(Math.Min((MouseHelper.Location.X - rectangle.Left) / rectangle.Width, 1.0f), 0.0f);
            }
            valueCircle.X = (int)(Value * rectangle.Width + 0.5) + rectangle.Left;
        }

        /// <summary>
        /// Draw subprogarm for <see cref="Slider"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing slider components
            spriteBatch.Draw(SharedData.WhiteImage, rectangle, backColor);
            for (byte i = 0; i < bufferCircles.Length; ++i)
            {
                bufferCircles[i].Draw(spriteBatch);
            }
            valueCircle.Draw(spriteBatch);
        }
    }
}
