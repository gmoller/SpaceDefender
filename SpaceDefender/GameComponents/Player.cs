using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Player : DrawableGameComponent
    {
        private readonly ProjectileList _projectiles;
        private SoundEffect _soundEffect;

        public Player(Vector2 centerPosition, ProjectileList projectiles)
            : base(centerPosition)
        {
            Color = Color.LightGreen;
            _projectiles = projectiles;
        }

        public override void LoadContent(ContentManager content)
        {
            _soundEffect = content.Load<SoundEffect>("laser_0");
            Texture = content.Load<Texture2D>("ship (1)");
            SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);
            BoundingSize = new Point { X = Texture.Width, Y = Texture.Height };
        }

        public override void Update(GameTime gameTime, InputState inputState)
        {
            MovementVector = Vector2.Zero;

            float distanceToMove = gameTime.ElapsedGameTime.Milliseconds / 10.0f;

            if (inputState.IsRight(PlayerIndex.One))
            {
                MovementVector.X += 3.0f;
            }
            if (inputState.IsLeft(PlayerIndex.One))
            {
                MovementVector.X -= 3.0f;
            }

            if (inputState.IsSpace(PlayerIndex.One))
            {
                Shoot(_projectiles);
            }

            CenterPosition += MovementVector * distanceToMove;
        }

        private void Shoot(IEnumerable<IDrawableGameComponent> projectiles)
        {
            foreach (IDrawableGameComponent item in projectiles)
            {
                var projectile = (Projectile)item;
                if (!item.IsAlive)
                {
                    _soundEffect.Play();
                    item.IsAlive = true;
                    projectile.SetMovementVector(new Vector2(0.0f, -1.0f));
                    // set centerposition  to just above player ship
                    projectile.SetCenterPosition(new Vector2(CenterPosition.X + 1, CenterPosition.Y - 32));
                    break;
                }
            }
        }
    }
}