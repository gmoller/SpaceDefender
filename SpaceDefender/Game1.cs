using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefender
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //private Primitives2D _primitives2D;

        private readonly List<IDrawableGameComponent> _gameObjects = new List<IDrawableGameComponent>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1024,
                    PreferredBackBufferHeight = 768
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
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //_primitives2D = new Primitives2D(GraphicsDevice, _spriteBatch);
            Primitives2D.Initialize(GraphicsDevice, _spriteBatch);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            IDrawableGameComponent backdrop = new Backdrop(Content.Load<Texture2D>("space"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _gameObjects.Add(backdrop);

            IDrawableGameComponent player = new Player(Content.Load<Texture2D>("ship (1)"), new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height - 50.0f), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _gameObjects.Add(player);

            IDrawableGameComponent alien1 = new Alien(Content.Load<Texture2D>("ship (3)"), new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height / 2.0f), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _gameObjects.Add(alien1);

            IDrawableGameComponent alien2 = new Alien2(Content.Load<Texture2D>("ship (4)"), new Vector2(100.0f, 100.0f), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _gameObjects.Add(alien2);

            IDrawableGameComponent hud = new Hud(Content.Load<SpriteFont>("arial-32"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) { Score = 0, Lives = 3 };
            _gameObjects.Add(hud);
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
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            foreach (IDrawableGameComponent item in _gameObjects)
            {
                item.Update(gameTime, keyboardState);
            }

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

            foreach (IDrawableGameComponent item in _gameObjects)
            {
                item.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}