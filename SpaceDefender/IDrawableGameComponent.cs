using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal interface IDrawableGameComponent
    {
        void Update(GameTime gameTime, KeyboardState keyboardState);
        void Draw(SpriteBatch spriteBatch);
    }
}