using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    internal class Alien : DrawableGameComponent
    {
        private readonly Random _random;

        internal Alien(Vector2 position, int viewportWidth, int viewportHeight)
            : base(position, viewportWidth, viewportHeight)
        {
            SpriteEffect = SpriteEffects.FlipVertically;
            _random = new Random();
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("ship (3)");
            SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
            BoundingSize = new Point { X = Texture.Width, Y = Texture.Height };
            BoundingSize.X = 250;
        }

        public override void Update(GameTime gameTime, InputState inputState)
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