namespace CollisionDetectionLibrary
{
    public interface IVector2D
    {
        float X { get; }
        float Y { get; }
        IVector2D Add(IVector2D v);
        IVector2D Substract(IVector2D v);
        IVector2D Negate();
        IVector2D Multiply(float scalar);
        IVector2D Divide(float divisor);
        float Length();
        IVector2D UnitVector();
        IVector2D Rotate(float degrees);
        IVector2D Rotate90();
        float DotProduct(IVector2D v);
        float AngleBetween(IVector2D v);
        IVector2D ProjectOnto(IVector2D v);
        bool IsParallel(IVector2D v);
        bool Equals(IVector2D v);
        string ToString();
    }
}