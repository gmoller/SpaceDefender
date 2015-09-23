using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    internal class PausedDisplay : GameComponent, IDrawableGameComponent
    {
        private const string MESSAGE = "Game Paused";

        private SpriteFont _font;
        private Vector2 _messageLength;
        private Vector2 _padding;

        internal PausedDisplay(int viewportWidth, int viewportHeight)
            : base(viewportWidth, viewportHeight)
        {
            _padding = new Vector2(20.0f, 10.0f);
        }

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("arial-32");
            _messageLength = _font.MeasureString(MESSAGE);
        }

        public void Update(GameTime gameTime, InputState inputState)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, MESSAGE, new Vector2((ViewportWidth - _messageLength.X) / 2.0f, _padding.Y), Color.Red);
        }

        public bool IsAlive
        {
            get { return true; }
            set { }
        }
    }
}