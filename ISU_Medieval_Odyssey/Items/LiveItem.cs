// Author: Joon Song
// File Name: LiveItem.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/16/2019
// Modified Date: 01/16/2019
// Description: Class to hold LiveItem object

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class LiveItem : ICollidable
    {
        /// <summary>
        /// Whether the <see cref="LiveItem"/> is still Live/has not despawned
        /// </summary>
        public bool Live => despawnCounter < DESPAWN_TIME;

        /// <summary>
        /// The <see cref="Item"/> assosiated with this <see cref="LiveItem"/>
        /// </summary>
        public Item Item { get; }

        /// <summary>
        /// The <see cref="Rectangle"/> that this <see cref="LiveItem"/> collides in
        /// </summary>
        public Rectangle HitBox { get; }

        // Time variables to keep track of despawn times
        private const int DESPAWN_TIME = 300;
        private float despawnCounter = 0;

        /// <summary>
        /// Constructor for <see cref="LiveItem"/> object
        /// </summary>
        /// <param name="item">The <see cref="Item"/> assosiated with this <see cref="LiveItem"/></param>
        /// <param name="rectangle">The <see cref="Rectangle"/> to draw this <see cref="LiveItem"/> in</param>
        public LiveItem(Item item, Rectangle rectangle)
        {
            // Setting up object properties
            Item = item;
            HitBox = rectangle;
        }

        /// <summary>
        /// Update subprogam for <see cref="LiveItem"/> object
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Incrementing despawn counter
            despawnCounter += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
        }

        /// <summary>
        /// Draw subprogram for <see cref="LiveItem"/> object
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Drawing the icon of the item
            Item.DrawIcon(spriteBatch, HitBox);
        }
    }
}
