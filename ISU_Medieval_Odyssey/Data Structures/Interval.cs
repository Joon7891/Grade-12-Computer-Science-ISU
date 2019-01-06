// Author: Joon Song
// File Name: Interval.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/24/2018
// Modified Date: 12/24/2018
// Description: Class to hold generic Interval struct - must be a numeric type

using System;

namespace ISU_Medieval_Odyssey
{
    public struct Interval<T> where T : IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
    {
        /// <summary>
        /// The lower bound of the <see cref="Interval{T}"/>
        /// </summary>
        public T LowerBound { get; set; }

        /// <summary>
        /// The lower bound type of the <see cref="Interval{T}"/>
        /// </summary>
        public IntervalType LowerBoundType { get; set; }

        /// <summary>
        /// The upper bound of the <see cref="Interval{T}"/>
        /// </summary>
        public T UpperBound { get; set; }

        /// <summary>
        /// The upper bound type of the <see cref="Interval{T}"/>
        /// </summary>
        public IntervalType UpperBoundType { get; set; }

        /// <summary>
        /// Constructor for <see cref="Interval{T}"/>
        /// </summary>
        /// <param name="lowerBound">The lower bound of the interval</param>
        /// <param name="lowerBoundType">The lower bound type of the interval - open/closed</param>
        /// <param name="upperBound">The upper bound of the interval</param>
        /// <param name="upperBoundType">The upper bound type of the interval - open/closed</param>
        public Interval(T lowerBound, IntervalType lowerBoundType, T upperBound, IntervalType upperBoundType)
        {
            // Assinging struct properties from constructor parameters
            LowerBound = lowerBound;
            LowerBoundType = lowerBoundType;
            UpperBound = upperBound;
            UpperBoundType = upperBoundType;
        }

        /// <summary>
        /// Subprogram to check if a value is within the <see cref="Interval{T}"/>
        /// </summary>
        /// <param name="value">The value to check if it is in the interval</param>
        /// <returns>Whether the value is inside the <see cref="Interval{T}"/></returns>
        public bool Contains(T value) => 
            LowerBoundType == IntervalType.Open ? LowerBound.CompareTo(value) < 0 : LowerBound.CompareTo(value) <= 0 &&
            UpperBoundType == IntervalType.Open ? UpperBound.CompareTo(value) > 0 : UpperBound.CompareTo(value) >= 0;

        public override string ToString() =>
            $"{(LowerBoundType == IntervalType.Open ? "(" : "[")}{LowerBound}, " +
            $"{UpperBound}{(UpperBoundType == IntervalType.Open ? ")" : "]")}";
    }
}
