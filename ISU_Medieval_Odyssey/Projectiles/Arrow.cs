using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public sealed class Arrow : Projectile
    {
        private static Dictionary<Direction, Texture2D> images = new Dictionary<Direction, Texture2D>();
        private const int WIDTH = 50;
        private const int HEIGHT = 5;
        private const int SPEED = 12;
        private const int MAX_DISTANCE = 10;

        /// <summary>
        /// Static constructor for <see cref="Arrow"/> object
        /// </summary>
        static Arrow()
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                images.Add(direction, Main.Content.Load<Texture2D>($"Images/Sprites/Projectiles/Arrow/arrow{direction}"));
            }
        }

        public Arrow(Direction direction, Vector2Int center, Entity shooter)
        {
            image = images[direction];

            if (direction == Direction.Up || direction == Direction.Down)
            {
                velocity = new Vector2(0, direction == Direction.Down ? SPEED : -SPEED);
                rectangle = new Rectangle(center.X - HEIGHT / 2, center.Y - (direction == Direction.Down ? 0 : WIDTH), HEIGHT, WIDTH);
            }
            else
            {
                velocity = new Vector2(direction == Direction.Right ? SPEED : -SPEED, 0);
                rectangle = new Rectangle(center.X - (direction == Direction.Left ? WIDTH : 0), center.Y, WIDTH, HEIGHT);
            }

            nonRoundedLocation = rectangle.Location.ToVector2();
        }
    }
}
