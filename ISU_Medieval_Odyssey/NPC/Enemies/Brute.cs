using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey.NPC
{
    class Brute : Enemy
    {
        /// <summary>
        /// The size of the brute, in pixels
        /// </summary>
        private static byte PIXEL_SIZE = 70;
        //public static byte VERTICAL_PIXEL_SIZE = 100;

        private const double SPEED = 0.8;
        static Texture2D sprite;

        const int AGGRO_RANGE = int.MaxValue;

        // this should be constructed by the world
        public Brute(Vector2 location)
        {
            unroundedLocation = location;

            // Setting up hitbox
            CollisionRectangle = new Rectangle((int)location.X, (int)location.Y, PIXEL_SIZE, PIXEL_SIZE);

            // Constructing world coordinate variables
            Center = Vector2Int.Zero;
            CurrentTile = Vector2Int.Zero;
        }

        public void Update(GameTime gameTime)
        {
            if (!Aggro)
            {
                int deltaX = Math.Abs(CollisionRectangle.X - GameScreen.Instance.Player.Center.X);
                int deltaY = Math.Abs(CollisionRectangle.Y - GameScreen.Instance.Player.Center.Y);
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                if(distance < AGGRO_RANGE)
                {
                    Aggro = true;
                }
            }

            if (Aggro)
            {
                // chase and attack player
            }


            CurrentTile = new Vector2Int(Center.X / Tile.HORIZONTAL_SPACING, Center.Y / Tile.VERTICAL_SPACING);
            CurrentChunk = CurrentTile / Chunk.SIZE;
        }
    }
}
