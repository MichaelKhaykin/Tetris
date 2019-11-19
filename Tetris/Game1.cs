using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public static int GridWidth = 10;
        public static int GridHeight = 20;
        public static int GridCellSize = 35;

        public static Texture2D Pixel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
      
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 701;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenManager.CurrentScreen = ScreenStates.Game;

            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            ScreenManager.Screens.Add(ScreenStates.Game, new GameScreen(Content, graphics));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.KeyboardState = Keyboard.GetState();

            ScreenManager.Update(gameTime);

            InputManager.OldKeyboardState = InputManager.KeyboardState;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            ScreenManager.Draw(spriteBatch);
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
