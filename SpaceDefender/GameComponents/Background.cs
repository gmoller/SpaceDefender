using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Background : DrawableGameComponent
    {
        // Textures to hold the two background images
        private Texture2D _background;
        private Texture2D _parallax;

        private Vector2 _backgroundOffset;
        private Vector2 _parallaxOffset;

        private Vector2 _backgroundScale;
        private Vector2 _parallaxScale;

        // Dimensions of background images scaled to screen size
        private Vector2 _backgroundScaledDimensions;
        private Vector2 _parallaxScaledDimensions;

        // Determines if we will draw the Parallax overlay.
        private bool DrawParallax { get; set; }

        private PlayerShip _playerShip;
        private float _updateDelay = 0.0f;

        private Vector2 BackgroundScale
        {
            get { return _backgroundScale; }
            set
            {
                _backgroundScale = value;
                _backgroundScaledDimensions = new Vector2(_background.Width * _backgroundScale.X, _background.Height * _backgroundScale.Y);
            }
        }

        private Vector2 ParallaxScale
        {
            get { return _backgroundScale; }
            set
            {
                _parallaxScale = value;
                _parallaxScaledDimensions = new Vector2(_parallax.Width * _parallaxScale.X, _parallax.Height * _parallaxScale.Y);
            }
        }

        private Vector2 BackgroundOffset
        {
            get { return _backgroundOffset; }
            set
            {
                _backgroundOffset = value;
                if (_backgroundOffset.X < 0)
                {
                    _backgroundOffset.X += _backgroundScaledDimensions.X;
                }
                if (_backgroundOffset.X > _backgroundScaledDimensions.X)
                {
                    _backgroundOffset.X -= _backgroundScaledDimensions.X;
                }
            }
        }

        private Vector2 ParallaxOffset
        {
            get { return _parallaxOffset; }
            set
            {
                _parallaxOffset = value;
                if (_parallaxOffset.X < 0)
                {
                    _parallaxOffset.X += _parallaxScaledDimensions.X;
                }
                if (_parallaxOffset.X > _parallaxScaledDimensions.X)
                {
                    _parallaxOffset.X -= _parallaxScaledDimensions.X;
                }
            }
        }

        public Background(Vector2 centerPosition, PlayerShip playerShip)
            : base(centerPosition)
        {
            DrawParallax = true;
            _playerShip = playerShip;
        }

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("PrimaryBackground");
            BackgroundScale = new Vector2(GameRoot.ScreenSize.X / _background.Width, GameRoot.ScreenSize.Y / _background.Height);
            _parallax = content.Load<Texture2D>("ParallaxStars");
            ParallaxScale = new Vector2(GameRoot.ScreenSize.X / _parallax.Width, GameRoot.ScreenSize.Y / _parallax.Height);
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            _updateDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_updateDelay > PlayArea.UpdateInterval)
            {
                _updateDelay = 0.0f;
                BackgroundOffset += new Vector2(_playerShip.ScrollRate, 0.0f);
                ParallaxOffset += new Vector2(_playerShip.ScrollRate*2, 0.0f);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the background panel, offset by the player's location
            Draw(spriteBatch, _background, BackgroundOffset, _backgroundScale, Color.White);

            if (DrawParallax)
            {
                // Draw the parallax star field
                Draw(spriteBatch, _parallax, ParallaxOffset, _parallaxScale, Color.SlateGray);
            }
        }

        private void Draw(SpriteBatch spriteBatch, Texture2D image, Vector2 offset, Vector2 scale, Color color)
        {
            int x = -1 * (int)offset.X;
            float scaledWidth = image.Width * scale.X;

            var topLeftPosition = new Vector2(x, 0);
            spriteBatch.Draw(image, topLeftPosition, scale: scale, color: color);

            // If the right edge of the background panel will end
            // within the bounds of the display, draw a second copy
            // of the background at that location.
            bool rightEndEndsWithinScreen = offset.X > scaledWidth - GameRoot.ScreenSize.X;

            if (rightEndEndsWithinScreen)
            {
                topLeftPosition = new Vector2(x + scaledWidth, 0);
                spriteBatch.Draw(image, topLeftPosition, scale: scale, color: color);
            }
        }
    }
}