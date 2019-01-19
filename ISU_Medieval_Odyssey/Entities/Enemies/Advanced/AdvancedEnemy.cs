using System;
using System.Linq;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    public abstract class AdvancedEnemy : Enemy
    {
        private readonly static Vector2Int[] adjacentMoves =
        {
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0),
        };
        private readonly static Vector2Int[] diagonalMoves =
        {
            new Vector2Int(1, 1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1)
        };
        private static Dictionary<Vector2Int, Vector2Int[]> diagonalMoveSequence;

        static AdvancedEnemy()
        {
            diagonalMoveSequence = new Dictionary<Vector2Int, Vector2Int[]>();
            diagonalMoveSequence.Add(new Vector2Int(1, 1), new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(1, 0)});
            diagonalMoveSequence.Add(new Vector2Int(-1, -1), new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(-1, 0) });
            diagonalMoveSequence.Add(new Vector2Int(1, -1), new Vector2Int[] { new Vector2Int(1, -1), new Vector2Int(1, 0) });
            diagonalMoveSequence.Add(new Vector2Int(-1, 1), new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0) });
        }

        protected override Queue<Vector2Int> FindPathToPlayer()
        {
            Queue<TileNode> tilesToEvaluate = new Queue<TileNode>();
            HashSet<Vector2Int> visitied = new HashSet<Vector2Int>();
            TileNode currentTile;
            int currentDistance;

            tilesToEvaluate.Enqueue(new TileNode(CurrentTile, 0));

            while (tilesToEvaluate.Count > 0)
            {
                currentTile = tilesToEvaluate.Dequeue();
                currentDistance = currentTile.Distance;

                if (currentTile.Coordinate == Player.Instance.CurrentTile)
                {
                    return BuildPath(currentTile);
                }
                else if (currentTile.Distance > scanRange)
                {
                    return null;
                }

                for (byte i = 0; i < adjacentMoves.Length; ++i)
                {
                    if (!visitied.Contains(currentTile.Coordinate + adjacentMoves[i]))
                    {
                        tilesToEvaluate.Enqueue(new TileNode(currentTile.Coordinate + adjacentMoves[i], currentDistance + 1, currentTile));
                        visitied.Add(currentTile.Coordinate + adjacentMoves[i]);
                    }
                }
                for (byte i = 0; i < diagonalMoves.Length; ++i)
                {
                    if (!visitied.Contains(currentTile.Coordinate + diagonalMoves[i]))
                    {
                        tilesToEvaluate.Enqueue(new TileNode(currentTile.Coordinate + diagonalMoves[i], currentDistance + 2, currentTile));
                        visitied.Add(currentTile.Coordinate + diagonalMoves[i]);
                    }
                }
            }

            return null;
        }

        private Queue<Vector2Int> BuildPath(TileNode endTile)
        {
            Queue<Vector2Int> pathTiles = new Queue<Vector2Int>();
            TileNode currentTile = endTile;
            Vector2Int tileDelta;

            while (currentTile.PreviousNode != null)
            {
                tileDelta = currentTile.Coordinate - currentTile.PreviousNode.Coordinate; //what you add to p -> c

                if (tileDelta.LengthSquared == 1)
                {
                    pathTiles.Enqueue(currentTile.Coordinate);
                }
                else
                {
                    foreach (Vector2Int move in diagonalMoveSequence[tileDelta])
                    {
                        pathTiles.Enqueue(currentTile.PreviousNode.Coordinate + move);
                    }
                }

                currentTile = currentTile.PreviousNode;
            }

            // Reversing and returning the path
            pathTiles.Enqueue(CurrentTile);
            pathTiles = new Queue<Vector2Int>(pathTiles.Reverse());
            return pathTiles;
        }
    }
}
