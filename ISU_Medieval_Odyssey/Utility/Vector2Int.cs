﻿// Author: Joon Song
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
        /// Addition with a scalar
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side scalar</param>
        /// <returns>The result of adding a scalar to a <see cref="Vector2Int"/></returns>
        public static Vector2Int operator +(Vector2Int left, int right) => new Vector2Int(left.X + right, left.Y + right);

        /// <summary>
        /// Subtraction with a scalar
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side scalar</param>
        /// <returns>The result of subtracting a scalar to a <see cref="Vector2Int"/></returns>
        public static Vector2Int operator -(Vector2Int left, int right) => new Vector2Int(left.X - right, left.Y - right);

        /// <summary>
        /// Multiplication with a scalar
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side scalar</param>
        /// <returns>The result of multiplying a scalar to a <see cref="Vector2Int"/></returns>
        public static Vector2Int operator *(Vector2Int left, int right) => new Vector2Int(left.X * right, left.Y * right);

        /// <summary>
        /// Division with a scalar
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side scalar</param>
        /// <returns>The result of dividing a scalar to a <see cref="Vector2"/></returns>
        public static Vector2Int operator /(Vector2Int left, int right) => new Vector2Int(left.X / right, left.Y / right);

        /// <summary>
        /// <see cref="Vector2Int"/> addition
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side <see cref="Vector2Int"/>></param>
        /// <returns>The result of adding two <see cref="Vector2Int"/></returns>
        public static Vector2Int operator +(Vector2Int left, Vector2Int right) => new Vector2Int(left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// <see cref="Vector2Int"/> subtraction
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side <see cref="Vector2Int"/>></param>
        /// <returns>The result of subtracting a <see cref="Vector2Int"/> from a <see cref="Vector2Int"/></returns>
        public static Vector2Int operator -(Vector2Int left, Vector2Int right) => new Vector2Int(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// <see cref="Vector2Int"/> multiplication
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side <see cref="Vector2Int"/>></param>
        /// <returns>The result of multiplying two <see cref="Vector2Int"/></returns>
        public static Vector2Int operator *(Vector2Int left, Vector2Int right) => new Vector2Int(left.X * right.X, left.Y * right.Y);


        /// <summary>
        /// <see cref="Vector2Int"/> division
        /// </summary>
        /// <param name="left">The left side <see cref="Vector2Int"/></param>
        /// <param name="right">The right side <see cref="Vector2Int"/>></param>
        /// <returns>The result of divising a <see cref="Vector2Int"/> from a <see cref="Vector2Int"/></returns>
        public static Vector2Int operator /(Vector2Int left, Vector2Int right) => new Vector2Int(left.X / right.X, left.Y / right.Y);

        /// <summary>
        /// Opposite/negative <see cref="Vector2Int"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector2Int"/> from which to calculate its opposite/negative <see cref="Vector2Int"/></param>
        /// <returns>The opposite/negative <see cref="Vector2Int"/></returns>
        public static Vector2Int operator -(Vector2Int vector) => new Vector2Int(-vector.X, -vector.Y);

        /// <summary>
        /// Eequals operator
        /// </summary>
        /// <param name="other">The vector to be compared to</param>
        /// <returns>If two vectors are equal</returns>
        public bool Equals(Vector2Int other) => X == other.X && Y == other.Y;

        /// <summary>
        /// Converts a <see cref="Vector2Int"/> to a <see cref="string"/>
        /// </summary>
        /// <returns>The string representation of a <see cref="Vector2Int"/></returns>
        public override string ToString() => $"({X}, {Y})";

        /// <summary>
        /// Converts a <see cref="Vector2Int"/> to a <see cref="Vector2"/>
        /// </summary>
        /// <returns>The <see cref="Vector2"/> representation of a <see cref="Vector2Int"/></returns>
        public Vector2 ToVector2() => new Vector2(X, Y);
    }
}
