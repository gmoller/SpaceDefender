using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Backdrop : IGameObject
    {
        private readonly Texture2D _texture;
        private readonly Color _color;
        private readonly int _viewportWidth;
        private readonly int _viewportHeight;

        internal Backdrop(Texture2D texture, int viewportWidth, int viewportHeight)
        {
            _texture = texture;
            _color = Color.White;
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(0, 0, _viewportWidth, _viewportHeight), _color);
        }
    }
}