using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary
{
    public class AnimatedSpriteNew : MyDrawableGameComponent
    {
        protected string AssetName { get; private set; }
        protected string AnimationSequenceId { get; private set; }

        private Dictionary<string, AnimationSequence> _animationSequenceList = new Dictionary<string, AnimationSequence>();

        // Amount of time (in milliseconds) to display each frame
        private readonly int _frameLength;

        // Amount of time (in milliseconds) that has passed since we last animated
        private float _frameTimer;

        public bool IsPlaying { get; set; }

        public bool IsLooping { get; set; }

        protected AnimatedSpriteNew(string assetName, Vector2 centerPosition, string animationSequenceId, int framesPerSecond = 60)
            : base(centerPosition)
        {
            AssetName = assetName;
            AnimationSequenceId = animationSequenceId;
            _frameLength = (int)((1.0f / framesPerSecond) * 1000.0f);
        }

        public override void LoadContent(ContentManager content)
        {
            var reader = new TextureAtlasReader();
            Sprite = new Sprite(reader.Read(content.Load<Texture2D>(AssetName)))
            {
                OriginNormalized = new Vector2(0.5f, 0.5f)
            };

            var reader2 = new AnimationSequenceReader();
            _animationSequenceList = reader2.Read(AssetName);
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (IsPlaying)
            {
                _frameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_frameTimer > _frameLength)
                {
                    _frameTimer = 0.0f;
                    Sprite.TextureAtlas.Frame = _animationSequenceList[AnimationSequenceId].GetNextFrame();
                    if (Sprite.TextureAtlas.Frame == -1)
                    {
                        Sprite.TextureAtlas.Frame = _animationSequenceList[AnimationSequenceId].StartFrame;
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
            Sprite.TextureAtlas.Frame = _animationSequenceList[AnimationSequenceId].StartFrame;
            IsPlaying = true;
            IsAlive = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Stop()
        {
            Sprite.TextureAtlas.Frame = _animationSequenceList[AnimationSequenceId].StartFrame;
            IsPlaying = false;
            IsAlive = false;
        }
    }
}