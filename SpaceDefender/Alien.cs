using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Alien : DrawableGameComponent, IDrawableGameComponent
    {
        private readonly Random _random;

        internal Alien(Texture2D texture, Vector2 position, int viewportWidth, int viewportHeight)
            : base(texture, position, viewportWidth, viewportHeight)
        {
            SpriteEffect = SpriteEffects.FlipVertically;
            _random = new Random();
            BoundingSize.X = 250;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            int rnd = _random.Next(0, 10);

            float distance = gameTime.ElapsedGameTime.Milliseconds / 10.0f;

            MovementVector = Vector2.Zero;

            switch (rnd)
            {
                case 0:
                    // move up
                    MovementVector.Y -= 1.0f;
                    break;
                case 1:
                    // move right
                    MovementVector.X += 1.0f;
                    break;
                case 2:
                    // move down
                    MovementVector.Y += 1.0f;
                    break;
                case 3:
                    // move left
                    MovementVector.X -= 1.0f;
                    break;
            }

            CenterPosition += MovementVector * distance;
        }
    }
}