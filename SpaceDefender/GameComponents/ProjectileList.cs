using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class ProjectileList : DrawableGameComponent, IEnumerable<IDrawableGameComponent>
    {
        private readonly List<IDrawableGameComponent> _projectiles = new List<IDrawableGameComponent>(10);

        public ProjectileList(Vector2 position)
            : base(position)
        {
            for (int i = 0; i < 10; i++)
            {
                _projectiles.Add(new Projectile(position));
            }
        }

        public override void LoadContent(ContentManager content)
        {
            foreach (IDrawableGameComponent item in _projectiles)
            {
                item.LoadContent(content);
            }
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            foreach (IDrawableGameComponent item in _projectiles)
            {
                item.Update(gameTime, inputState);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (IDrawableGameComponent item in _projectiles)
            {
                if (item.IsAlive)
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        public IEnumerator<IDrawableGameComponent> GetEnumerator()
        {
            return _projectiles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _projectiles.GetEnumerator();
        }
    }

    public class Projectile : DrawableGameComponent
    {
        public Projectile(Vector2 position)
            : base(position)
        {
            SourceRectangle = new Rectangle(13, 12, 6, 10);
            Origin = new Vector2(3.0f, 5.0f);
            BoundingSize = new Point { X = 10, Y = 10 };
            Scale = new Vector2(1.0f, 1.0f);
            IsAlive = false;
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("M484BulletCollection1");
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            // do nothing if MovementVector is zero
            if (MovementVector.X.ApproximatelyEquals(0) && MovementVector.Y.ApproximatelyEquals(0))
            {
                return;
            }

            float distance = gameTime.ElapsedGameTime.Milliseconds / 2.0f;
            Vector2 newPosition = CenterPosition + (MovementVector * distance);

            BoundsCheck check = WithinScreenBounds(newPosition);
            if (check == BoundsCheck.InBounds)
            {
                CenterPosition = newPosition;
            }
            else
            {
                IsAlive = false;
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

        public void SetMovementVector(Vector2 v)
        {
            MovementVector = v;
        }

        public void SetCenterPosition(Vector2 v)
        {
            CenterPosition = v;
        }
    }
}