// Author: Joon Song
// File Name: TileNoiseMap.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/24/2018
// Modified Date: 12/24/2018
// Description: TileNoiseMap structure - maps a noise to a tile type

namespace ISU_Medieval_Odyssey
{
    public struct TileNoiseMap
    {
        /// <summary>
        /// The interval that the noise value must fall within - [a, b)
        /// </summary>
        public Interval<float> NoiseInterval { get; }

        /// <summary>
        /// The tile type that the height maps to
        /// </summary>
        public TileType Type { get; }

        /// <summary>
        /// Constructor for <see cref="TileNoiseMap"/> structure
        /// </summary>
        /// <param name="lowerBound">The closed lower bound of the noise interval</param>
        /// <param name="upperBound">The open uppper bound of the noise interval</param>
        /// <param name="type">The corresponding tile type</param>
        public TileNoiseMap(float lowerBound, float upperBound, TileType type)
        {
            // Assigning bounds to interval, and tile type
            NoiseInterval = new Interval<float>(lowerBound, IntervalType.Closed, upperBound, IntervalType.Open);
            Type = type;
        }
    }
}
