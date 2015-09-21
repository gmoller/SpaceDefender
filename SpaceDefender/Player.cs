using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Player : DrawableGameComponent, IDrawableGameComponent
    {
        internal Player(Texture2D texture, Vector2 position, int viewportWidth, int viewportHeight)
            : base(texture, position, viewportWidth, viewportHeight)
        {
            Color = Color.LightGreen;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            MovementVector = Vector2.Zero;

            float distance = gameTime.ElapsedGameTime.Milliseconds / 10.0f;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                MovementVector.X += 3.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                MovementVector.X -= 3.0f;
            }

            CenterPosition += MovementVector * distance;
        }
    }
}