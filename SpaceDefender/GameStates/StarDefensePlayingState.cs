using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.GameComponents;

namespace SpaceDefender.GameStates
{
    public class StarDefensePlayingState : IGameState
    {
        private readonly StateManager _gameStateManager;
        private Dictionary<string, IDrawableGameComponent> _gameComponents = new Dictionary<string, IDrawableGameComponent>();

        public StarDefensePlayingState(StateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;

            IDrawableGameComponent playerShip = new PlayerShip(new Vector2(GameRoot.ScreenSize.X / 2.0f, GameRoot.ScreenSize.Y / 2.0f));
            //IDrawableGameComponent playerShip = new PlayerShip(new Vector2(0.0f, 0.0f));
            IDrawableGameComponent background = new Background(new Vector2(GameRoot.ScreenSize.X / 2.0f, GameRoot.ScreenSize.Y / 2.0f), (PlayerShip)playerShip);

            _gameComponents.Add("background", background);
            _gameComponents.Add("playerShip", playerShip);
        }

        public string Id
        {
            get { return "StarDefensePlaying"; }
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