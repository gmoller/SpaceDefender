using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        internal static bool ShowBounds;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputState _inputState;

        private readonly StateManager _gameStateManager = new StateManager();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1920, // 1024
                    PreferredBackBufferHeight = 1080 // 768
                };
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            MakeFullScreen();

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Primitives2D.Initialize(GraphicsDevice);
            _inputState = new InputState();

            SetupGameStates(_gameStateManager);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _gameStateManager.ChangeState("Paused");
            _gameStateManager.LoadContent(Content);
            _gameStateManager.ChangeState("Playing");
            _gameStateManager.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _inputState.Update();
            if (_inputState.IsExitGame(PlayerIndex.One))
            {
                Exit();
            }
            if (_inputState.IsF1(PlayerIndex.One))
            {
                ShowBounds = !ShowBounds;
            }

            _gameStateManager.Update(gameTime, _inputState);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _gameStateManager.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void MakeFullScreen()
        {
            //Window.Position = new Point(0, 0);
            //Window.IsBorderless = true;

            // make full-screen and set resolution to monitors resolution
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        private void SetupGameStates(StateManager gameStateManager)
        {
            //gameStateManager.AddState("Menu", new MenuState(textureManager, _font, gameState, this));
            //gameStateManager.AddState("HighScores", new HighScoresState(_font, gameState));
            gameStateManager.AddState("Playing", new PlayingState(gameStateManager, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            //gameStateManager.AddState("LevelCleared", new LevelClearedState(_font, gameState));
            gameStateManager.AddState("Paused", new PausedState(gameStateManager, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            //gameStateManager.AddState("GameOver", new GameOverState(textureManager, _font, gameState));
            //gameStateManager.AddState("Quit", new QuitState(_font, gameState));
        }
    }
}