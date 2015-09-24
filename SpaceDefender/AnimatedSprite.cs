using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SpaceDefender
{
    internal class AnimatedSprite : DrawableGameComponent
    {
        private int Rows { get; set; }
        private int Columns { get; set; }
        private int _currentFrame;
        private readonly int _totalFrames;

        private float _width;
        private float _height;

        // Amount of time (in milliseconds) to display each frame
        private const int FRAME_LENGTH = 50;

        // Amount of time that has passed since we last animated
        private float _frameTimer;

        public AnimatedSprite(Vector2 position, int viewportWidth, int viewportHeight)
            : base(position, viewportWidth, viewportHeight)
        {
            Rows = 4;
            Columns = 4;
            _currentFrame = 0;
            _totalFrames = Rows * Columns;
            Scale = new Vector2(2.0f, 2.0f);
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("exp2_0");
            _width = (float)Texture.Width / Columns;
            _height = (float)Texture.Height / Rows;
            Origin = new Vector2(_width / 2.0f, _height / 2.0f);
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (IsAlive)
            {
                _frameTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_frameTimer > FRAME_LENGTH)
                {
                    _frameTimer = 0.0f;
                    _currentFrame++;
                    if (_currentFrame == _totalFrames)
                    {
                        _currentFrame = 0;
                        IsAlive = false;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                var row = (int) ((float) _currentFrame / Columns);
                int column = _currentFrame % Columns;

                var sourceRectangle = new Rectangle((int)_width * column, (int)_height * row, (int)_width, (int)_height);

                spriteBatch.Draw(texture: Texture,
                                 position: CenterPosition,
                                 origin: Origin,
                                 scale: Scale,
                                 rotation: Rotation,
                                 color: Color,
                                 effects: SpriteEffect,
                                 sourceRectangle: sourceRectangle);
            }
        }
    }
}