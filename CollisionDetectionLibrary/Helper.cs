namespace CollisionDetectionLibrary
{
    public static class Helper
    {
        public const float Epsilon = 1.0f / 32.0f;

        public static bool Overlapping(float minA, float maxA, float minB, float maxB)
        {
            return minB <= maxA &&
                   minA <= maxB;
        }
    }
}