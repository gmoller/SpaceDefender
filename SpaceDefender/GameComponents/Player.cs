using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender.GameComponents
{
    public class Player : GameLibrary.MyDrawableGameComponent
    {
        private readonly ProjectileList _projectiles;
        private SoundEffect _soundEffect;

        public Player(Vector2 centerPosition, ProjectileList projectiles)
            : base(centerPosition)
        {
            _projectiles = projectiles;
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = new Sprite(new TextureAtlas(content.Load<Texture2D>("Ship"), new[] { new Rectangle(987, 0, 219, 243) }), Color.LightGreen)
                {
                    Scale = new Vector2(0.2f, 0.2f)*new Vector2(GameRoot.ScreenSize.X/1280.0f, GameRoot.ScreenSize.Y/720.0f),
                    OriginNormalized = new Vector2(0.5f, 0.5f)
                };
            BoundingSize = new Point { X = Sprite.TextureAtlas.SingleTextureWidth, Y = Sprite.TextureAtlas.SingleTextureHeight };

            _soundEffect = content.Load<SoundEffect>("laser_0");
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