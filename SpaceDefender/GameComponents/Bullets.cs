using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Bullets : GameLibrary.MyDrawableGameComponent
    {
        private const int MAX_BULLETS = 20;

        private readonly IDrawableGameComponent[] _bullets = new IDrawableGameComponent[MAX_BULLETS];

        public Bullets(Vector2 centerPosition)
            : base(centerPosition)
        {
            for (int i = 0; i < MAX_BULLETS; i++)
            {
                _bullets[i] = new Bullet(centerPosition);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            foreach (IDrawableGameComponent item in _bullets)
            {
                item.LoadContent(content);
            }
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            foreach (IDrawableGameComponent item in _bullets)
            {
                item.Update(gameTime, inputState);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (IDrawableGameComponent item in _bullets)
            {
                if (item.IsAlive)
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        public void Fire(Vector2 centerPosition, Facing facing)
        {
            foreach (Bullet item in _bullets)
            {
                if (!item.IsAlive)
                {
                    item.Fire(centerPosition, facing);
                    break;
                }
            }
        }
    }

    public class Bullet : GameLibrary.MyDrawableGameComponent
    {
        private Facing _facing;

        private float _elapsed;
        private const float UPDATE_INTERVAL = 0.015f;

        public Bullet(Vector2 centerPosition)
            : base(centerPosition)
        {
            IsAlive = false;
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = new Sprite(new TextureAtlas(content.Load<Texture2D>("PlayerBullet"), 1, 2))
                {
                    Scale = new Vector2(1.0f, 1.0f) * new Vector2(GameRoot.ScreenSize.X / 1280.0f, GameRoot.ScreenSize.Y / 720.0f),
                    OriginNormalized = new Vector2(0.5f, 0.5f)
                };
            BoundingSize = new Point { X = Sprite.TextureAtlas.SingleTextureWidth, Y = Sprite.TextureAtlas.SingleTextureHeight };
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (IsAlive)
            {
                _elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_elapsed > UPDATE_INTERVAL)
                {
                    _elapsed = 0.0f;

                    float distance = gameTime.ElapsedGameTime.Milliseconds / 16.0f;
                    CenterPosition += (MovementVector * distance);

                    // If the bullet has moved off of the screen,
                    // set it to inactive
                    if ((BottomRightPosition.X > GameRoot.ScreenSize.X) || (TopLeftPosition.X < 0))
                    {
                        IsAlive = false;
                    }
                }
            }
        }

        public void Fire(Vector2 centerPosition, Facing facing)
        {
            _facing = facing;
            Sprite.TextureAtlas.Frame = _facing == Facing.Right ? 0 : 1;

            MovementVector = _facing == Facing.Right ? new Vector2(12.0f, 0.0f) : new Vector2(-12.0f, 0.0f);

            IsAlive = true;
            CenterPosition = centerPosition;
        }
    }
}