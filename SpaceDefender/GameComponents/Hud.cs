using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    internal class Hud : GameComponent, IDrawableGameComponent
    {
        private SpriteFont _font;
        private Vector2 _padding;

        internal int Score { get; set; }
        internal int Lives { get; set; }

        internal Hud(int viewportWidth, int viewportHeight)
            : base(viewportWidth, viewportHeight)
        {
            _padding = new Vector2(20.0f, 10.0f);
        }

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("arial-32");
        }

        public void Update(GameTime gameTime, InputState inputState)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string score = string.Format("Score: {0,5:D5}", Score);
            string lives = string.Format("Lives: {0,1:D1}", Lives);
            Vector2 livesLength = _font.MeasureString(lives);

            spriteBatch.DrawString(_font, score, new Vector2(_padding.X, _padding.Y), Color);
            spriteBatch.DrawString(_font, lives, new Vector2(ViewportWidth - livesLength.X - _padding.X, _padding.Y), Color);
        }

        public bool IsAlive
        {
            get { return true; }
            set { }
        }
    }
}