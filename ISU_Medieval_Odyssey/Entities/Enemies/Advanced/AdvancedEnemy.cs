// Author: Joon Song
// File Name: AdvancedEnemy.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/18/2019
// Description: Class to hold AdvancedEnemy object

using System.Linq;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey
{
    public abstract class AdvancedEnemy : Enemy
    {
        // Various coordinates needed for the advance enemy's movement
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

        /// <summary>
        /// Static constructor for <see cref="AdvancedEnemy"/> object
        /// </summary>
        static AdvancedEnemy()
        {
            // Setting up a diagonal move sequence dictionary
            diagonalMoveSequence = new Dictionary<Vector2Int, Vector2Int[]>();
            diagonalMoveSequence.Add(new Vector2Int(1, 1), new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(1, 0)});
            diagonalMoveSequence.Add(new Vector2Int(-1, -1), new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(-1, 0) });
            diagonalMoveSequence.Add(new Vector2Int(1, -1), new Vector2Int[] { new Vector2Int(1, -1), new Vector2Int(1, 0) });
            diagonalMoveSequence.Add(new Vector2Int(-1, 1), new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0) });
        }

        /// <summary>
        /// Subprogram to determine the <see cref="AdvancedEnemy"/>'s path to the <see cref="Player"/>, if it exists
        /// </summary>
        /// <returns>The path to <see cref="Player"/>, if it exists</returns>
        protected override Queue<Vector2Int> FindPathToPlayer()
        {
            // Various data structures and variables to hold current tile data, visited tiles, and tiles to visit
            Queue<TileNode> tilesToEvaluate = new Queue<TileNode>();
            HashSet<Vector2Int> visitied = new HashSet<Vector2Int>();
            Vector2Int newCoordinate;
            TileNode currentTile;

            // Adding the enemy's initial location
            tilesToEvaluate.Enqueue(new TileNode(CurrentTile, 0));

            // Continuing to evaluate tiles while they exist
            while (tilesToEvaluate.Count > 0)
            {
                // Obtaining current node
                currentTile = tilesToEvaluate.Dequeue();

                // If a path is found, return the path, if the enemy's scan range is exceeded, return null
                if (currentTile.Coordinate == Player.Instance.CurrentTile)
                {
                    return BuildPath(currentTile);
                }
                else if (currentTile.Distance > scanRange)
                {
                    return null;
                }

                // Adding all of the tiles adjacent to the current tile, if appropriate
                for (byte i = 0; i < adjacentMoves.Length; ++i)
                {
                    newCoordinate = currentTile.Coordinate + adjacentMoves[i];
                    if (!visitied.Contains(newCoordinate) && !World.Instance.GetTileAt(newCoordinate).OutsideObstructState)
                    {
                        tilesToEvaluate.Enqueue(new TileNode(newCoordinate, currentTile.Distance + 1, currentTile));
                        visitied.Add(newCoordinate);
                    }
                }

                // Adding all of the tiles diagonal to the current tile, if appropriate
                for (byte i = 0; i < diagonalMoves.Length; ++i)
                {
                    newCoordinate = currentTile.Coordinate + diagonalMoves[i];
                    if (!visitied.Contains(newCoordinate) && !World.Instance.GetTileAt(newCoordinate).OutsideObstructState)
                    {
                        tilesToEvaluate.Enqueue(new TileNode(newCoordinate, currentTile.Distance + 2, currentTile));
                        visitied.Add(newCoordinate);
                    }
                }
            }

            // Otherwise returning null
            return null;
        }

        /// <summary>
        /// Subprogram to build the path from the <see cref="AdvancedEnemy"/>'s current <see cref="Tile"/> to the specified end <see cref="Tile"/>
        /// </summary>
        /// <param name="endTile">The <see cref="Tile"/> representing the end of the path</param>
        /// <returns>A queue holding the in order <see cref="Tile"/> to traverse</returns>
        private Queue<Vector2Int> BuildPath(TileNode endTile)
        {
            // The path, and various objects regarding the current/previous tile
            Queue<Vector2Int> pathTiles = new Queue<Vector2Int>();
            TileNode currentTile = endTile;
            Vector2Int tileDelta;

            // Continuing the add tiles as long as the current tile is not the start tile
            while (currentTile.PreviousNode != null)
            {
                // Calculating the difference between the current and previous path
                tileDelta = currentTile.Coordinate - currentTile.PreviousNode.Coordinate; //what you add to p -> c

                // If the move is non-diagonal, add the path
                if (tileDelta.LengthSquared == 1)
                {
                    pathTiles.Enqueue(currentTile.Coordinate);
                }
                else
                {
                    // Otherwise, add the diagonal move sequences for the diagonal move
                    foreach (Vector2Int move in diagonalMoveSequence[tileDelta])
                    {
                        pathTiles.Enqueue(currentTile.PreviousNode.Coordinate + move);
                    }
                }

                // Traversing to next tile in path
                currentTile = currentTile.PreviousNode;
            }

            // Adding the enemy's current tile and returning the path tiles (in reverse)
            pathTiles.Enqueue(CurrentTile);
            pathTiles = new Queue<Vector2Int>(pathTiles.Reverse());
            return pathTiles;
        }
    }
}
