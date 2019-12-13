using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MichaelLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    public class AdvancedShadowButton : ShadowButton
    {
        public Action ClickedAction { get; set; }
        public TimeSpan DelayBeforeRunningAction { get; set; }

        private TimeSpan elapsedTimeSpan;

        private bool startCounting = false;
        public AdvancedShadowButton(Texture2D texture, Vector2 position, Color color, Vector2 scale, Texture2D pixel, Color shadowColor) 
            : base(texture, position, color, scale, pixel, shadowColor)
        {

        }

        public void Update(GameTime gameTime, MouseState mouse)
        {
            if(startCounting)
            {
                elapsedTimeSpan += gameTime.ElapsedGameTime;
            }

            if(elapsedTimeSpan >= DelayBeforeRunningAction)
            {
                ClickedAction();
            }

            base.Update(gameTime, mouse);
            startCounting = IsClicked(mouse);
        }
    }
}
