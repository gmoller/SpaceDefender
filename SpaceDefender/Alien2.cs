using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Alien2 : IGameObject
    {
        private readonly Texture2D _texture;
        private Vector2 _centerPosition;
        private readonly Vector2 _origin;
        private readonly Vector2 _scale;
        private float _rotation;
        private readonly Color _color;

        private Vector2 _movementVector;

        internal Alien2(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _centerPosition = position;
            _origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
            _scale = new Vector2(0.3f, 0.3f);
            
            _rotation = 0.0f; // North
            //_rotation = Convert.ToSingle(0.5f * Math.PI); // East
            //_rotation = Convert.ToSingle(Math.PI); // South
            //_rotation = Convert.ToSingle(1.5f * Math.PI); // West

            _color = Color.White;

            _movementVector = new Vector2(0.0f, 0.0f);    // Nowhere
            //_movementVector = new Vector2(0.0f, -1.0f); // North
            //_movementVector = new Vector2(1.0f, 0.0f);  // East
            //_movementVector = new Vector2(0.0f, 1.0f);  // South
            //_movementVector = new Vector2(-1.0f, 0.0f); // West
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            const float epsilon = 0.0001f;

            if (Math.Abs(_movementVector.X - 0) < epsilon && Math.Abs(_movementVector.Y - 0) < epsilon)
            {
                return;
            }

            // convert movementVector to radians (or degrees)
            double angleFromVector = Math.Atan2(_movementVector.X, -_movementVector.Y);

            // and then get rotation closer to the angle
            AdjustRotation(angleFromVector);

            int distance = gameTime.ElapsedGameTime.Milliseconds / 10;

            _centerPosition += _movementVector * distance;
        }

        private void AdjustRotation(double desiredRotation)
        {
            if (_rotation < desiredRotation)
            {
                _rotation += 0.02f;
            }
            else if (_rotation > desiredRotation)
            {
                _rotation -= 0.02f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                             _centerPosition,
                             origin: _origin,
                             scale: _scale,
                             rotation: _rotation + (float)Math.PI,
                             color: _color);
        }
    }
}