using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.TestOfEachPiece
{
    public class RightL : BaseTetromino
    {
        public override Vector2 Position 
        { 
            get
            {
                Vector2 position = new Vector2(GridPosition.X * Game1.GridCellSize + GameScreen.offSet.X, GridPosition.Y * Game1.GridCellSize + GameScreen.offSet.Y);

                switch (RotationOption)
                {
                    case RotationOptions.NoRotation:
                        position += new Vector2(Game1.GridCellSize, Texture.Height / 2);
                        break;
                    case RotationOptions.NintyDegrees:
                        position += new Vector2(Game1.GridCellSize * 1 / 2 + Game1.GridCellSize, Game1.GridCellSize);
                        break;
                    case RotationOptions.HundredEightyDegrees:
                        position += new Vector2(Game1.GridCellSize, Game1.GridCellSize / 2 + Game1.GridCellSize);
                        break;
                    case RotationOptions.TwoHundredSeventyDegrees:
                        position += new Vector2(Game1.GridCellSize * 3 / 2, Game1.GridCellSize);
                        break;
                }

                return position;
            }
        }
        public RightL(Texture2D texture, Point gridPosition, Color color, Vector2 scale, RotationOptions rotationOption) 
            : base(texture, gridPosition, color, scale, rotationOption)
        {
            Shape = new Dictionary<RotationOptions, int[,]>()
            {
                [RotationOptions.NoRotation] = new int[,]
                {
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                    { 1, 1, 0 },
                },
                [RotationOptions.NintyDegrees] = new int[,]
                {
                    { 1, 0, 0 },
                    { 1, 1, 1 },
                    { 0, 0, 0 },
                },
                [RotationOptions.HundredEightyDegrees] = new int[,]
                {
                    { 0, 1, 1  },
                    { 0, 1, 0  },
                    { 0, 1, 0  },
                },
                [RotationOptions.TwoHundredSeventyDegrees] = new int[,]
                {
                    { 1, 1, 1  },
                    { 0, 0, 1  },
                    { 0, 0, 0  }
                }
            };
        }
    }
}
