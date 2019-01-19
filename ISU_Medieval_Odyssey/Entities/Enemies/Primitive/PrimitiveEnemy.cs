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
        protected override Queue<Vector2Int> FindPathToPlayer()
        {
            Vector2Int deltaPosition = Player.Instance.CurrentChunk - CurrentChunk;
            Queue<Vector2Int> path = new Queue<Vector2Int>();
            int currentX = 0;
            float xyRatio;

            if (deltaPosition.ManhattanLength > scanRange)
            {
                return null;
            }

            if (deltaPosition.X == 0)
            {
                for (int i = 0; i <= deltaPosition.Y; ++i)
                {
                    path.Enqueue(CurrentChunk + new Vector2Int(0, i));
                }
            }
            else if (deltaPosition.Y == 0)
            {
                for (int i = 0; i <= deltaPosition.X; ++i)
                {
                    path.Enqueue(CurrentChunk + new Vector2Int(i, 0));
                }
            }
            else
            {
                xyRatio = deltaPosition.X / (float)deltaPosition.Y;

                for (int i = 0; i <= deltaPosition.Y; ++i)
                {
                    path.Enqueue(CurrentChunk + new Vector2Int(i, currentX));

                    while (i * xyRatio >= currentX) // After how many y, should we insert an x?
                    {
                        ++currentX;
                        path.Enqueue(CurrentChunk + new Vector2Int(i, currentX));
                    }
                }
            }

            return path;
        }
    }
}
