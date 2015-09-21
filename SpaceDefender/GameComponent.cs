using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    internal class GameComponent
    {
        protected Texture2D Texture;
        protected Color Color;
        protected SpriteEffects SpriteEffect;

        /// <summary>
        /// Position of center of object with respect to the entire viewport. Top-left co-ordinate is 0,0.
        /// </summary>
        protected Vector2 CenterPosition;

        /// <summary>
        /// Origin offsets the CenterPosition when drawing to the screen. So top-left is CenterPosition - Origin.
        /// </summary>
        protected Vector2 Origin;

        /// <summary>
        /// Amount to scale the texture by when drawing to the screen.
        /// </summary>
        protected Vector2 Scale;

        /// <summary>
        /// Amount to rotate the texture by (in radians) when drawing to the screen. 0 * PI = no rotation, 0.5 * PI = 90 degrees, 1 * PI = 180 degrees, 1.5 * PI = 270.
        /// </summary>
        protected float Rotation;

        protected int ViewportWidth;
        protected int ViewportHeight;

        protected Point BoundingSize;

        public Rectangle BoundingBox
        {
            get
            {
                var rectangle = new Rectangle();
                if (Texture != null)
                {
                    rectangle = new Rectangle
                        {
                            Width = (int)(BoundingSize.X * Scale.X),
                            Height = (int)(BoundingSize.Y * Scale.Y),
                            X = (int) (CenterPosition.X - (Origin.X * Scale.X)),
                            Y = (int) (CenterPosition.Y - (Origin.Y * Scale.Y))
                        };
                }

                return rectangle;
            }
        }

        public Circle BoundingCircle
        {
            get { return new Circle(CenterPosition, (BoundingSize.X / 2.0f) * Scale.X); }
        }

        protected Vector2 MovementVector;

        internal GameComponent(Texture2D texture, Vector2 position, int viewportWidth, int viewportHeight)
        {
            Texture = texture;
            SpriteEffect = SpriteEffects.None;
            CenterPosition = position;
            Scale = new Vector2(0.3f, 0.3f);
            Rotation = 0.0f;
            Color = Color.White;
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;

            Origin = Texture != null ? new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f) : Vector2.Zero;
            BoundingSize = Texture != null ? new Point { X = Texture.Width, Y = Texture.Height } : Point.Zero;

            MovementVector = Vector2.Zero;
        }
    }
}