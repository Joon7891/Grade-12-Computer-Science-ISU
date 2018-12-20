using Microsoft.Xna.Framework;

namespace ISU_Medieval_Odyssey.Utility
{
    public struct Vector2Int
    {
        public int X { get; }
        public int Y { get; }

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Vector2Int left, Vector2Int right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Vector2Int left, Vector2Int right) => !(left == right);

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
