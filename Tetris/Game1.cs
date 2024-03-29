﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection;
using Tetris.Screens;

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

        public static Action GameExitDelegate;

        public static bool isDebugMode = false;
        public Game1(string arg)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            isDebugMode = arg == "true";
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

            GameExitDelegate = new Action(() => Exit());

            ScreenManager.ChangeScreen(ScreenStates.Menu);

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

            ScreenManager.AddScreen(ScreenStates.Menu, new MenuScreen(Content, graphics));
            ScreenManager.AddScreen(ScreenStates.SingePlayerGame, new GameScreen(Content, graphics));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.KeyboardState = Keyboard.GetState();
            InputManager.MouseState = Mouse.GetState();

            ScreenManager.Update(gameTime);

            InputManager.OldKeyboardState = InputManager.KeyboardState;
            InputManager.OldMouseState = InputManager.MouseState;

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
