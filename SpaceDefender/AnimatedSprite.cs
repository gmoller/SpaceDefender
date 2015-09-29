using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SpaceDefender
{
    public class AnimatedSprite : DrawableGameComponent
    {
        private readonly string _assetName;
        private int Rows { get; set; }
        private int Columns { get; set; }
        private readonly int _row;
        private int _currentFrame;
        private readonly int _totalFrames;
        private readonly int _startFrame;
        private readonly int _endFrame;

        private float _width;
        private float _height;

        // Amount of time (in milliseconds) to display each frame
        private readonly int _frameLength;

        // Amount of time (in milliseconds) that has passed since we last animated
        private float _frameTimer;

        private bool _isAnimating;

        public AnimatedSprite(string assetName, Vector2 position, int rows, int columns, int row, int frameLength = 20)
            : base(position)
        {
             if (row >= rows)
            {
                throw new ApplicationException("Row too large.");
            }

            _assetName = assetName;
            Rows = rows;
            Columns = columns;
            _row = row;
            _frameLength = frameLength;
            _currentFrame = row * 16;
            _totalFrames = Columns;
            _startFrame = _currentFrame;
            _endFrame = _startFrame + _totalFrames;
            Scale = new Vector2(1.5f, 1.5f) * new Vector2(GameRoot.ScreenSize.X / 1280, GameRoot.ScreenSize.Y / 720);
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>(_assetName);
            _width = (float)Texture.Width / Columns;
            _height = (float)Texture.Height / Rows;
            Origin = new Vector2(_width / 2.0f, _height / 2.0f);
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (_isAnimating)
            {
                _frameTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_frameTimer > _frameLength)
                {
                    _frameTimer = 0.0f;
                    _currentFrame++;
                    //_currentFrame = (_currentFrame + 1) % _totalFrames; 
                    if (_currentFrame >= _endFrame)
                    {
                        _currentFrame = _row * 16;
                        _isAnimating = false;
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
                                 position: TopLeftPosition,
                                 scale: Scale,
                                 rotation: Rotation,
                                 color: Color,
                                 effects: SpriteEffect,
                                 sourceRectangle: sourceRectangle);
            }
        }

        public void StartAnimating()
        {
            _isAnimating = true;
        }

        public void StopAnimating()
        {
            _isAnimating = false;
        }
    }
}