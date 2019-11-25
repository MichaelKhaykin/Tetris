using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Cell : Sprite
    {

        private Point gridPosition;
        public ref Point GridPosition => ref gridPosition;

        public bool isFadingOut { get; set; } = false;

        public Color OgColor { get; set; }

        public float TravelPercentage { get; set; } = 0;
        public override Vector2 Position
        {
            get
            {
                return new Vector2(GridPosition.X * Game1.GridCellSize + GameScreen.offSet.X + Game1.GridCellSize / 2, GridPosition.Y * Game1.GridCellSize + GameScreen.offSet.Y + Game1.GridCellSize / 2);
            }
        }
        public Cell(Texture2D texture, Point gridPosition, Color color, Vector2 scale, float rotation = 0, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
            : base(texture, Vector2.Zero, color, scale, rotation, effects, layerDepth)
        {
            GridPosition = gridPosition;
            OgColor = color;
        }

        public override void Update(GameTime gameTime)
        {
            if(isFadingOut)
            {
                Color = Color.Lerp(OgColor, Color.Transparent, TravelPercentage);
                TravelPercentage += 0.05f;
            }
        }
    }
}
