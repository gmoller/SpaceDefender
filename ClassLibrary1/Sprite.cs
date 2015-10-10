using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary
{
    public class Sprite
    {
        public TextureAtlas TextureAtlas { get; private set; }

        // Origin offsets the CenterPosition when drawing to the screen. So top-left is CenterPosition - Origin.
        public Vector2 Origin { get; private set; }

        public Vector2 OriginNormalized
        {
            get { return new Vector2(Origin.X / TextureAtlas.SingleTextureWidth, Origin.Y / TextureAtlas.SingleTextureHeight); }
            set { Origin = new Vector2(TextureAtlas.SingleTextureWidth * value.X, TextureAtlas.SingleTextureHeight * value.Y); }
        }

        public Vector2 Scale { get; set; }

        // Amount to rotate the texture by (in radians) when drawing to the screen. 0 * PI = no rotation, 0.5 * PI = 90 degrees, 1 * PI = 180 degrees, 1.5 * PI = 270.
        public float Rotation { get; set; }

        public Color Color { get; private set; }

        public SpriteEffects SpriteEffect { get; private set; }

        public Sprite(TextureAtlas textureAtlas, Color color = default(Color), SpriteEffects spriteEffects = SpriteEffects.None)
        {
            TextureAtlas = textureAtlas;
            Color = color == default(Color) ? Color.White : color;
            SpriteEffect = spriteEffects;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 centerPosition)
        {
            spriteBatch.Draw(TextureAtlas.Texture,
                             centerPosition,
                             TextureAtlas.FrameRectangle,
                             origin: Origin,
                             scale: Scale,
                             rotation: Rotation,
                             color: Color,
                             effects: SpriteEffect,
                             layerDepth: 0.0f);

            spriteBatch.DrawCircle(centerPosition, 1, Color.RoyalBlue);
        }
    }
}