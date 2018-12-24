// Author: Joon Song
// File Name: TileHeightMap.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/24/2018
// Modified Date: 12/24/2018
// Description: TileHeightMap structure - maps a height to a tile type

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    public struct TileHeightMap
    {
        /// <summary>
        /// The max-height the corresponding tile type can have
        /// </summary>
        public float MaxHeight { get; }

        /// <summary>
        /// The tile type that the height maps to
        /// </summary>
        public TileType Type { get; }

        /// <summary>
        /// Constructor for <see cref="TileHeightMap"/> structure
        /// </summary>
        /// <param name="maxHeight">The max height that the corresponding tile type can have</param>
        /// <param name="type">The tile type that the height maps to</param>
        public TileHeightMap(float maxHeight, TileType type)
        {
            // Assigning structure properties
            MaxHeight = maxHeight;
            Type = type;
        }

    }
}
