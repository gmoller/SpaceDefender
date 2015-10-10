using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SpaceDefender.GameComponents
{
    public class Backdrop : GameLibrary.MyDrawableGameComponent
    {
        private readonly List<Texture2D> _textures = new List<Texture2D>();
        private Vector2 _scale;

        public Backdrop(Vector2 centerPosition)
            : base(centerPosition)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            var texture1 = content.Load<Texture2D>("bg_1_1");
            _textures.Add(texture1);

            var texture2 = content.Load<Texture2D>("bg_1_1");
            _textures.Add(texture2);

            var texture3 = content.Load<Texture2D>("bg_1_1");
            _textures.Add(texture3);

            _scale = new Vector2(GameRoot.ScreenSize.X / texture1.Width, GameRoot.ScreenSize.Y / texture1.Height);
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            var direction = new Vector2(0.0f, 1.0f);
            var velocity = new Vector2(0.0f, 200.0f);

            CenterPosition += direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            float scaledSizeY = _textures[0].Height * _scale.Y;

            if (CenterPosition.Y > (scaledSizeY * (_textures.Count == 1 ? 1 : _textures.Count - 1)) + (scaledSizeY / 2.0f))
            {
                CenterPosition = new Vector2(GameRoot.ScreenSize.X / 2.0f, (GameRoot.ScreenSize.Y / 2.0f) - scaledSizeY);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                float scaledSizeY = _textures[0].Height * _scale.Y;

                Draw(spriteBatch, _textures[_textures.Count - 1], new Vector2(CenterPosition.X, CenterPosition.Y + scaledSizeY), Color.Red);

                var centerPosition2 = CenterPosition;
                int i = 0;
                foreach (Texture2D texture in _textures)
                {
                    Color col;
                    if (i == 0)
                        col = Color.White;
                    else if (i == 1)
                        col = Color.Blue;
                    else
                        col = Color.Red;

                    Draw(spriteBatch, texture, centerPosition2, col);

                    centerPosition2.Y = centerPosition2.Y - scaledSizeY;

                    i++;
                }

                Draw(spriteBatch, _textures[_textures.Count - 1], centerPosition2, Color.Blue);
            }
        }

        private void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 centerPosition, Color color)
        {
            Vector2 origin = new Vector2(texture.Width/2.0f, texture.Height/2.0f);
            Vector2 topLeft = centerPosition - origin * _scale;
            Vector2 bottomRight = centerPosition + origin * _scale;

            if (topLeft.X >= GameRoot.ScreenSize.X || topLeft.Y >= GameRoot.ScreenSize.Y)
            {
                return;
            }

            if (bottomRight.X <= 0 || bottomRight.Y <= 0)
            {
                return;
            }

            spriteBatch.Draw(texture: texture,
                             position: centerPosition,
                             sourceRectangle: new Rectangle(0, 0, texture.Width, texture.Height),
                             origin: origin,
                             scale: _scale,
                             rotation: 0.0f,
                             color: color,
                             effects: SpriteEffects.None,
                             layerDepth: 0.0f);
        }
    }
}