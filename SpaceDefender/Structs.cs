using Microsoft.Xna.Framework;

namespace SpaceDefender
{
    internal struct Circle
    {
        private readonly Vector2 _center;
        internal Vector2 Center { get { return _center; } }

        private readonly float _radius;
        internal float Radius { get { return _radius; } }

        internal Circle(Vector2 center, float radius)
        {
            _center = center;
            _radius = radius;
        }
    }

    internal enum BoundsCheck
    {
        InBounds = 0,
        OutsideLeftOrRight = 1,
        OutsideTopOrBottom = 2
    }
}