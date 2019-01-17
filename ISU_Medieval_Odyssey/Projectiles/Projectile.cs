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
        public bool Active => distanceTraveled <= maxDistance;

        /// <summary>
        /// The <see cref="Rectangle"/> of this <see cref="Projectile"/>
        /// </summary>
        public Rectangle Rectangle => rectangle;
        protected Rectangle rectangle;

        // Arrow movement variables
        protected Vector2 velocity;
        protected float maxDistance;
        protected Direction direction;
        private float distanceTraveled = 0;
        protected Vector2 nonRoundedLocation;

        // The image of the projectile
        protected Texture2D image;

        /// <summary>
        /// Update subprogram for <see cref="Projectile"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Moving projectile if the projectile is active
            if (Active)
            {
                distanceTraveled += velocity.Length() * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                nonRoundedLocation += Tile.SPACING * velocity * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
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
    }
}
