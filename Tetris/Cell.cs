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

        public override Vector2 Position
        {
            get
            {
                return new Vector2(GridPosition.X * Game1.GridCellSize + GameScreen.offSet.X + Game1.GridCellSize / 2, GridPosition.Y * Game1.GridCellSize + GameScreen.offSet.Y + Game1.GridCellSize / 2);
            }
        }
        public Cell(Texture2D texture, Point gridPosition, Color color, Vector2 scale, float rotation = 0, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
            : base(texture, new Vector2(0, 0), color, scale, rotation, effects, layerDepth)
        {
            GridPosition = gridPosition;
        }
    }
}
