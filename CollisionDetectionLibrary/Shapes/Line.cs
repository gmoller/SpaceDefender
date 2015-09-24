namespace CollisionDetectionLibrary.Shapes
{
    // Immutable
    public struct Line
    {
        public IVector2D Base { get; private set; }
        public IVector2D Direction { get; private set; }

        public Line(IVector2D basee, IVector2D direction)
            : this()
        {
            Base = basee;
            Direction = direction;
        }

        public bool CollidesWith(Line line)
        {
            if (Direction.IsParallel(line.Direction)) // vectors are parallel
            {
                return IsEquivalent(line);
            }

            return true;
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

        public bool CollidesWith(LineSegment lineSegment)
        {
            return !OnOneSide(lineSegment);
        }

        public bool CollidesWith(OrientedRectangle orientedRectangle)
        {
            Rectangle lr = orientedRectangle.TransformToLocalRectangle();

            IVector2D basee = Base.Substract(orientedRectangle.Center);
            basee = basee.Rotate(-orientedRectangle.Rotation);
            basee = basee.Add(orientedRectangle.HalfExtend);
            IVector2D direction = Direction.Rotate(-orientedRectangle.Rotation);
            var line = new Line(basee, direction);

            return line.CollidesWith(lr);
        }

        public bool OnOneSide(LineSegment lineSegment)
        {
            IVector2D d1 = lineSegment.Point1.Substract(Base);
            IVector2D d2 = lineSegment.Point2.Substract(Base);

            IVector2D n = Direction.Rotate90();

            return (n.DotProduct(d1) * n.DotProduct(d2)) > 0;
        }

        public bool IsEquivalent(Line line)
        {
            if (!Direction.IsParallel(line.Direction))
            {
                return false;
            }

            IVector2D d = Base.Substract(line.Base);

            return d.IsParallel(Direction);
        }
    }
}