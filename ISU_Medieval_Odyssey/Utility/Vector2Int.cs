// Author: Joon Song
// File Name: Vector2Int.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 12/20/2018
// Modified Date: 12/20/2018
// Description: Class to hold Vector2Int struct

using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey.Utility
{
    /// <summary>
    /// Descrtibes a 2D vector with integer coordinates
    /// </summary>
    public struct Vector2Int
    {
        /// <summary>
        /// The x-coordainte of the <see cref="Vector2Int"/>
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The y-coordainte of the <see cref="Vector2Int"/>
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Constuctor for <see cref="Vector2Int"/> object
        /// </summary>
        /// <param name="x">The x-coordinate</param>
        /// <param name="y">The y-coordinate</param>
        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side <see cref="Vector2Int"/></param>
        /// <returns>If the left side <see cref="Vector2Int"/> is equal to the right side <see cref="Vector2Int"/></returns>
        public static bool operator ==(Vector2Int left, Vector2Int right) => left.X == right.X && left.Y == right.Y;

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side <see cref="Vector2Int"/></param>
        /// <returns>If the left side <see cref="Vector2Int"/> is not equal to the right side <see cref="Vector2Int"/></returns>
        public static bool operator !=(Vector2Int left, Vector2Int right) => !(left == right);

        /// <summary>
        /// Addition with a scalar operator
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side scalar</param>
        /// <returns>The result of adding a scalar to a <see cref="Vector2Int"/></returns>
        public static Vector2Int operator +(Vector2Int left, int right) => new Vector2Int(left.X + right, left.Y + right);
        public static Vector2Int operator -(Vector2Int left, int right) => new Vector2Int(left.X - right, left.Y - right);
        public static Vector2Int operator *(Vector2Int left, int right) => new Vector2Int(left.X * right, left.Y * right);
        public static Vector2Int operator /(Vector2Int left, int right) => new Vector2Int(left.X / right, left.Y / right);

        public static Vector2Int operator +(Vector2Int left, Vector2Int right) => new Vector2Int(left.X + right.X, left.Y + right.Y);
        public static Vector2Int operator -(Vector2Int left, Vector2Int right) => new Vector2Int(left.X - right.X, left.Y - right.Y);
        public static Vector2Int operator *(Vector2Int left, Vector2Int right) => new Vector2Int(left.X * right.X, left.Y * right.Y);
        public static Vector2Int operator /(Vector2Int left, Vector2Int right) => new Vector2Int(left.X / right.X, left.Y / right.Y);

        public static Vector2Int operator -(Vector2Int vector) => new Vector2Int(-vector.X, -vector.Y);
        public bool Equals(Vector2Int other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2Int i && Equals(i);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString() => $"({X}, {Y})";
        public Vector2 ToVector2() => new Vector2(X, Y);
    }
}
