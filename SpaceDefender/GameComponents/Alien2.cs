using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    internal class Alien2 : DrawableGameComponent
    {
        internal Alien2(Vector2 position, int viewportWidth, int viewportHeight)
            : base(position, viewportWidth, viewportHeight)
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

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("ship (4)");
            SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
            BoundingSize = new Point { X = Texture.Width, Y = Texture.Height };
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            if (IsAlive)
            {
                // do nothing if MovementVector is zero
                if (MovementVector.X.ApproximatelyEquals(0) && MovementVector.Y.ApproximatelyEquals(0))
                {
                    return;
                }

                AdjustRotation(gameTime);

                // calculate new sprite position
                float distance = gameTime.ElapsedGameTime.Milliseconds/2.0f;
                Vector2 newPosition = CenterPosition + (MovementVector*distance);

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
        }

        private void AdjustRotation(GameTime gameTime)
        {
            float amountToRotate = gameTime.ElapsedGameTime.Milliseconds / 250.0f;

            // convert movementVector to radians (or degrees)
            double desiredRotation = Math.Atan2(MovementVector.X, -MovementVector.Y);

            // get rotation closer to the MovementVector angle
            if (!Rotation.ApproximatelyEquals(desiredRotation, 0.01f))
            {
                if (Rotation < desiredRotation)
                {
                    Rotation += amountToRotate;
                }
                else if (Rotation > desiredRotation)
                {
                    Rotation -= amountToRotate;
                }
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