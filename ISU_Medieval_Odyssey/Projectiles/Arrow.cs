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
        // Variables related to Arrow graphics
        private static Dictionary<Direction, Texture2D> images = new Dictionary<Direction, Texture2D>();
        private const int WIDTH = 50;
        private const int HEIGHT = 5;

        // Movement/speed related consants
        private const int SPEED = 22;
        private const int MAX_DISTANCE = 10;

        /// <summary>
        /// Static constructor for <see cref="Arrow"/> object
        /// </summary>
        static Arrow()
        {
            // Importing arrow graphics
            for (Direction direction = Direction.Up; direction <= Direction.Left; ++direction)
            {
                images.Add(direction, Main.Content.Load<Texture2D>($"Images/Sprites/Projectiles/Arrow/arrow{direction}"));
            }
        }

        /// <summary>
        /// Constructor for <see cref="Arrow"/> object
        /// </summary>
        /// <param name="direction">The <see cref="Direction"/> the arrow is traveling in</param>
        /// <param name="shooter">The <see cref="Entity"/> that is shooting this <see cref="Arrow"/></param>
        public Arrow(Direction direction, Entity shooter)
        {
            // Settingup arrow image
            image = images[direction];

            // Setting up projectile velocity and rectangle
            if (direction == Direction.Up || direction == Direction.Down)
            {
                velocity = new Vector2(0, direction == Direction.Down ? SPEED : -SPEED);
                rectangle = new Rectangle(shooter.Center.X - (HEIGHT >> 1), shooter.Center.Y - (direction == Direction.Down ? 0 : WIDTH), HEIGHT, WIDTH);
            }
            else
            {
                velocity = new Vector2(direction == Direction.Right ? SPEED : -SPEED, 0);
                rectangle = new Rectangle(shooter.Center.X - (direction == Direction.Left ? WIDTH : 0), shooter.Center.Y, WIDTH, HEIGHT);
            }
            nonRoundedLocation = rectangle.Location.ToVector2();
            maxDistance = MAX_DISTANCE;
        }
    }
}
