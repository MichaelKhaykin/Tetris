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
        public static Dictionary<ScreenStates, Screen> Screens = new Dictionary<ScreenStates, Screen>();
        public static Stack<ScreenStates> PreviousScreens;
        public static ScreenStates CurrentScreen;

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
