namespace Cellverse.Model
{
    public readonly struct Vector(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;

        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector operator *(Vector a, int n) => new(a.X * n, a.Y * n);

        public override bool Equals(object? obj) => obj is Vector v && X == v.X && Y == v.Y;
        public bool Equals(Vector other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public static bool operator ==(Vector a, Vector b) => a.Equals(b);
        public static bool operator !=(Vector a, Vector b) => !a.Equals(b);
        public override string ToString() => $"({X}, {Y})";
    }
}