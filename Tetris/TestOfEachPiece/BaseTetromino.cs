using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris.TestOfEachPiece
{
    public abstract class BaseTetromino : Sprite
    {
        public TimeSpan moveDownTimer = TimeSpan.FromMilliseconds(750);

        public TimeSpan elapsedMoveDownTime = TimeSpan.Zero;

        private Point gridPosition;
        public ref Point GridPosition { get => ref gridPosition; }
        public RotationOptions RotationOption { get; set; }
        public override float Rotation { get => (int)RotationOption; }
        public Dictionary<RotationOptions, int[,]> Shape { get; set; }
        public bool IsEnabled { get; set; } = true;

        public BaseTetromino(Texture2D texture, Point gridPosition, Color color, Vector2 scale, RotationOptions rotationOption)
            : base(texture, Vector2.Zero, color, scale, (int)rotationOption, SpriteEffects.None, 0)
        {
            GridPosition = gridPosition;
            RotationOption = rotationOption;
        }

        private int CalculateFurthestPointOnShape(RotationOptions rotationOption, Dictionary<RotationOptions, int[,]> shape)
        {
            var x = GetMarkedSpots(rotationOption, shape)[0].OrderByDescending(w => w).First();
            return GridPosition.X + x + 1;
        }

        private int CalculateClosestPointOnShape(RotationOptions rotationOption, Dictionary<RotationOptions, int[,]> shape)
        {
            var x = GetMarkedSpots(rotationOption, shape)[0].OrderByDescending(w => w).Last();
            return GridPosition.X + x;
        }

        private List<int>[] GetMarkedSpots(RotationOptions rotationOption, Dictionary<RotationOptions, int[,]> shape)
        {
            var array = shape[rotationOption];
            List<int> ySpots = new List<int>();
            List<int> xSpots = new List<int>();
            for (int z = 0; z < array.GetLength(0); z++)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    if (array[z, x] == 1)
                    {
                        ySpots.Add(z);
                        xSpots.Add(x);
                        //mark this
                    }
                }
            }

            return new List<int>[] { xSpots, ySpots };
        }
        public new virtual void Update(GameTime gameTime)
        {
            if (IsEnabled == false) return;

            var ySpots = GetMarkedSpots(RotationOption, Shape)[1];

            var y = ySpots.OrderByDescending(w => w).First() + GridPosition.Y + 1;
            if (y >= Game1.GridHeight) // also disable tile if it is going on a tile that is also filled
            {
                IsEnabled = false;

                var array = Shape[RotationOption];
                int yLength = array.GetLength(0);
                int xLength = array.GetLength(1);
                for (int w = 0; w < yLength; w++)
                {
                    for(int z = 0; z < xLength; z++)
                    {
                        if(array[w, z] == 1)
                        {
                            GameScreen.grid[GridPosition.Y + w, GridPosition.X + z] = true;
                        }
                    }
                }
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.Down) && InputManager.OldKeyboardState.IsKeyUp(Keys.Down))
            {
                GridPosition.Y += 1;
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Left) && InputManager.OldKeyboardState.IsKeyUp(Keys.Left))
            {
                var xSpot = CalculateClosestPointOnShape(RotationOption, Shape) - 1;

                if (xSpot >= 0)
                {
                    GridPosition.X -= 1;
                }

            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Right) && InputManager.OldKeyboardState.IsKeyUp(Keys.Right))
            {
                var xSpot = CalculateFurthestPointOnShape(RotationOption, Shape) + 1;
                if (xSpot <= Game1.GridWidth)
                {
                    GridPosition.X += 1;
                }
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Up) && InputManager.OldKeyboardState.IsKeyUp(Keys.Up))
            {
                switch (RotationOption)
                {
                    case RotationOptions.NoRotation:
                        {
                            var xSpot = CalculateClosestPointOnShape(RotationOptions.NintyDegrees, Shape);
                            if (xSpot < 0)
                            {
                                GridPosition.X += Math.Abs(xSpot);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.NintyDegrees, Shape);
                            if (farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            RotationOption = RotationOptions.NintyDegrees;
                        }
                        break;
                    case RotationOptions.NintyDegrees:
                        {
                            var xSpot = CalculateClosestPointOnShape(RotationOptions.HundredEightyDegrees, Shape);
                            if (xSpot < 0)
                            {
                                GridPosition.X += Math.Abs(xSpot);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.HundredEightyDegrees, Shape);
                            if(farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            RotationOption = RotationOptions.HundredEightyDegrees;
                        }
                        break;
                    case RotationOptions.HundredEightyDegrees:
                        {
                            var xSpot = CalculateClosestPointOnShape(RotationOptions.TwoHundredSeventyDegrees, Shape);
                            if (xSpot < 0)
                            {
                                GridPosition.X += Math.Abs(xSpot);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.TwoHundredSeventyDegrees, Shape);
                            if (farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            RotationOption = RotationOptions.TwoHundredSeventyDegrees;
                        }
                        break;
                    case RotationOptions.TwoHundredSeventyDegrees:
                        {
                            var xSpot = CalculateClosestPointOnShape(RotationOptions.NoRotation, Shape);
                            if (xSpot < 0)
                            {
                                GridPosition.X += Math.Abs(xSpot);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.NoRotation, Shape);
                            if (farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            RotationOption = RotationOptions.NoRotation;
                        }
                        break;
                }
            }

            elapsedMoveDownTime += gameTime.ElapsedGameTime;
            if (elapsedMoveDownTime >= moveDownTimer)
            {
                GridPosition.Y += 1;
                elapsedMoveDownTime = TimeSpan.Zero;
            }
        }
    }
}
