using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    internal class Alien2 : DrawableGameComponent, IDrawableGameComponent
    {
        internal Alien2(Texture2D texture, Vector2 position, int viewportWidth, int viewportHeight)
            : base(texture, position, viewportWidth, viewportHeight)
        {
            SpriteEffect = SpriteEffects.FlipVertically;
            //Rotation = 0.0f; // North
            //Rotation = Convert.ToSingle(0.5f * Math.PI); // East
            //Rotation = Convert.ToSingle(Math.PI); // South
            //Rotation = Convert.ToSingle(1.5f * Math.PI); // West

            //MovementVector = new Vector2(0.0f, -1.0f); // North
            //MovementVector = new Vector2(1.0f, 0.0f);  // East
            //MovementVector = new Vector2(0.0f, 1.0f);  // South
            //MovementVector = new Vector2(-1.0f, 0.0f); // West
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            const float epsilon = 0.0001f;

            if (Math.Abs(MovementVector.X - 0) < epsilon && Math.Abs(MovementVector.Y - 0) < epsilon)
            {
                return;
            }

            // convert movementVector to radians (or degrees)
            double angleFromVector = Math.Atan2(MovementVector.X, -MovementVector.Y);

            // and then get rotation closer to the angle
            AdjustRotation(angleFromVector);

            int distance = gameTime.ElapsedGameTime.Milliseconds / 10;

            CenterPosition += MovementVector * distance;
        }

        private void AdjustRotation(double desiredRotation)
        {
            if (Rotation < desiredRotation)
            {
                Rotation += 0.02f;
            }
            else if (Rotation > desiredRotation)
            {
                Rotation -= 0.02f;
            }
        }
    }
}