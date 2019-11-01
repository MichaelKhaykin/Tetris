using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class Tetromino : Sprite
    {
        public Vector2 GridPosition { get; set; }

        public int[,] shape;
        public Tetromino(Texture2D texture, Vector2 gridPosition, Color color, Vector2 scale, TetrominoType type, float rotation = 0, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0) 
            : base(texture, new Vector2(-100, -100), color, scale, rotation, effects, layerDepth)
        {
            GridPosition = gridPosition;

            switch (type)
            {
                case TetrominoType.Straight:
                    shape = new int[4, 4]
                    {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;

                case TetrominoType.LeftL:
                    shape = new int[4, 4]
                    {
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 1, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;

                case TetrominoType.RightL:
                    shape = new int[4, 4]
                    {
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 1, 1, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;

                case TetrominoType.Square:
                    shape = new int[4, 4]
                    {
                        { 0, 1, 1, 0 },
                        { 0, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;

                case TetrominoType.RightZigZag:
                    shape = new int[4, 4]
                    {
                        { 0, 1, 1, 0 },
                        { 1, 1, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;

                case TetrominoType.LeftZigZag:
                    shape = new int[4, 4]
                    {
                        { 0, 1, 1, 0 },
                        { 0, 0, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;

                case TetrominoType.SmallT:
                    shape = new int[4, 4]
                    {
                        { 0, 1, 0, 0 },
                        { 1, 1, 1, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    };
                    break;
            }
        }

        public void Update(GameTime gameTime, Vector2 offSet)
        {
            Position = new Vector2(GridPosition.X * Globals.CellSize + offSet.X, GridPosition.Y * Globals.CellSize - Globals.CellSize / 2);
        }
    }
}
