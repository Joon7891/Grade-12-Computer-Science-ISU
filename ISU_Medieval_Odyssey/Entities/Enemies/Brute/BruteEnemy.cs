// Author: Joon Song
// File Name: BruteEnemy.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold BruteEnemy object

using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public abstract class BruteEnemy : Enemy
    {
        /// <summary>
        /// The size of the brute, in pixels
        /// </summary>
        private static byte PIXEL_SIZE = 70;
        //public static byte VERTICAL_PIXEL_SIZE = 100;

        public void Update(GameTime gameTime)
        {
            
            
            //if (!Aggro)
            //{
            //    int deltaX = Math.Abs(CollisionRectangle.X - GameScreen.Instance.Player.Center.X);
            //    int deltaY = Math.Abs(CollisionRectangle.Y - GameScreen.Instance.Player.Center.Y);
            //    double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            //    if(distance < AGGRO_RANGE)
            //    {
            //        Aggro = true;
            //    }
            //}
            //// chase and attack player
            //if (Aggro)
            //{
            //    int maxMoveX = (int)(Tile.HORIZONTAL_SPACING * Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
            //    int maxMoveY = (int)(Tile.VERTICAL_SPACING * Speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f);

            //    int diffX = CollisionRectangle.X - GameScreen.Instance.Player.Center.X;
            //    int diffY = CollisionRectangle.Y - GameScreen.Instance.Player.Center.Y;

            //    if (diffX > 0)
            //    {
            //        Move(Direction.Left, Math.Max(maxMoveX, Math.Abs(diffX)));
            //    }
            //    else if(0 > diffX)
            //    {
            //        Move(Direction.Right, Math.Max(maxMoveX, Math.Abs(diffX)));
            //    }

            //    if(diffY > 0)
            //    {
            //        Move(Direction.Down, Math.Max(maxMoveY, Math.Abs(diffY)));
            //    }
            //    else if(0 > diffY)
            //    {
            //        Move(Direction.Up, Math.Max(maxMoveY, Math.Abs(diffY)));
            //    }
            //}


            //CurrentTile = new Vector2Int(Center.X / Tile.HORIZONTAL_SPACING, Center.Y / Tile.VERTICAL_SPACING);
        }
    }
}
