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

            MovementVector = new Vector2(1.0f, 0.3f);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            const float epsilon = 0.0001f;

            // do nothing if MovementVector is zero
            if (Math.Abs(MovementVector.X - 0) < epsilon && Math.Abs(MovementVector.Y - 0) < epsilon)
            {
                return;
            }

            AdjustRotation(gameTime);

            // calculate new sprite position
            float distance = gameTime.ElapsedGameTime.Milliseconds / 2.0f;
            Vector2 newPosition = CenterPosition + (MovementVector * distance);

            BoundsCheck check = WithinScreenBounds(newPosition);
            if (check == BoundsCheck.InBounds)
            {
                CenterPosition = newPosition;
            }
            else
            {
                if (check == BoundsCheck.OutsideLeftOrRight)
                {
                    MovementVector.X = -MovementVector.X;
                }
                else
                {
                    MovementVector.Y = -MovementVector.Y;
                }
            }
        }

        private void AdjustRotation(GameTime gameTime)
        {
            float amountToRotate = gameTime.ElapsedGameTime.Milliseconds / 500.0f;

            // convert movementVector to radians (or degrees)
            double desiredRotation = Math.Atan2(MovementVector.X, -MovementVector.Y);

            // get rotation closer to the MovementVector angle
            if (Rotation < desiredRotation)
            {
                Rotation += amountToRotate;
            }
            else if (Rotation > desiredRotation)
            {
                Rotation -= amountToRotate;
            }
        }

        private BoundsCheck WithinScreenBounds(Vector2 newPosition)
        {
            if (newPosition.X < 50 || newPosition.X > ViewportWidth - 50)
            {
                return BoundsCheck.OutsideLeftOrRight;
            }

            if (newPosition.Y < 50 || newPosition.Y > ViewportHeight - 50)
            {
                return BoundsCheck.OutsideTopOrBottom;
            }

            return BoundsCheck.InBounds;
        }
    }
}