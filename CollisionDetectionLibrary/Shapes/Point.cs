namespace CollisionDetectionLibrary.Shapes
{
    // Immutable
    public struct Point
    {
        public IVector2D Position { get; private set; }

        public Point(IVector2D position)
            : this()
        {
            Position = position;
        }

        public bool CollidesWith(Point point)
        {
            return Position.Equals(point.Position);
        }

        public bool CollidesWith(Circle circle)
        {
            return circle.CollidesWith(this);
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            return rectangle.CollidesWith(this);
        }

        public bool CollidesWith(Line line)
        {
            var lineBase = new Point(line.Base);
            if (lineBase.CollidesWith(this))
            {
                return true;
            }

            IVector2D lp = Position.Substract(line.Base);

            return lp.IsParallel(line.Direction);
        }

        public bool CollidesWith(LineSegment lineSegment)
        {
            IVector2D d = lineSegment.Point2.Substract(lineSegment.Point1);
            IVector2D lp = Position.Substract(lineSegment.Point1);
            IVector2D pr = lp.ProjectOnto(d);

            return lp.Equals(pr) &&
                   pr.Length() <= d.Length() &&
                   pr.DotProduct(d) >= 0;
        }

        public bool CollidesWith(OrientedRectangle orientedRectangle)
        {
            Rectangle localRectangle = orientedRectangle.TransformToLocalRectangle();

            IVector2D lp = Position.Substract(orientedRectangle.Center);
            lp = lp.Rotate(-orientedRectangle.Rotation);
            lp = lp.Add(orientedRectangle.HalfExtend);
            var localPoint = new Point(lp);

            return localPoint.CollidesWith(localRectangle);
        }
    }
}