using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey
{
    public abstract class SmartEnemy : Enemy
    {
        private static Vector2Int[] adjacentMoves =
        {
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(-1, 0)
        };

        protected override Queue<Vector2Int> EvaluateTarget()
        {
            TileNode currentTileNode;
            Vector2Int nextTileCoordinate;
            Queue<TileNode> tilesToEvaluate = new Queue<TileNode>();
            HashSet<Vector2Int> visitedTiles = new HashSet<Vector2Int>();

            tilesToEvaluate.Enqueue(new TileNode(CurrentTile, 0, Direction.None));
            visitedTiles.Add(CurrentTile);

            while (tilesToEvaluate.Count > 0)
            {
                currentTileNode = tilesToEvaluate.Dequeue();

                if (currentTileNode.Coordinate == Player.Instance.CurrentTile)
                {
                    return BuildPath(currentTileNode);
                }
                else if (currentTileNode.Distance > scanRange)
                {
                    tilesToEvaluate.Enqueue(currentTileNode);
                    break;
                }

                for (byte i = 0; i < adjacentMoves.Length; ++i)
                {
                    nextTileCoordinate = currentTileNode.Coordinate + adjacentMoves[i];

                    if (!visitedTiles.Contains(nextTileCoordinate))
                    {
                        visitedTiles.Add(currentTileNode.Coordinate + adjacentMoves[i]);

                        if (!World.Instance.IsTileObstructed(nextTileCoordinate))
                        {
                            tilesToEvaluate.Enqueue(new TileNode(nextTileCoordinate, currentTileNode.Distance + 1, (Direction)i, currentTileNode)); ;
                        }
                    }
                }
            }

            int removalLength = SharedData.RNG.Next(tilesToEvaluate.Count);
            for (int i = 0; i < removalLength; ++i)
            {
                tilesToEvaluate.Dequeue();
            }

            return BuildPath(tilesToEvaluate.Peek());
        }

        private Queue<Vector2Int> BuildPath(TileNode endTileNode)
        {
            Queue<Vector2Int> pathTiles = new Queue<Vector2Int>();
            TileNode currentTileNode = endTileNode;

            while (currentTileNode.Coordinate != CurrentTile)
            {
                pathTiles.Enqueue(currentTileNode.Coordinate);
                currentTileNode = currentTileNode.PreviousNode;
            }

            pathTiles = new Queue<Vector2Int>(pathTiles.Reverse());
            return pathTiles;
        }
    }
}
