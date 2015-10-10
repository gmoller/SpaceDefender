using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class PlayerShip : GameLibrary.MyDrawableGameComponent
    {
        private Facing _facing = Facing.Right;
        private bool _isThrusting;
        private int _scrollRate;
        private int _shipAccelerationRate = 1;
        private int _shipVerticalMoveRate = 3;
        private float _speedChangeCount;
        private float _speedChangeDelay = 0.1f;
        private float _verticalChangeCount;
        private float _verticalChangeDelay = 0.01f;
        private int _maxHorizontalSpeed = 8;
        public float WorldX { get; set; }

        private readonly Bullets _bullets;

        public int ScrollRate { get { return _scrollRate; } }

        public PlayerShip(Vector2 centerPosition, Bullets bullets)
            : base(centerPosition)
        {
            _bullets = bullets;
            WorldX = centerPosition.X;
        }

        public override void LoadContent(ContentManager content)
        {
            var reader = new TextureAtlasReader();
            Sprite = new Sprite(reader.Read(content.Load<Texture2D>("PlayerShip")))
                {
                    Scale = new Vector2(2.0f, 2.0f) * new Vector2(GameRoot.ScreenSize.X / 1280.0f, GameRoot.ScreenSize.Y / 720.0f),
                    OriginNormalized = new Vector2(0.5f, 0.5f)
                }; // Color.SeaGreen;
            BoundingSize = new Point { X = Sprite.TextureAtlas.SingleTextureWidth, Y = Sprite.TextureAtlas.SingleTextureHeight };
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (_facing == Facing.Right)
            {
                Sprite.TextureAtlas.Frame = _isThrusting ? 1 : 0;
            }
            else // Left
            {
                Sprite.TextureAtlas.Frame = _isThrusting ? 3 : 2;
            }

            _speedChangeCount += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_speedChangeCount > _speedChangeDelay)
            {
                CheckHorizontalMovementKeys(gameTime, inputState);
            }

            _verticalChangeCount += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_verticalChangeCount > _verticalChangeDelay)
            {
                CheckVerticalMovementKeys(gameTime, inputState);
            }

            if (inputState.IsSpace(PlayerIndex.One))
            {
                _bullets.Fire(CenterPosition, _facing);
            }
        }

        private void CheckHorizontalMovementKeys(GameTime gameTime, InputState inputState)
        {
            bool resetTimer = false;

            _isThrusting = false;
            if (inputState.IsRight(PlayerIndex.One))
            {
                if (_scrollRate < _maxHorizontalSpeed)
                {
                    _scrollRate += _shipAccelerationRate;
                    
                    if (_scrollRate > _maxHorizontalSpeed)
                    {
                        _scrollRate = _maxHorizontalSpeed;
                    }
                    resetTimer = true;
                }
                _isThrusting = true;
                _facing = Facing.Right;
            }

            if (inputState.IsLeft(PlayerIndex.One))
            {
                if (_scrollRate > -_maxHorizontalSpeed)
                {
                    _scrollRate -= _shipAccelerationRate;
                    if (_scrollRate < -_maxHorizontalSpeed)
                    {
                        _scrollRate = -_maxHorizontalSpeed;
                    }
                    resetTimer = true;
                }
                _isThrusting = true;
                _facing = Facing.Left;
            }

            if (resetTimer)
            {
                _speedChangeCount = 0.0f;
            }
        }

        private void CheckVerticalMovementKeys(GameTime gameTime, InputState inputState)
        {
            bool resetTimer = false;

            MovementVector = Vector2.Zero;

            if (inputState.IsUp(PlayerIndex.One))
            {
                if (CenterPosition.Y > PlayArea.Top)
                {
                    float distanceToMove = gameTime.ElapsedGameTime.Milliseconds / 16.0f;
                    MovementVector = new Vector2(0.0f, -_shipVerticalMoveRate * distanceToMove);
                    resetTimer = true;
                }
            }

            if (inputState.IsDown(PlayerIndex.One))
            {
                if (CenterPosition.Y < PlayArea.Bottom)
                {
                    float distanceToMove = gameTime.ElapsedGameTime.Milliseconds / 16.0f;
                    MovementVector = new Vector2(0.0f, _shipVerticalMoveRate * distanceToMove);
                    resetTimer = true;
                }
            }

            CenterPosition += MovementVector;

            if (resetTimer)
            {
                _verticalChangeCount = 0.0f;
            }
        }
    }
}