namespace CollisionDetectionLibrary
{
    public static class VectorFactory
    {
        public static IVector2D GetVector2D(float x, float y)
        {
            return new Vector2D(x, y);
        }
    }
}