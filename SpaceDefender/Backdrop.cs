using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Backdrop : GameComponent, IDrawableGameComponent
    {
        internal Backdrop(Texture2D texture, int viewportWidth, int viewportHeight)
            : base(texture, new Vector2(viewportWidth / 2.0f, viewportHeight / 2.0f), viewportWidth, viewportHeight)
        {
            Scale = Vector2.One;
            Origin = Vector2.Zero;
            BoundingSize = new Point {X = 0, Y = 0};
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: Texture,
                             destinationRectangle: new Rectangle(0, 0, ViewportWidth, ViewportHeight),
                             origin: Origin,
                             scale: Scale,
                             rotation: Rotation,
                             color: Color,
                             effects: SpriteEffect);
        }
    }
}