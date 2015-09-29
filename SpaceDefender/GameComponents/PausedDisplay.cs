using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class PausedDisplay
    {
        private const string MESSAGE = "Game Paused";

        private SpriteFont _font;
        private Vector2 _messageLength;
        private Vector2 _padding;

        public PausedDisplay()
        {
            _padding = new Vector2(20.0f, 10.0f);
        }

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("arial-32");
            _messageLength = _font.MeasureString(MESSAGE);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, MESSAGE, new Vector2((GameRoot.ScreenSize.X - _messageLength.X) / 2.0f, _padding.Y), Color.Red);
        }
    }
}