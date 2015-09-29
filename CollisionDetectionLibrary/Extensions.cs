using System;

namespace CollisionDetectionLibrary
{
    public static class NumericExtensions
    {
        public static bool ApproximatelyEquals(this float f1, double f2, float epsilon = 1.0f / 8192.0f)
        {
            return Math.Abs(f1 - f2) < epsilon;
        }

        public static float Minimum(this float f1, float f2)
        {
            return f1 < f2 ? f1 : f2;
        }

        public static float Maximum(this float f1, float f2)
        {
            return f1 > f2 ? f1 : f2;
        }

        /// <summary>
        /// Convert to Radians.
        /// </summary>
        /// <param name="val">The value to convert to radians</param>
        /// <returns>The value in radians</returns>
        public static float ToRadians(this float val)
        {
            return (float)((Math.PI / 180.0f) * val);
        }

        public static float ToDegrees(this float val)
        {
            return (float)(val * 180.0f / Math.PI);
        }
    }
}