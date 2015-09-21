using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Backdrop : GameComponent, IDrawableGameComponent
    {
        internal Backdrop(Texture2D texture, int viewportWidth, int viewportHeight)
            : base(texture, viewportWidth, viewportHeight)
        {
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: Texture,
                             destinationRectangle: new Rectangle(0, 0, ViewportWidth, ViewportHeight),
                             origin: Vector2.Zero,
                             scale: Vector2.One,
                             rotation: 0.0f,
                             color: Color);
        }
    }
}