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
    public sealed class LiveItem
    {
        public bool Live => despawnCounter < DESPAWN_TIME;

        private const int DESPAWN_TIME = 300;
        private float despawnCounter = 0;

        public Item Item { get; }
        public Rectangle Rectangle { get; }

        public LiveItem(Item item, Rectangle rectangle)
        {
            Item = item;
            Rectangle = rectangle;
        }

        public void Update(GameTime gameTime)
        {
            despawnCounter += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Item.DrawIcon(spriteBatch, Rectangle);
        }
    }
}
