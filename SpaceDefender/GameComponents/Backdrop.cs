using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    internal class Backdrop : GameComponent, IDrawableGameComponent
    {
        internal Backdrop(int viewportWidth, int viewportHeight)
            : base(viewportWidth, viewportHeight)
        {
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("stars");
        }

        public void Update(GameTime gameTime, InputState inputState)
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

        public bool IsAlive
        {
            get { return true; }
            set { }
        }
    }
}