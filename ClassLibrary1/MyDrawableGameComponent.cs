using CollisionDetectionLibrary;
using CollisionDetectionLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace GameLibrary
{
    public abstract class MyDrawableGameComponent : IDrawableGameComponent
    {
        protected Sprite Sprite { get; set; }

        protected Vector2 MovementVector;

        public Vector2 CenterPosition { get; set; }

        /// <summary>
        /// Position of top left co-ordinates of object with respect to the entire viewport.
        /// </summary>
        protected Vector2 TopLeftPosition
        {
            get { return CenterPosition - Sprite.Origin * Sprite.Scale; }
        }

        /// <summary>
        /// Position of bottom right co-ordinates of object with respect to the entire viewport.
        /// </summary>
        protected Vector2 BottomRightPosition
        {
            get { return CenterPosition + Sprite.Origin * Sprite.Scale; }
        }

        protected Point BoundingSize;

        public virtual Circle BoundingCircle
        {
            get
            {
                float radius = (MathHelper.Max(BoundingSize.X, BoundingSize.Y) / 2.0f) * Sprite.Scale.X;

                return new Circle(VectorFactory.GetVector2D(CenterPosition.X, CenterPosition.Y), radius);
            }
        }

        public virtual CollisionDetectionLibrary.Shapes.Rectangle BoundingRectangle
        {
            get
            {
                var x = (int)(CenterPosition.X - (BoundingSize.X * Sprite.Scale.X) / 2.0f);
                var y = (int)(CenterPosition.Y - (BoundingSize.Y * Sprite.Scale.Y) / 2.0f);
                var width = (int)(BoundingSize.X * Sprite.Scale.X);
                var height = (int)(BoundingSize.Y * Sprite.Scale.Y);

                return new CollisionDetectionLibrary.Shapes.Rectangle(new Vector2D(x, y), new Vector2D(width, height));
            }
        }

        public bool IsAlive { get; set; }

        protected MyDrawableGameComponent(Vector2 centerPosition)
        {
            CenterPosition = centerPosition;
            MovementVector = Vector2.Zero;
            IsAlive = true;
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime, InputState inputState);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                Sprite.Draw(spriteBatch, CenterPosition);

                if (Globals.ShowBounds)
                {
                    // BoundingCircle - green
                    spriteBatch.DrawCircle(new Vector2(BoundingCircle.Center.X, BoundingCircle.Center.Y), BoundingCircle.Radius, Color.Green, 1000);

                    // Bounding Rectangle - yellow
                    spriteBatch.DrawRectangle(new Rectangle((int)BoundingRectangle.Origin.X, (int)BoundingRectangle.Origin.Y, (int)BoundingRectangle.Size.X, (int)BoundingRectangle.Size.Y), Color.Yellow, false);

                    // Rectangle around entire sprite - red
                    TextureAtlas atlas = Sprite.TextureAtlas;
                    spriteBatch.DrawRectangle(new Rectangle((int)TopLeftPosition.X, (int)TopLeftPosition.Y, (int)((atlas.SingleTextureWidth) * Sprite.Scale.X), (int)(atlas.SingleTextureHeight * Sprite.Scale.Y)), Color.Red, false);
                }
            }
        }
    }
}