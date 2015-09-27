using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceDefender.GameStates;

namespace SpaceDefender
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    internal class GameRoot : Game
    {
        internal static GameRoot Instance { get; set; }
        //private static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        //internal static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        internal static Vector2 ScreenSize { get { return new Vector2(Instance._graphics.PreferredBackBufferWidth, Instance._graphics.PreferredBackBufferHeight); } }

        internal static bool ShowBounds;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputState _inputState;

        private readonly StateManager _gameStateManager = new StateManager();

        private Effect _effect;

        public GameRoot()
        {
            Instance = this;

            _graphics = new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1600, // 800, 1024, 1600, 1680, 1920
                    PreferredBackBufferHeight = 900 // 600, 768, 900, 1050, 1080
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

            //_effect = Content.Load<Effect>("myEffect");
            //_effect.Parameters["Contrast"].SetValue(0.0f);
            //_effect.Parameters["Brightness"].SetValue(0.0f);
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
            try
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
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                //_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, effect: _effect);
                _spriteBatch.Begin();

                _gameStateManager.Draw(_spriteBatch);

                _spriteBatch.End();

                base.Draw(gameTime);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        private void MakeFullScreen()
        {
            Window.Position = new Point(0, 0);
            //Window.IsBorderless = true;

            // make full-screen and set resolution to monitors resolution
            //_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();
        }

        private void SetupGameStates(StateManager gameStateManager)
        {
            //gameStateManager.AddState("Menu", new MenuState(textureManager, _font, gameState, this));
            //gameStateManager.AddState("HighScores", new HighScoresState(_font, gameState));
            gameStateManager.AddState("Playing", new PlayingState(gameStateManager));
            //gameStateManager.AddState("LevelCleared", new LevelClearedState(_font, gameState));
            gameStateManager.AddState("Paused", new PausedState(gameStateManager));
            //gameStateManager.AddState("GameOver", new GameOverState(textureManager, _font, gameState));
            //gameStateManager.AddState("Quit", new QuitState(_font, gameState));
        }

        private void LogError(Exception ex)
        {
            using (var file = new StreamWriter("debug.txt"))
            {
                file.WriteLine("{0} - {1}", DateTime.Now.ToString("s"), ex.Message);
                file.WriteLine("CallStack: {0}", ex.StackTrace);
            }
        }
    }
}