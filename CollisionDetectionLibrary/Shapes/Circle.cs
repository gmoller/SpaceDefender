namespace CollisionDetectionLibrary.Shapes
{
    // Immutable
    public struct Circle
    {
        public IVector2D Center { get; private set; }
        public float Radius { get; private set; }

        public Circle(IVector2D center, float radius)
            : this()
        {
            Center = center;
            Radius = radius;
        }

        public bool CollidesWith(Circle circle)
        {
            float radiusSum = Radius + circle.Radius;
            IVector2D distance = Center.Substract(circle.Center);

            return distance.Length() <= radiusSum;
        }

        public bool CollidesWith(Point point)
        {
            IVector2D distance = Center.Substract(point.Position);

            return distance.Length() <= Radius;
        }

        public bool CollidesWith(Line line)
        {
            IVector2D lc = Center.Substract(line.Base);
            IVector2D p = lc.ProjectOnto(line.Direction);
            IVector2D nearest = line.Base.Add(p);

            return CollidesWith(new Point(nearest));
        }

        public bool CollidesWith(LineSegment lineSegment)
        {
            if (CollidesWith(new Point(lineSegment.Point1)))
            {
                return true;
            }
            if (CollidesWith(new Point(lineSegment.Point2)))
            {
                return true;
            }

            IVector2D d = lineSegment.Point2.Substract(lineSegment.Point1);
            IVector2D lc = Center.Substract(lineSegment.Point1);
            IVector2D p = lc.ProjectOnto(d);
            IVector2D nearest = lineSegment.Point1.Add(p);

            return CollidesWith(new Point(nearest)) &&
                   p.Length() <= d.Length() &&
                   p.DotProduct(d) >= 0;
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            IVector2D clamped = rectangle.Clamp(Center);

            return CollidesWith(new Point(clamped));
        }

        public bool CollidesWith(IVector2D movement, Rectangle rectangle)
        {
            IVector2D halfMovement = movement.Divide(2.0f);
            float movementDistance = movement.Length();
            IVector2D envelopeCenter = Center.Add(halfMovement);
            float envelopeRadius = Radius + movementDistance / 2.0f;
            var envelope = new Circle(envelopeCenter, envelopeRadius);

            if (envelope.CollidesWith(rectangle))
            {
                float minimumMoveDistance = (Radius / 4.0f).Maximum(Helper.Epsilon);

                if (movementDistance < minimumMoveDistance)
                {
                    return true;
                }

                envelope = new Circle(envelope.Center, Radius);

                return CollidesWith(halfMovement, rectangle) ||
                       envelope.CollidesWith(halfMovement, rectangle);
            }

            return false;
        }

        public bool CollidesWith(OrientedRectangle orientedRectangle)
        {
            Rectangle localRectangle = orientedRectangle.TransformToLocalRectangle();

            IVector2D distance = Center.Substract(orientedRectangle.Center);
            distance = distance.Rotate(-orientedRectangle.Rotation);
            var localCenter = distance.Add(orientedRectangle.HalfExtend);
            var localCircle = new Circle(localCenter, Radius);

            return localCircle.CollidesWith(localRectangle);
        }
    }
}