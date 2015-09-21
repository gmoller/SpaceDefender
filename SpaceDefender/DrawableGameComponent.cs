using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    internal class DrawableGameComponent : GameComponent
    {
        internal DrawableGameComponent(Texture2D texture, Vector2 position, int viewportWidth, int viewportHeight) : base(texture, position, viewportWidth, viewportHeight)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: Texture,
                             position: CenterPosition,
                             origin: Origin,
                             scale: Scale,
                             rotation: Rotation,
                             color: Color,
                             effects: SpriteEffect);

            var color = new Color(0, 128, 0, 128);
            spriteBatch.DrawRectangle(BoundingBox, color, false);
            spriteBatch.DrawCircle(BoundingCircle.Center, BoundingCircle.Radius, color, 1000);
        }
    }
}