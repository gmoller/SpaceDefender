using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    internal abstract class DrawableGameComponent : GameComponent, IDrawableGameComponent
    {
        protected Vector2 MovementVector;
        protected Point BoundingSize;
        protected SpriteEffects SpriteEffect;

        /// <summary>
        /// Area to "lift" off of spritesheet for the sprite to be displayed.
        /// </summary>
        protected Rectangle SourceRectangle;

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

        protected bool IsAlive;

        internal Rectangle BoundingRectangle
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
                        X = (int)(CenterPosition.X - (Origin.X * Scale.X)),
                        Y = (int)(CenterPosition.Y - (Origin.Y * Scale.Y))
                    };
                }

                return rectangle;
            }
        }

        internal Circle BoundingCircle
        {
            get
            {
                float radius = (MathHelper.Max(BoundingSize.X, BoundingSize.Y) / 2.0f) * Scale.X;

                return new Circle(CenterPosition, radius);
            }
        }

        internal DrawableGameComponent(Vector2 position, int viewportWidth, int viewportHeight)
            : base(viewportWidth, viewportHeight)
        {
            CenterPosition = position;
            Scale = new Vector2(0.3f, 0.3f);
            Rotation = 0.0f;
            MovementVector = Vector2.Zero;
            SpriteEffect = SpriteEffects.None;
            IsAlive = true;
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime, InputState inputState);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                spriteBatch.Draw(texture: Texture,
                                 position: CenterPosition,
                                 origin: Origin,
                                 scale: Scale,
                                 rotation: Rotation,
                                 color: Color,
                                 effects: SpriteEffect,
                                 sourceRectangle: SourceRectangle);

                spriteBatch.DrawCircle(CenterPosition, 1, Color.RoyalBlue);

                if (Game1.ShowBounds)
                {
                    var color = new Color(0, 128, 0, 128);
                    spriteBatch.DrawCircle(BoundingCircle.Center, BoundingCircle.Radius, color, 1000);
                }
            }
        }

        bool IDrawableGameComponent.IsAlive
        {
            get { return IsAlive; }
            set { IsAlive = value; }
        }
    }
}