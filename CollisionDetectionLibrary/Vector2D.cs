using System;

namespace CollisionDetectionLibrary
{
    // Immutable
    public struct Vector2D : IVector2D
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public Vector2D(float x, float y)
            : this()
        {
            X = x;
            Y = y;
        }

        public IVector2D Add(IVector2D v)
        {
            IVector2D r = new Vector2D(X + v.X, Y + v.Y);

            return r;
        }

        public IVector2D Substract(IVector2D v)
        {
            IVector2D r = new Vector2D(X - v.X, Y - v.Y);

            return r;
        }

        public IVector2D Negate()
        {
            IVector2D r = new Vector2D(-X, -Y);

            return r;
        }

        public IVector2D Multiply(float scalar)
        {
            IVector2D r = new Vector2D(X * scalar, Y * scalar);

            return r;
        }

        public IVector2D Divide(float divisor)
        {
            IVector2D r = new Vector2D(X / divisor, Y / divisor);

            return r;
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public IVector2D UnitVector()
        {
            float length = Length();
            if (length > 0)
            {
                return Divide(length);
            }

            return this;
        }

        public IVector2D Rotate(float degrees)
        {
            float radians = degrees.ToRadians();

            var sine = (float)Math.Sin(radians);
            var cosine = (float)Math.Cos(radians);

            IVector2D r = new Vector2D(X * cosine - Y * sine, X * sine - Y * cosine);

            return r;
        }

        public IVector2D Rotate90()
        {
            IVector2D r = new Vector2D(-Y, X);

            return r;
        }

        public float DotProduct(IVector2D v)
        {
            return X * v.X + Y * v.Y;
        }

        public float AngleBetween(IVector2D v)
        {
            IVector2D u1 = UnitVector();
            IVector2D u2 = v.UnitVector();

            float dp = u1.DotProduct(u2);

            var f = (float)Math.Acos(dp);

            return f.ToDegrees();
        }

        public IVector2D ProjectOnto(IVector2D v)
        {
            float d = v.DotProduct(v);

            if (d > 0)
            {
                float dp = DotProduct(v);

                return v.Multiply(dp / d);
            }

            return v;
        }

        public bool IsParallel(IVector2D v)
        {
            IVector2D na = Rotate90();

            float dp = na.DotProduct(v);

            return dp.ApproximatelyEquals(0.0f);
        }

        public bool Equals(IVector2D v)
        {
            return X.ApproximatelyEquals(v.X) && Y.ApproximatelyEquals(v.Y);
        }

        public override string ToString()
        {
            return string.Format("{0};{1}", X, Y);
        }
    }
}