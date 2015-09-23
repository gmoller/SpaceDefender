using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.GameComponents;

namespace SpaceDefender
{
    internal class PlayingState : IGameState
    {
        private readonly StateManager _gameStateManager;
        private Dictionary<string, IDrawableGameComponent> _gameComponents = new Dictionary<string, IDrawableGameComponent>();

        internal PlayingState(StateManager gameStateManager, int viewportWidth, int viewportHeight)
        {
            _gameStateManager = gameStateManager;

            IDrawableGameComponent backdrop = new Backdrop(viewportWidth, viewportHeight);
            _gameComponents.Add("backdrop", backdrop);

            var projectiles = new ProjectileList(Vector2.Zero, viewportWidth, viewportHeight);
            _gameComponents.Add("projectiles", projectiles);

            IDrawableGameComponent player = new Player(new Vector2(viewportWidth / 2.0f, viewportHeight - 50.0f), viewportWidth, viewportHeight, projectiles);
            _gameComponents.Add("player", player);

            IDrawableGameComponent alien1 = new Alien(new Vector2(viewportWidth / 2.0f, viewportHeight / 2.0f), viewportWidth, viewportHeight);
            _gameComponents.Add("alien1", alien1);

            IDrawableGameComponent alien2 = new Alien2(new Vector2(100.0f, 100.0f), viewportWidth, viewportHeight);
            _gameComponents.Add("alien2", alien2);

            IDrawableGameComponent hud = new Hud(viewportWidth, viewportHeight) { Score = 0, Lives = 3 };
            _gameComponents.Add("hud", hud);

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
    }
}