using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tetris
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public static int GridWidth = 10;
        public static int GridHeight = 20;
        public static int GridCellSize = 35;

        public static Random Random = new Random();

        public static Texture2D Pixel;

        public static List<Color> AllColors = new List<Color>();

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

            var infos = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
            int count = -1;
            foreach(var info in infos)
            {
                count++;
                if (count < 1) continue;
                AllColors.Add((Color)info.GetValue(null));
            }


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
            GraphicsDevice.Clear(Color.LightBlue);

            spriteBatch.Begin();

            ScreenManager.Draw(spriteBatch);
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
