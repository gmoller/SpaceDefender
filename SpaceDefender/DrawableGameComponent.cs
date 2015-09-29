using CollisionDetectionLibrary;
using CollisionDetectionLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SpaceDefender
{
    public abstract class DrawableGameComponent : IDrawableGameComponent
    {
        protected Texture2D Texture;
        protected Color Color = Color.White;
        protected Vector2 MovementVector;
        protected Point Size;
        protected Point BoundingSize;
        protected SpriteEffects SpriteEffect;

        /// <summary>
        /// Area to "lift" off of spritesheet for the sprite to be displayed.
        /// </summary>
        protected Rectangle SourceRectangle;

        /// <summary>
        /// Position of center of object with respect to the entire viewport. Top-left co-ordinate is 0,0.
        /// </summary>
        private Vector2 _centerPosition;

        protected Vector2 CenterPosition
        {
            get { return _centerPosition; }
            set
            {
                _centerPosition = value;
                TopLeftPosition = CenterPosition - Origin * Scale;
                BottomRightPosition = CenterPosition + Origin * Scale;
            }
        }

        /// <summary>
        /// Position of top left co-ordinates of object with respect to the entire viewport.
        /// </summary>
        protected Vector2 TopLeftPosition;

        /// <summary>
        /// Position of bottom right co-ordinates of object with respect to the entire viewport.
        /// </summary>
        protected Vector2 BottomRightPosition;

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

        protected bool IsAlive;

        private Circle BoundingCircle
        {
            get
            {
                float radius = (MathHelper.Max(BoundingSize.X, BoundingSize.Y) / 2.0f) * Scale.X;

                return new Circle(VectorFactory.GetVector2D(CenterPosition.X, CenterPosition.Y), radius);
            }
        }

        protected DrawableGameComponent(Vector2 centerPosition)
        {
            CenterPosition = centerPosition;
            Scale = new Vector2(0.2f, 0.2f) * new Vector2(GameRoot.ScreenSize.X / 1280, GameRoot.ScreenSize.Y / 720);
            Rotation = 0.0f;
            MovementVector = Vector2.Zero;
            SpriteEffect = SpriteEffects.None;
            IsAlive = true;
        }

        bool IDrawableGameComponent.IsAlive
        {
            get { return IsAlive; }
            set { IsAlive = value; }
        }

        Vector2 IDrawableGameComponent.CenterPosition
        {
            get { return CenterPosition; }
            set
            {
                CenterPosition = value;
            }
        }

        Circle IDrawableGameComponent.BoundingCircle
        {
            get { return BoundingCircle; }
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime, InputState inputState);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                spriteBatch.Draw(texture: Texture,
                                 position: CenterPosition,
                                 sourceRectangle: SourceRectangle,
                                 origin: Origin,
                                 scale: Scale,
                                 rotation: Rotation,
                                 color: Color,
                                 effects: SpriteEffect,
                                 layerDepth: 0.0f);

                spriteBatch.DrawCircle(CenterPosition, 1, Color.RoyalBlue);

                if (GameRoot.ShowBounds)
                {
                    var color = new Color(0, 128, 0, 128);
                    spriteBatch.DrawCircle(new Vector2(BoundingCircle.Center.X, BoundingCircle.Center.Y), BoundingCircle.Radius, color, 1000);
                }
            }
        }
    }
}