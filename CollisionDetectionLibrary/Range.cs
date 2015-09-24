namespace CollisionDetectionLibrary
{
    // Immutable
    public struct Range
    {
        public readonly float Minimum;
        public readonly float Maximum;

        public Range(float minimum, float maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public Range SortRange()
        {
            Range sorted = Minimum > Maximum ? new Range(Maximum, Minimum) : new Range(Minimum, Maximum);

            return sorted;
        }

        public bool Overlaps(Range r)
        {
            return Helper.Overlapping(Minimum, Maximum, r.Minimum, r.Maximum);
        }

        public Range Hull(Range r)
        {
            float minimum = Minimum < r.Minimum ? Minimum : r.Minimum;
            float maximum = Maximum > r.Maximum ? Maximum : r.Maximum;

            var hull = new Range(minimum, maximum);

            return hull;
        }

        public float Clamp(float x)
        {
            if (x < Minimum)
            {
                return Minimum;
            }

            if (x > Maximum)
            {
                return Maximum;
            }

            return x;
        }
    }
}