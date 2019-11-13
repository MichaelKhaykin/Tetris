using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Sprite : GameObject, IGameObject
    {
        public Texture2D Texture { get; set; }
        public int ScaledWidth => Texture.Width * (int)Scale.X;
        public int ScaledHeight => Texture.Height * (int)Scale.Y;

        public bool IsDebug = false;
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)(Position.X - Origin.X * Scale.X), (int)(Position.Y - Origin.Y * Scale.Y), ScaledWidth, ScaledHeight);
            }
        }
        public Sprite(Texture2D texture, Vector2 position, Color color, Vector2 scale, float rotation = 0f, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
            : base(position, color, rotation, scale, new Vector2(texture.Width / 2, texture.Height / 2), effects, layerDepth)
        {
            Texture = texture;
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, MathHelper.ToRadians(Rotation), Origin, Scale, Effects, LayerDepth);

            if (IsDebug)
            {
                spriteBatch.Draw(Game1.Pixel, HitBox, Color.Red * 0.5f);
            }
        }
    }
}
