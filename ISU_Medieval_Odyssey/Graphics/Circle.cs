// Author: Joon Song
// File Name: Circle.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/06/2018
// Modified Date: 01/06/2018
// Description: Class to hold Circle object - implements IGraphic

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Circle : IGraphic
    {
        /// <summary>
        /// The x-coordinate of the <see cref="Circle"/>'s center
        /// </summary>
        public int X
        {
            get => center.X;
            set
            {
                // Making required x-coordinate updates
                center.X = value;
                rectangle.X = center.X - Radius;
            }
        }

        /// <summary>
        /// The y-coordinate of the <see cref="Circle"/>'s center
        /// </summary>
        public int Y
        {
            get => center.Y;
            set
            {
                // Making required y-coordinate updates
                center.Y = value;
                rectangle.Y = center.Y - Radius;
            }
        }

        /// <summary>
        /// The radius of the <see cref="Circle"/>
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// The diameter of the <see cref="Circle"/>
        /// </summary>
        public int Diameter => Radius * 2;

        // Various graphical variables for the circle
        private static Texture2D image;
        private Rectangle rectangle;
        private Vector2Int center;
        private Color color;

        /// <summary>
        /// Static constructor for <see cref="Circle"/> object
        /// </summary>
        static Circle()
        {
            // Loading in circle image
            image = Main.Content.Load<Texture2D>("Images/Sprites/circleImage");
        }

        /// <summary>
        /// Constructor for <see cref="Circle"/> object
        /// </summary>
        /// <param name="center">The center of the <see cref="Circle"/></param>
        /// <param name="radius">The radius of the <see cref="Circle"/></param>
        /// <param name="color">The color of the circle</param>
        public Circle(Vector2Int center, int radius, Color color)
        {
            // Setting up various cicle attributes
            Radius = radius;
            this.color = color;
            this.center = center;
            rectangle = new Rectangle(center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
        }

        /// <summary>
        /// Draw subprogram for <see cref="Circle"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing circle
            spriteBatch.Draw(image, rectangle, color);
        }
    }
}
