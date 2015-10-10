using System;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Alien2 : GameLibrary.MyDrawableGameComponent
    {
        public Alien2(Vector2 centerPosition)
            : base(centerPosition)
        {
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
            Sprite = new Sprite(new TextureAtlas(content.Load<Texture2D>("Ship"), new[] { new Rectangle(958, 450, 282, 320) }), spriteEffects: SpriteEffects.FlipVertically)
                {
                    Scale = new Vector2(0.2f, 0.2f)*new Vector2(GameRoot.ScreenSize.X/1280.0f, GameRoot.ScreenSize.Y/720.0f),
                    OriginNormalized = new Vector2(0.5f, 0.5f)
                };
            BoundingSize = new Point { X = Sprite.TextureAtlas.SingleTextureWidth, Y = Sprite.TextureAtlas.SingleTextureHeight };
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
            if (!Sprite.Rotation.ApproximatelyEquals(desiredRotation, 0.01f))
            {
                if (Sprite.Rotation < desiredRotation)
                {
                    Sprite.Rotation += amountToRotate;
                }
                else if (Sprite.Rotation > desiredRotation)
                {
                    Sprite.Rotation -= amountToRotate;
                }
            }
        }

        private BoundsCheck WithinScreenBounds(Vector2 newPosition)
        {
            if (newPosition.X < 50 || newPosition.X > GameRoot.ScreenSize.X - 50)
            {
                return BoundsCheck.OutsideLeftOrRight;
            }

            if (newPosition.Y < 50 || newPosition.Y > GameRoot.ScreenSize.Y - 50)
            {
                return BoundsCheck.OutsideTopOrBottom;
            }

            return BoundsCheck.InBounds;
        }
    }
}