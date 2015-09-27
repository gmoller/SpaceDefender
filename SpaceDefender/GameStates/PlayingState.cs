using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.GameComponents;

namespace SpaceDefender.GameStates
{
    internal class PlayingState : IGameState
    {
        private readonly StateManager _gameStateManager;
        private Dictionary<string, IDrawableGameComponent> _gameComponents = new Dictionary<string, IDrawableGameComponent>();
        private SoundEffect _soundEffect;
        private readonly Random _random;

        internal PlayingState(StateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;

            IDrawableGameComponent backdrop1 = new Backdrop(new Vector2(GameRoot.ScreenSize.X / 2.0f, GameRoot.ScreenSize.Y / 2.0f));
            _gameComponents.Add("backdrop1", backdrop1);

            //IDrawableGameComponent backdrop2 = new Backdrop(new Vector2(GameRoot.ScreenSize.X / 2.0f, (GameRoot.ScreenSize.Y / 2.0f) + GameRoot.ScreenSize.Y));
            //_gameComponents.Add("backdrop2", backdrop2);

            var projectiles = new ProjectileList(Vector2.Zero);
            _gameComponents.Add("projectiles", projectiles);

            IDrawableGameComponent player = new Player(new Vector2(GameRoot.ScreenSize.X / 2.0f, GameRoot.ScreenSize.Y - 50.0f), projectiles);
            _gameComponents.Add("player", player);

            IDrawableGameComponent alien1 = new Alien(new Vector2(GameRoot.ScreenSize.X / 2.0f, GameRoot.ScreenSize.Y / 2.0f));
            _gameComponents.Add("alien1", alien1);

            IDrawableGameComponent alien2 = new Alien2(new Vector2(100.0f, 100.0f));
            _gameComponents.Add("alien2", alien2);

            IDrawableGameComponent explosion = new AnimatedSprite(Vector2.Zero);
            explosion.IsAlive = false;
            _gameComponents.Add("explosion", explosion);

            IDrawableGameComponent hud = new Hud { Score = 0, Lives = 3 };
            _gameComponents.Add("hud", hud);

            _random = new Random();
        }

        public string Id
        {
            get { return "Playing"; }
        }

        public Dictionary<string, IDrawableGameComponent> GameComponents
        {
            get
            {
                return _gameComponents;
            }
            set
            {
                _gameComponents = value;
            }
        }

        public void LoadContent(ContentManager content)
        {
            _soundEffect = content.Load<SoundEffect>("Explosion 2");

            foreach (KeyValuePair<string, IDrawableGameComponent> item in _gameComponents)
            {
                item.Value.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime, InputState inputState)
        {
            if (inputState.IsPause(PlayerIndex.One))
            {
                _gameStateManager.ChangeState("Paused", _gameComponents);

                return;
            }

            CheckForCollisions();

            foreach (KeyValuePair<string, IDrawableGameComponent> item in _gameComponents)
            {
                item.Value.Update(gameTime, inputState);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<string, IDrawableGameComponent> item in _gameComponents)
            {
                item.Value.Draw(spriteBatch);
            }
        }

        private void CheckForCollisions()
        {
            var projectiles = (ProjectileList)_gameComponents["projectiles"];
            foreach (IDrawableGameComponent projectile in projectiles)
            {
                if (projectile.IsAlive)
                {
                    IDrawableGameComponent alien1 = _gameComponents["alien1"];
                    if (alien1.IsAlive)
                    {
                        if (projectile.BoundingCircle.CollidesWith(alien1.BoundingCircle))
                        {
                            IncrementScore();
                            AlienExplosion(projectile, alien1);
                            CreateNewAlien("alien1");
                        }
                    }

                    IDrawableGameComponent alien2 = _gameComponents["alien2"];
                    if (alien2.IsAlive)
                    {
                        if (projectile.BoundingCircle.CollidesWith(alien2.BoundingCircle))
                        {
                            IncrementScore();
                            AlienExplosion(projectile, alien2);
                            CreateNewAlien("alien2");
                        }
                    }
                }
            }
        }

        private void IncrementScore()
        {
            // increment score
            var hud = (Hud)_gameComponents["hud"];
            hud.Score++;
        }

        private void AlienExplosion(IDrawableGameComponent projectile, IDrawableGameComponent alien)
        {
            _soundEffect.Play();
            projectile.IsAlive = false;

            IDrawableGameComponent explosion = _gameComponents["explosion"];
            explosion.CenterPosition = alien.CenterPosition;
            explosion.IsAlive = true;

            alien.IsAlive = false;
        }

        private void CreateNewAlien(string alien1)
        {
            IDrawableGameComponent alien = _gameComponents[alien1];
            alien.IsAlive = true;
            alien.CenterPosition = new Vector2(_random.Next(50, (int)GameRoot.ScreenSize.X - 50), _random.Next(50, (int)GameRoot.ScreenSize.Y - 100));
        }
    }
}