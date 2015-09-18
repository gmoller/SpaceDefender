using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    internal class Alien : IGameObject
    {
        private readonly Random _random;
        private readonly Texture2D _texture;
        private Vector2 _centerPosition;
        private readonly Vector2 _origin;
        private readonly Vector2 _scale;
        private readonly float _rotation;
        private readonly Color _color;

        internal Alien(Texture2D texture, Vector2 position)
        {
            _random = new Random();
            _texture = texture;
            _centerPosition = position;
            _origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
            _scale = new Vector2(0.3f, 0.3f);
            _rotation = 0.0f;
            _color = Color.White;
        }

        internal void MoveRandomly(GameTime gameTime)
        {
            int rnd = _random.Next(0, 10);

            int distance = gameTime.ElapsedGameTime.Milliseconds / 10;

            Vector2 movementVector = Vector2.Zero;

            switch (rnd)
            {
                case 0:
                    // move up
                    movementVector.Y -= 1.0f;
                    break;
                case 1:
                    // move right
                    movementVector.X += 1.0f;
                    break;
                case 2:
                    // move down
                    movementVector.Y += 1.0f;
                    break;
                case 3:
                    // move left
                    movementVector.X -= 1.0f;
                    break;
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