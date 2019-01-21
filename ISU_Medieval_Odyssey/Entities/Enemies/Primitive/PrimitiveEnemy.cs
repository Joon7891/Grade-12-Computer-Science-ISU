// Author: Joon Song
// File Name: BruteEnemy.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold BruteEnemy object

using System;
using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public abstract class PrimitiveEnemy : Enemy
    {
        /// <summary>
        /// Subprogram to update this <see cref="AdvancedEnemy"/> movement
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        protected override void UpdateMovement(GameTime gameTime)
        {
            // Various variables to calculate this enemy's movement
            Vector2Int futurePixelLocation = Vector2Int.Zero;
            Vector2Int futureTileLocation = Vector2Int.Zero;

            // Calling base update movement subprogram
            base.UpdateMovement(gameTime);

            // Moving enemy in appropriate direction
            switch (Direction)
            {
                case Direction.Up:

                    // Calculating the enemy's future location if it goes up
                    futurePixelLocation.X = center.X;
                    futurePixelLocation.Y = (int)(hitBox.Top - GetPixelSpeed(gameTime) + 0.5);
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Moving up if there are no barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState &&
                        !World.Instance.GetTileAt(futureTileLocation).InsideObstructState)
                    {
                        unroundedLocation.Y -= GetPixelSpeed(gameTime);
                    }          
                    break;

                case Direction.Right:

                    // Calculating the enemy's future location if it goes right
                    futurePixelLocation.X = (int)(hitBox.Right + GetPixelSpeed(gameTime) + 0.5);
                    futurePixelLocation.Y = center.Y;
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Moving right if there are no barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState &&
                        !World.Instance.GetTileAt(futureTileLocation).InsideObstructState)
                    {
                        unroundedLocation.X += GetPixelSpeed(gameTime);
                    }
                    break;

                case Direction.Down:

                    // Calculating the enemy's future location if it goes down
                    futurePixelLocation.X = center.X;
                    futurePixelLocation.Y = (int)(hitBox.Bottom + GetPixelSpeed(gameTime) + 0.5);
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Moving down if there are no barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState &&
                        !World.Instance.GetTileAt(futureTileLocation).InsideObstructState)
                    {
                        unroundedLocation.Y += GetPixelSpeed(gameTime);
                    }
                    break;

                case Direction.Left:

                    // Calculating the enemy's future location if it goes left
                    futurePixelLocation.X = (int)(hitBox.Left - GetPixelSpeed(gameTime) + 0.5);
                    futurePixelLocation.Y = center.Y;
                    futureTileLocation = World.PixelToTileCoordinate(futurePixelLocation);

                    // Moving left if there are no barriers
                    if (!World.Instance.GetTileAt(futureTileLocation).OutsideObstructState &&
                        !World.Instance.GetTileAt(futureTileLocation).InsideObstructState)
                    {
                        unroundedLocation.X -= GetPixelSpeed(gameTime);
                    }
                    break;

            }

            // Calling subprogram to calculate prim. enemy locations
            CalculateLocations();
        }
    }
}
