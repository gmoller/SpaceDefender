namespace CollisionDetectionLibrary.Shapes
{
    // Immutable
    public struct LineSegment
    {
        public IVector2D Point1 { get; private set; }
        public IVector2D Point2 { get; private set; }

        public LineSegment(IVector2D point1, IVector2D point2)
            : this()
        {
            Point1 = point1;
            Point2 = point2;
        }

        public bool CollidesWith(LineSegment lineSegment)
        {
            var axisA = new Line(Point1, Point2.Substract(Point1));

            if (axisA.OnOneSide(lineSegment))
            {
                return false;
            }

            var axisB = new Line(lineSegment.Point1, lineSegment.Point2.Substract(lineSegment.Point1));

            if (axisB.OnOneSide(this))
            {
                return false;
            }

            if (axisA.Direction.IsParallel(axisB.Direction))
            {
                Range rangeA = ProjectOnto(axisA.Direction);
                Range rangeB = lineSegment.ProjectOnto(axisA.Direction);

                return rangeA.Overlaps(rangeB);
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

        public bool CollidesWith(Line line)
        {
            return line.CollidesWith(this);
        }

        public bool CollidesWith(OrientedRectangle orientedRectangle)
        {
            Rectangle lr = orientedRectangle.TransformToLocalRectangle();

            IVector2D point1 = Point1.Substract(orientedRectangle.Center);
            point1 = point1.Rotate(-orientedRectangle.Rotation);
            point1 = point1.Add(orientedRectangle.HalfExtend);

            IVector2D point2 = Point2.Substract(orientedRectangle.Center);
            point2 = point2.Rotate(-orientedRectangle.Rotation);
            point2 = point2.Add(orientedRectangle.HalfExtend);

            var ls = new LineSegment(point1, point2);

            return lr.CollidesWith(ls);
        }

        public Range ProjectOnto(IVector2D onto)
        {
            IVector2D ontoUnit = onto.UnitVector();

            float min = ontoUnit.DotProduct(Point1);
            float max = ontoUnit.DotProduct(Point2);
            var r = new Range(min, max);
            r = r.SortRange();

            return r;
        }

        public bool SeparatingAxisForOrientedRectangle(OrientedRectangle orientedRectangle)
        {
            LineSegment rEdge0 = orientedRectangle.Edge(0);
            LineSegment rEdge2 = orientedRectangle.Edge(2);
            IVector2D n = Point1.Substract(Point2);

            Range axisRange = ProjectOnto(n);
            Range r0Range = rEdge0.ProjectOnto(n);
            Range r2Range = rEdge2.ProjectOnto(n);
            Range rProjection = r0Range.Hull(r2Range);

            return !axisRange.Overlaps(rProjection);
        }

        public bool SeparatingAxisForRectangle(Rectangle rectangle)
        {
            IVector2D n = Point1.Substract(Point2);

            var rEdgeA = new LineSegment(rectangle.Corner(0), rectangle.Corner(1));
            var rEdgeB = new LineSegment(rectangle.Corner(2), rectangle.Corner(3));

            Range rEdgeARange = rEdgeA.ProjectOnto(n);
            Range rEdgeBRange = rEdgeB.ProjectOnto(n);
            Range rProjection = rEdgeARange.Hull(rEdgeBRange);

            Range axisRange = ProjectOnto(n);

            return !axisRange.Overlaps(rProjection);
        }
    }
}