using System;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Alien : GameLibrary.MyDrawableGameComponent
    {
        private readonly Random _random;

        public Alien(Vector2 centerPosition)
            : base(centerPosition)
        {
            _random = new Random();
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = new Sprite(new TextureAtlas(content.Load<Texture2D>("Ship"), new[] { new Rectangle(1718, 0, 317, 235) }), spriteEffects: SpriteEffects.FlipVertically)
                {
                    Scale = new Vector2(0.2f, 0.2f) * new Vector2(GameRoot.ScreenSize.X / 1280.0f, GameRoot.ScreenSize.Y / 720.0f),
                    OriginNormalized = new Vector2(0.5f, 0.5f)
                };
            BoundingSize = new Point { X = Sprite.TextureAtlas.SingleTextureWidth, Y = Sprite.TextureAtlas.SingleTextureHeight };
            BoundingSize.X = 250;
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (IsAlive)
            {
                int rnd = _random.Next(0, 10);

                float distance = gameTime.ElapsedGameTime.Milliseconds/10.0f;

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
}