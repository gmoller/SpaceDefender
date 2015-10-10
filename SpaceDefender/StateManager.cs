using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    public interface IGameState
    {
        string Id { get; }
        Dictionary<string, IDrawableGameComponent> GameComponents { get; set; }
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime, InputState inputState);
        void Draw(SpriteBatch spriteBatch);
    }

    public class StateManager
    {
        private readonly Dictionary<string, IGameState> _stateStore = new Dictionary<string, IGameState>();
        private IGameState _currentState;

        public string CurrentStateId
        {
            get { return _currentState.Id; }
        }

        public void AddState(string stateId, IGameState state)
        {
            _stateStore.Add(stateId, state);
        }

        public void ResetState(string stateId, IGameState newState)
        {
            _stateStore[stateId] = newState;
        }

        public void ChangeState(string stateId)
        {
            _currentState = _stateStore[stateId];
        }

        public void ChangeState(string stateId, Dictionary<string, IDrawableGameComponent> gameComponents)
        {
           _currentState = _stateStore[stateId];
            _currentState.GameComponents = gameComponents;
        }

        public bool Exists(string stateId)
        {
            return _stateStore.ContainsKey(stateId);
        }

        public void LoadContent(ContentManager content)
        {
            _currentState.LoadContent(content);
        }

        public void Update(GameTime gameTime, InputState inputState)
        {
            if (_currentState == null)
            {
                return;
            }

            _currentState.Update(gameTime, inputState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentState == null)
            {
                return;
            }

            _currentState.Draw(spriteBatch);
        }
    }
}