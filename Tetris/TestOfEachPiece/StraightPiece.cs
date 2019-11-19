using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.TestOfEachPiece
{
    public class StraightPiece : BaseTetromino
    {
        public override Vector2 Position 
        {
            get
            {
                //calculate actual position based off of grid position and rotation

                Vector2 position = new Vector2(GridPosition.X * 35 + GameScreen.offSet.X, GridPosition.Y * 35 + GameScreen.offSet.Y);

                switch(RotationOption)
                {
                    case RotationOptions.NoRotation:
                        position += new Vector2(Texture.Width / 2, Texture.Height / 2 + 35);
                        break;

                    case RotationOptions.HundredEightyDegrees:
                        position += new Vector2(Texture.Width / 2, Texture.Height / 2 + 70);
                        break;

                    case RotationOptions.NintyDegrees:
                        position += new Vector2(Texture.Height / 2 + 35, Texture.Width / 2);
                        break;
                            
                    case RotationOptions.TwoHundredSeventyDegrees:
                        position += new Vector2(Texture.Height / 2 + 70, Texture.Width / 2);
                        break;
                }

                return position;
            } 
        }
        public StraightPiece(Texture2D texture, Point gridPosition, Color color, Vector2 scale, RotationOptions rotationOption) 
            : base(texture, gridPosition, color, scale, rotationOption)
        {
            GridPosition = gridPosition;

            Shape = new Dictionary<RotationOptions, int[,]>()
            {
                [RotationOptions.NoRotation] = new int[,]
                {
                    { 0, 0, 0, 0 },
                    { 1, 1, 1, 1 },
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 }
                },

                [RotationOptions.NintyDegrees] = new int[,]
                {
                    { 0, 1, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 1, 0, 0 }
                },

                [RotationOptions.HundredEightyDegrees] = new int[,]
                {
                    { 0, 0, 0, 0 },
                    { 0, 0, 0, 0 },
                    { 1, 1, 1, 1 },
                    { 0, 0, 0, 0 }
                },

                [RotationOptions.TwoHundredSeventyDegrees] = new int[,]
                {
                    { 0, 0, 1, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 1, 0 }
                },
            };
        }
    }
}
