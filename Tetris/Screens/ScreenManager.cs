using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class ScreenManager
    {
        private static Dictionary<ScreenStates, Screen> Screens = new Dictionary<ScreenStates, Screen>();
        private static Stack<ScreenStates> PreviousScreens = new Stack<ScreenStates>();
        private static ScreenStates CurrentScreen = ScreenStates.None;

        public static void AddScreen(ScreenStates screenState, Screen screen)
        {
            Screens.Add(screenState, screen);
        }

        public static void ChangeScreen(ScreenStates changeTo)
        {
            if (CurrentScreen != ScreenStates.None)
            {
                PreviousScreens.Push(CurrentScreen);
            }
            CurrentScreen = changeTo;
        }
        public static void Update(GameTime gameTime)
        {
            Screens[CurrentScreen].Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Screens[CurrentScreen].Draw(spriteBatch);
        }
    }
}
