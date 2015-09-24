using System;

namespace SpaceDefender
{
    internal static class Extensions
    {
        internal static bool ApproximatelyEquals(this float f1, double f2, float epsilon = 1.0f / 8192.0f)
        {
            return Math.Abs(f1 - f2) < epsilon;
        }
    }
}