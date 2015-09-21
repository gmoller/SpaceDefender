using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    internal class GameComponent
    {
        protected Texture2D Texture;
        protected Color Color;

        protected int ViewportWidth;
        protected int ViewportHeight;

        internal GameComponent(Texture2D texture, int viewportWidth, int viewportHeight)
        {
            Texture = texture;
            Color = Color.White;
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
        }
    }
}