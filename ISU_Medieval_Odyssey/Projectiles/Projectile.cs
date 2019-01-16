using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public abstract class Projectile // TODO: MAKE POSITIONING WORK BY TILE, NOT COORD, UNLOADING
    {
        /// <summary>
        /// Whether this <see cref="Projectile"/> is active
        /// </summary>
        public bool Active => true;

        protected Direction direction;
        protected Vector2 velocity;

        private float distanceTraveled = default(float);
        private float maxDistance;
        protected Rectangle rectangle;
        protected Vector2 nonRoundedLocation;
        protected Texture2D image;

        /// <summary>
        /// Update subprogram for <see cref="Projectile"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                distanceTraveled += velocity.Length() * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                nonRoundedLocation += Tile.VERTICAL_SIZE * velocity * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                rectangle.X = (int)(nonRoundedLocation.X + 0.5);
                rectangle.Y = (int)(nonRoundedLocation.Y + 0.5);
            }
        }

        /// <summary>
        /// Draw subprogram for <see cref="Projectile"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing projectile if it is active
            if (Active)
            {
                spriteBatch.Draw(image, rectangle, Color.White);
            }
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }
    }
}
