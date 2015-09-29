using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class PlayerShip : DrawableGameComponent
    {
        private AnimatedSprite _sprite;
        private int _x = 604;
        private int _y = 260;
        private int _facing = 0; // 0 - right, 1 - left
        private bool _isThrusting = false;
        private int _scrollRate = 0;
        private int _shipAccelerationRate = 1;
        private int _shipVerticalMoveRate = 3;
        private float _speedChangeCount = 0.0f;
        private float _speedChangeDelay = 0.1f;
        private float _verticalChangeCount = 0.0f;
        private float _verticalChangeDelay = 0.01f;
        private int _maxHorizontalSpeed = 8;

        public int ScrollRate { get { return _scrollRate; } }

        public Rectangle BoundingBox
        {
            get { return new Rectangle(_x, _y, 72, 16); }
        }

        public PlayerShip(Vector2 centerPosition)
            : base(centerPosition)
        {
            //Color = Color.SeaGreen;
            Scale = new Vector2(2.0f, 2.0f);
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("PlayerShip");
            SourceRectangle = new Rectangle(0, 0, 72, Texture.Height);
            Origin = new Vector2(72 / 2.0f, Texture.Height / 2.0f);
            BoundingSize = new Point { X = 72, Y = Texture.Height };
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (_facing == 0) // right
            {
                SourceRectangle = _isThrusting ? new Rectangle(72, 0, 72, Texture.Height) : new Rectangle(0, 0, 72, Texture.Height);
            }
            else // left
            {
                SourceRectangle = _isThrusting ? new Rectangle(216, 0, 72, Texture.Height) : new Rectangle(144, 0, 72, Texture.Height);
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
                _facing = 0;
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
                _facing = 1;
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