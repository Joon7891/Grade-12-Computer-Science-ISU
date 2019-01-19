// Author: Joon Song
// File Name: TileNode.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/18/2019
// Modified Date: 01/19/2019
// Description: Class to hold TileNode struct

namespace ISU_Medieval_Odyssey
{
    public class TileNode
    {
        /// <summary>
        /// The <see cref="Tile"/> coordinate of this <see cref="TileNode"/>
        /// </summary>
        public Vector2Int Coordinate { get; set; }

        /// <summary>
        /// The distance this <see cref="TileNode"/> is from the origin/root
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// The previous <see cref="TileNode"/> to this <see cref="TileNode"/>
        /// </summary>
        public TileNode PreviousNode { get; set; }

        /// <summary>
        /// Constructor for <see cref="TileNode"/> object
        /// </summary>
        /// <param name="coordinate">The <see cref="Tile"/> coordinate of this <see cref="TileNode"/></param>
        /// <param name="distance">The distance this <see cref="TileNode"/> is from the origin/root</param>
        /// <param name="previousNode">The previous <see cref="TileNode"/> to this <see cref="TileNode"/></param>
        public TileNode(Vector2Int coordinate, int distance, TileNode previousNode = null)
        {
            // Setting up TileNode properties
            Coordinate = coordinate;
            Distance = distance;
            PreviousNode = previousNode;
        }
    }
}
