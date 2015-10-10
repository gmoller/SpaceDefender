using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary
{
    public class AnimatedSprite : MyDrawableGameComponent
    {
        protected string AssetName { get; private set; }
        protected int Rows { get; private set; }
        protected int Columns { get; private set; }
        protected int Row { get; private set; }
        private readonly int _totalFrames;
        private readonly int _startFrame;
        private readonly int _endFrame;

        // Amount of time (in milliseconds) to display each frame
        private readonly int _frameLength;

        // Amount of time (in milliseconds) that has passed since we last animated
        private float _frameTimer;

        public bool IsPlaying { get; set; }

        public bool IsLooping { get; set; }

        protected AnimatedSprite(string assetName, Vector2 centerPosition, int rows, int columns, int row, int framesPerSecond = 60)
            : base(centerPosition)
        {
            if (row >= rows)
            {
                throw new ApplicationException("Row too large.");
            }

            AssetName = assetName;
            Rows = rows;
            Columns = columns;
            Row = row;
            _frameLength = (int)((1.0f / framesPerSecond) * 1000.0f);
            _totalFrames = Columns;
            _startFrame = row * _totalFrames;
            _endFrame = _startFrame + _totalFrames - 1;
        }

        public override void LoadContent(ContentManager content)
        {
            var reader = new TextureAtlasReader();
            Sprite = new Sprite(reader.Read(content.Load<Texture2D>(AssetName)))
            {
                OriginNormalized = new Vector2(0.5f, 0.5f)
            };
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (IsPlaying)
            {
                _frameTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_frameTimer > _frameLength)
                {
                    _frameTimer = 0.0f;
                    Sprite.TextureAtlas.Frame++;
                    if (Sprite.TextureAtlas.Frame > _endFrame)
                    {
                        Sprite.TextureAtlas.Frame = Row * Columns;
                        if (!IsLooping)
                        {
                            IsPlaying = false;
                            IsAlive = false;
                        }
                    }
                }
            }
        }

        public void Play()
        {
            Sprite.TextureAtlas.Frame = Row * Columns;
            IsPlaying = true;
            IsAlive = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Stop()
        {
            Sprite.TextureAtlas.Frame = _startFrame;
            IsPlaying = false;
            IsAlive = false;
        }
    }
}