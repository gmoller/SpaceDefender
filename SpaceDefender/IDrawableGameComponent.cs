using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal interface IDrawableGameComponent
    {
        Rectangle BoundingBox { get; }
        Circle BoundingCircle { get; }
        void Update(GameTime gameTime, KeyboardState keyboardState);
        void Draw(SpriteBatch spriteBatch);
    }
}