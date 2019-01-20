// Author: Joon Song
// File Name: BruteEnemy.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold BruteEnemy object

using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    public abstract class PrimitiveEnemy : Enemy
    {
        /// <summary>
        /// Subprogram to determine the tiles path to <see cref="Player"/>, if it exists
        /// </summary>
        /// <returns>The path to the <see cref="Player"/>, if it exists</returns>
        public override Queue<Vector2Int> FindPathToPlayer()
        {
            // Various variables to determine the player's path
            Vector2Int delta = Player.Instance.CurrentTile - CurrentTile;
            Queue<Vector2Int> path = new Queue<Vector2Int>();
            int unitX = delta.X >= 0 ? 1 : -1;
            int unitY = delta.Y >= 0 ? 1 : -1;
            int xCounter = 1;
            int yCounter = 1;

            // If the player is not within range, return null
            if (delta.ManhattanLength > scanRange)
            {
                return null;
            }

            // Building the path
            path.Enqueue(CurrentTile);
            while (path.Count <= delta.ManhattanLength + 1)
            {
                // Adding a x or y component, depending on how the path is spread out
                if (delta.X / (float)xCounter >= delta.Y / (float)yCounter)
                {
                    path.Enqueue(CurrentTile + new Vector2Int(unitX * (xCounter - 1), unitY * (yCounter - 1)));
                    ++xCounter;
                }
                else
                {
                    path.Enqueue(CurrentTile + new Vector2Int(unitX * (xCounter - 1), unitY * (yCounter - 1)));
                    ++yCounter;
                }
            }

            // Returning the path to the player
            return path;
        }
    }
}
