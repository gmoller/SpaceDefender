using CollisionDetectionLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Hud : IDrawableGameComponent
    {
        private readonly Color _color = Color.White;
        private SpriteFont _font;
        private Vector2 _padding;
        private Vector2 _scale;

        public int Score { get; set; }
        public int Lives { private get; set; }

        public Hud()
        {
            _padding = new Vector2(20.0f, 10.0f);
            _scale = new Vector2(GameRoot.ScreenSize.X / 1280, GameRoot.ScreenSize.Y / 720);
        }

        public bool IsAlive
        {
            get { return true; }
            set { }
        }

        Vector2 IDrawableGameComponent.CenterPosition
        {
            get { return Vector2.Zero; }
            set { }
        }

        public Circle BoundingCircle
        {
            get { return new Circle(); }
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
            Vector2 livesLength = _font.MeasureString(lives) * _scale;

            spriteBatch.DrawString(_font, score, new Vector2(_padding.X, _padding.Y), _color, 0.0f, Vector2.Zero, _scale, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, lives, new Vector2(GameRoot.ScreenSize.X - livesLength.X - _padding.X, _padding.Y), _color, 0.0f, Vector2.Zero, _scale, SpriteEffects.None, 0.0f);
        }
    }
}