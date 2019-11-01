using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class TextLabel : GameObject, IGameObject
    {
        public string Text { get; private set; }
        public SpriteFont SpriteFont { get; private set; }
        public TextLabel(string text, SpriteFont font, Vector2 position, Color color, float rotation, Vector2 scale, Vector2 origin, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0) 
            : base(position, color, rotation, scale, origin, effects, layerDepth)
        {
            Text = text;
            SpriteFont = font;
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }
    }
}
