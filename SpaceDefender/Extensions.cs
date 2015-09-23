using System;

namespace SpaceDefender
{
    internal static class Extensions
    {
        internal static bool IsApproximately(this float f1, double f2, float epsilon = 0.0001f)
        {
            return Math.Abs(f1 - f2) < epsilon;
        }
    }
}