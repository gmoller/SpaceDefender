using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Hud : IGameObject
    {
        private readonly SpriteFont _font;
        private Vector2 _padding;
        private readonly Color _color;
        private readonly int _viewportWidth;

        internal int Score { get; set; }
        internal int Lives { get; set; }

        internal Hud(SpriteFont font, int viewportWidth)
        {
            _font = font;
            _padding = new Vector2(20.0f, 10.0f);
            _color = Color.White;
            _viewportWidth = viewportWidth;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string score = string.Format("Score: {0,5:D5}", Score);
            string lives = string.Format("Lives: {0,1:D1}", Lives);
            Vector2 livesLength = _font.MeasureString(lives);

            spriteBatch.DrawString(_font, score, new Vector2(_padding.X, _padding.Y), _color);
            spriteBatch.DrawString(_font, lives, new Vector2(_viewportWidth - livesLength.X - _padding.X, _padding.Y), _color);
        }
    }
}