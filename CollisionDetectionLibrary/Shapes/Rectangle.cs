namespace CollisionDetectionLibrary.Shapes
{
    // Immutable
    public struct Rectangle
    {
        public IVector2D Origin { get; private set; }
        public IVector2D Size { get; private set; }

        public Rectangle(IVector2D origin, IVector2D size)
            : this()
        {
            Origin = origin;
            Size = size;
        }

        public bool CollidesWith(Rectangle rectangle)
        {
            float aLeft = Origin.X;
            float aRight = aLeft + Size.X;

            float bLeft = rectangle.Origin.X;
            float bRight = bLeft + rectangle.Size.X;

            float aBottom = Origin.Y;
            float aTop = aBottom + Size.Y;

            float bBottom = rectangle.Origin.Y;
            float bTop = bBottom + rectangle.Size.Y;

            return Helper.Overlapping(aLeft, aRight, bLeft, bRight) &&
                   Helper.Overlapping(aBottom, aTop, bBottom, bTop);
        }

        public bool CollidesWith(IVector2D movement, Rectangle rectangle)
        {
            IVector2D envelopeOrigin = Origin.Add(movement);
            var envelope = new Rectangle(envelopeOrigin, Size);
            envelope = envelope.EnlargeRectangleRectangle(this);

            if (envelope.CollidesWith(rectangle))
            {
                float min = Size.X.Minimum(Size.Y) / 4.0f;
                float minimumMoveDistance = min.Maximum(Helper.Epsilon);

                if (movement.Length() < minimumMoveDistance)
                {
                    return true;
                }

                IVector2D halfMovement = movement.Divide(2.0f);

                envelope = new Rectangle(Origin.Add(halfMovement), Size);

                return CollidesWith(halfMovement, rectangle) ||
                       envelope.CollidesWith(halfMovement, rectangle);
            }

            return false;
        }

        public bool CollidesWith(Circle circle)
        {
            return circle.CollidesWith(this);
        }

        public bool CollidesWith(Point point)
        {
            float left = Origin.X;
            float right = left + Size.X;
            float bottom = Origin.Y;
            float top = bottom + Size.Y;

            return point.Position.X >= left &&
                   point.Position.Y >= bottom &&
                   point.Position.X <= right &&
                   point.Position.Y <= top;
        }

        public bool CollidesWith(Line line)
        {
            IVector2D n = line.Direction.Rotate90();

            IVector2D c1 = Origin;
            IVector2D c2 = c1.Add(Size);
            IVector2D c3 = VectorFactory.GetVector2D(c2.X, c1.Y);
            IVector2D c4 = VectorFactory.GetVector2D(c1.X, c2.Y);

            c1 = c1.Substract(line.Base);
            c2 = c2.Substract(line.Base);
            c3 = c3.Substract(line.Base);
            c4 = c4.Substract(line.Base);

            float dp1 = n.DotProduct(c1);
            float dp2 = n.DotProduct(c2);
            float dp3 = n.DotProduct(c3);
            float dp4 = n.DotProduct(c4);

            return (dp1 * dp2 <= 0) ||
                   (dp2 * dp3 <= 0) ||
                   (dp3 * dp4 <= 0);
        }

        public bool CollidesWith(LineSegment lineSegment)
        {
            var sLine = new Line(lineSegment.Point1, lineSegment.Point2.Substract(lineSegment.Point1));
            if (!sLine.CollidesWith(this))
            {
                return false;
            }

            var rRange = new Range(Origin.X, Origin.X + Size.X);
            var sRange = new Range(lineSegment.Point1.X, lineSegment.Point2.X);
            sRange = sRange.SortRange();
            if (!rRange.Overlaps(sRange))
            {
                return false;
            }

            rRange = new Range(Origin.Y, Origin.Y + Size.Y);
            sRange = new Range(lineSegment.Point1.Y, lineSegment.Point2.Y);
            sRange = sRange.SortRange();

            return rRange.Overlaps(sRange);
        }

        public bool CollidesWith(OrientedRectangle orientedRectangle)
        {
            Rectangle orHull = orientedRectangle.RectangleHull();
            if (!orHull.CollidesWith(this))
            {
                return false;
            }

            LineSegment edge = orientedRectangle.Edge(0);
            if (edge.SeparatingAxisForRectangle(this))
            {
                return false;
            }

            edge = orientedRectangle.Edge(1);

            return !edge.SeparatingAxisForRectangle(this);
        }

        public IVector2D Clamp(IVector2D p)
        {
            var rangeX = new Range(Origin.X, Origin.X + Size.X);
            var rangeY = new Range(Origin.Y, Origin.Y + Size.Y);
            float x = rangeX.Clamp(p.X);
            float y = rangeY.Clamp(p.Y);

            IVector2D clamp = VectorFactory.GetVector2D(x, y);

            return clamp;
        }

        public Rectangle EnlargeRectanglePoint(IVector2D point)
        {
            IVector2D origin = VectorFactory.GetVector2D(Origin.X.Minimum(point.X), Origin.Y.Minimum(point.Y));
            IVector2D size = VectorFactory.GetVector2D((Origin.X + Size.X).Maximum(point.X), (Origin.Y + Size.Y).Maximum(point.Y));
            size = size.Substract(origin);
            var enlarged = new Rectangle(origin, size);

            return enlarged;
        }

        public Rectangle EnlargeRectangleRectangle(Rectangle extender)
        {
            IVector2D maxCorner = extender.Origin.Add(extender.Size);
            Rectangle enlarged = EnlargeRectanglePoint(maxCorner);

            return enlarged.EnlargeRectanglePoint(extender.Origin);
        }

        public IVector2D Corner(int number)
        {
            IVector2D corner;
            switch (number % 4)
            {
                case 0:
                    corner = VectorFactory.GetVector2D(Origin.X + Size.X, Origin.Y);
                    break;
                case 1:
                    corner = Origin.Add(Size);
                    break;
                case 2:
                    corner = VectorFactory.GetVector2D(Origin.X, Origin.Y + Size.Y);
                    break;
                default:
                    corner = Origin;
                    break;
            }

            return corner;
        }
    }
}