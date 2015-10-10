using System;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Enemies : GameLibrary.MyDrawableGameComponent
    {
        private const int MAX_ENEMIES = 9;

        private readonly IDrawableGameComponent[] _enemies = new IDrawableGameComponent[MAX_ENEMIES];

        public Enemies(Vector2 centerPosition, PlayerShip playerShip)
            : base(centerPosition)
        {
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                _enemies[i] = new Enemy(centerPosition, playerShip);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            foreach (IDrawableGameComponent item in _enemies)
            {
                item.LoadContent(content);
            }
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            foreach (IDrawableGameComponent item in _enemies)
            {
                item.Update(gameTime, inputState);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (IDrawableGameComponent item in _enemies)
            {
                if (item.IsAlive)
                {
                    item.Draw(spriteBatch);
                }
            }
        }
    }

    public class Enemy : GameLibrary.MyDrawableGameComponent
    {
        private static readonly Random Random = new Random();
        private static PlayerShip _playerShip;

        public Enemy(Vector2 centerPosition, PlayerShip playerShip)
            : base(centerPosition)
        {
            IsAlive = false;
            _playerShip = playerShip;
            Generate();
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = new Sprite(new TextureAtlas(content.Load<Texture2D>("Enemy")))
                {
                    Scale = new Vector2(1.0f, 1.0f)*new Vector2(GameRoot.ScreenSize.X/1280.0f, GameRoot.ScreenSize.Y/720.0f),
                    OriginNormalized = new Vector2(0.5f, 0.5f)
                };
            BoundingSize = new Point { X = Sprite.TextureAtlas.SingleTextureWidth, Y = Sprite.TextureAtlas.SingleTextureHeight };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // adjust Y position
            Vector2 originalCenterPosition = CenterPosition;
            CenterPosition = new Vector2(GetDrawX(), CenterPosition.Y);

            base.Draw(spriteBatch);

            // reset Y position
            CenterPosition = originalCenterPosition;
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            float x = MovementVector.X;
            float y = MovementVector.Y;

            x = AdjustX(x);
            var newPosition = CenterPosition * MovementVector;

            if (Random.Next(200) == 1)
            {
                RandomizeMovement();
            }

            if (newPosition.Y < PlayArea.Top)
            {
                newPosition.Y = PlayArea.Top;
                RandomizeMovement();
            }

            if (newPosition.Y > PlayArea.Bottom)
            {
                newPosition.Y = PlayArea.Bottom;
                RandomizeMovement();
            }

            CenterPosition = newPosition;
        }

        private float AdjustX(float x)
        {
            if (x < 0)
            {
                x += PlayArea.MapWidth;
            }
            if (x > PlayArea.MapWidth)
            {
                x -= PlayArea.MapWidth;
            }

            return x;
        }

        private void Generate()
        {
            // Generate a random Y location between iPlayAreaTop 
            // and iPlayAreaBottom (the area of our game screen)
            float y = Random.Next(PlayArea.Top, PlayArea.Bottom);

            // Generate a random X location that is NOT 
            // within 200 pixels of the player's ship.
            float x;
            do
            {
                x = Random.Next(PlayArea.MapWidth);
                CenterPosition = new Vector2(x, y);
            } while (Math.Abs(GetDrawX() - _playerShip.CenterPosition.X) < 200);

            CenterPosition = new Vector2(x, y);
            RandomizeMovement();
            IsAlive = true;
        }

        private float GetDrawX()
        {
            float x = CenterPosition.X - _playerShip.WorldX;
            x = AdjustX(x);

            return x;
        }

        private void RandomizeMovement()
        {
            MovementVector = new Vector2(Random.Next(-50, 50), Random.Next(-50, 50));
            MovementVector.Normalize();

            float speed = Random.Next(3, 6);
            MovementVector *= speed;
        }
    }
}