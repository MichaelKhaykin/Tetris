using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.TestOfEachPiece
{
    public class Square : BaseTetromino
    {
        public override Vector2 Position
        {
            get
            {
                //calculate actual position based off of grid position and rotation
                Vector2 position = new Vector2(GridPosition.X * Game1.GridCellSize + GameScreen.offSet.X, GridPosition.Y * Game1.GridCellSize + GameScreen.offSet.Y);
                position += new Vector2(Texture.Height / 2, Texture.Width / 2);
                return position;
            }
        }
        public Square(Texture2D texture, Point gridPosition, Color color, Vector2 scale, RotationOptions rotationOption) : base(texture, gridPosition, color, scale, rotationOption)
        {
            Shape = new Dictionary<RotationOptions, int[,]>()
            {
                [RotationOption = RotationOptions.NoRotation] = new int[,]
                {
                    { 1, 1 },
                    { 1, 1 },
                },
                [RotationOption = RotationOptions.HundredEightyDegrees] = new int[,]
                {
                    { 1, 1 },
                    { 1, 1 },
                },
                [RotationOption = RotationOptions.NintyDegrees] = new int[,]
                {
                    { 1, 1 },
                    { 1, 1 },
                },
                [RotationOption = RotationOptions.TwoHundredSeventyDegrees] = new int[,]
                {
                    { 1, 1 },
                    { 1, 1 },
                },
            };
        }
    }
}
