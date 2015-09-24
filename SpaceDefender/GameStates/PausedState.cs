using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.GameComponents;

namespace SpaceDefender.GameStates
{
    internal class PausedState : IGameState
    {
        private readonly StateManager _gameStateManager;
        private Dictionary<string, IDrawableGameComponent> _gameComponents = new Dictionary<string, IDrawableGameComponent>();
        private readonly IDrawableGameComponent _pausedMessage;

        public PausedState(StateManager gameStateManager, int viewportWidth, int viewportHeight)
        {
            _gameStateManager = gameStateManager;
            _pausedMessage = new PausedDisplay(viewportWidth, viewportHeight);
        }

        public string Id
        {
            get { return "Paused"; }
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
            _pausedMessage.LoadContent(content);
        }

        public void Update(GameTime gameTime, InputState inputState)
        {
            if (inputState.IsPause(PlayerIndex.One))
            {
                _gameStateManager.ChangeState("Playing", _gameComponents);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<string, IDrawableGameComponent> item in _gameComponents)
            {
                item.Value.Draw(spriteBatch);
                _pausedMessage.Draw(spriteBatch);
            }
        }
    }
}