using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Player : IGameObject
    {
        private readonly Texture2D _texture;
        private Vector2 _centerPosition;
        private readonly Vector2 _origin;
        private readonly Vector2 _scale;
        private readonly float _rotation;
        private readonly Color _color;

        internal Player(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _centerPosition = position;
            _origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
            _scale = new Vector2(0.3f, 0.3f);
            _rotation = 0.0f;
            _color = Color.LightGreen;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Vector2 movementVector = Vector2.Zero;

            float distance = gameTime.ElapsedGameTime.Milliseconds / 10.0f;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                movementVector.X += 3.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                movementVector.X -= 3.0f;
            }

            _centerPosition += movementVector * distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                             _centerPosition,
                             origin: _origin,
                             scale: _scale,
                             rotation: _rotation,
                             color: _color);
        }
    }
}