namespace CollisionDetectionLibrary.Shapes
{
    // Immutable
    public struct OrientedRectangle
    {
        public IVector2D Center { get; private set; }
        public IVector2D HalfExtend { get; private set; }
        public float Rotation { get; private set; }

        public OrientedRectangle(IVector2D center, IVector2D halfExtend, float rotation)
            : this()
        {
            Center = center;
            HalfExtend = halfExtend;
            Rotation = rotation;
        }

        public bool CollidesWith(OrientedRectangle orientedRectangle)
        {
            LineSegment edge = Edge(0);

            if (edge.SeparatingAxisForOrientedRectangle(orientedRectangle))
            {
                return false;
            }

            edge = Edge(1);
            if (edge.SeparatingAxisForOrientedRectangle(orientedRectangle))
            {
                return false;
            }

            edge = orientedRectangle.Edge(0);

            if (edge.SeparatingAxisForOrientedRectangle(this))
            {
                return false;
            }

            edge = orientedRectangle.Edge(1);

            return !edge.SeparatingAxisForOrientedRectangle(this);
        }

        public bool CollidesWith(Circle circle)
        {
            return circle.CollidesWith(this);
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.CollidesWith(this);
        }

        public bool CollidesWith(Point point)
        {
            return point.CollidesWith(this);
        }

        public bool CollidesWith(Line line)
        {
            return line.CollidesWith(this);
        }

        public bool CollidesWith(LineSegment lineSegment)
        {
            return lineSegment.CollidesWith(this);
        }

        public LineSegment Edge(int number)
        {
            IVector2D a;
            IVector2D b;

            switch (number % 4)
            {
                case 0:
                    a = VectorFactory.GetVector2D(-HalfExtend.X, HalfExtend.Y);
                    b = HalfExtend;
                    break;
                case 1:
                    a = HalfExtend;
                    b = VectorFactory.GetVector2D(HalfExtend.X, -HalfExtend.Y);
                    break;
                case 2:
                    a = VectorFactory.GetVector2D(HalfExtend.X, -HalfExtend.Y);
                    b = HalfExtend.Negate();
                    break;
                default:
                    a = HalfExtend.Negate();
                    b = VectorFactory.GetVector2D(-HalfExtend.X, HalfExtend.Y);
                    break;
            }

            a = a.Rotate(Rotation);
            a = a.Add(Center);

            b = b.Rotate(Rotation);
            b = b.Add(Center);

            var edge = new LineSegment(a, b);

            return edge;
        }

        public Rectangle RectangleHull()
        {
            var hull = new Rectangle(Center, VectorFactory.GetVector2D(0.0f, 0.0f));

            for (int number = 0; number < 4; number++)
            {
                IVector2D corner = Corner(number);
                hull = hull.EnlargeRectanglePoint(corner);
            }

            return hull;
        }

        public Circle CircleHull()
        {
            var hull = new Circle(Center, HalfExtend.Length());

            return hull;
        }

        public IVector2D Corner(int number)
        {
            IVector2D corner;
            switch (number % 4)
            {
                case 0:
                    corner = VectorFactory.GetVector2D(-HalfExtend.X, HalfExtend.Y);
                    break;
                case 1:
                    corner = HalfExtend;
                    break;
                case 2:
                    corner = VectorFactory.GetVector2D(HalfExtend.X, -HalfExtend.Y);
                    break;
                default:
                    corner = HalfExtend.Negate();
                    break;
            }

            corner = corner.Rotate(Rotation);

            return corner.Add(Center);
        }

        public Rectangle TransformToLocalRectangle()
        {
            IVector2D localOrigin = VectorFactory.GetVector2D(0.0f, 0.0f);
            IVector2D localSize = HalfExtend.Multiply(2.0f);
            var localRectangle = new Rectangle(localOrigin, localSize);

            return localRectangle;
        }
    }
}