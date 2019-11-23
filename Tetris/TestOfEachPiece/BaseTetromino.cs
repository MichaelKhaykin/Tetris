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
        public TimeSpan moveDownTimer = TimeSpan.FromMilliseconds(500);

        public TimeSpan elapsedMoveDownTime = TimeSpan.Zero;

        private Point gridPosition;
        public ref Point GridPosition { get => ref gridPosition; }
        public RotationOptions RotationOption { get; set; }
        public override float Rotation { get => (int)RotationOption; }
        public Dictionary<RotationOptions, int[,]> Shape { get; set; }
        public bool IsEnabled { get; set; } = true;

        public PieceTypes PieceType { get; set; }

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

        public List<int>[] GetMarkedSpots(RotationOptions rotationOption, Dictionary<RotationOptions, int[,]> shape)
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

        private void FillSpot()
        {
            IsEnabled = false;

            var array = Shape[RotationOption];
            int yLength = array.GetLength(0);
            int xLength = array.GetLength(1);
            for (int w = 0; w < yLength; w++)
            {
                for (int z = 0; z < xLength; z++)
                {
                    if (array[w, z] == 1)
                    {
                        GameScreen.grid[GridPosition.Y + w, GridPosition.X + z] = true;
                    }
                }
            }
        }
        public new virtual void Update(GameTime gameTime)
        {
            if (IsEnabled == false) return;

            var spots = GetMarkedSpots(RotationOption, Shape);

            var ySpots = spots[1];
            var y = ySpots.OrderByDescending(w => w).First() + GridPosition.Y + 1;

            if (GridPosition.Y >= -1)
            {
                for (int i = 0; i < spots[0].Count; i++)
                {
                    if (spots[1][i] + GridPosition.Y + 1 >= GameScreen.grid.GetLength(0)) continue;
                    

                    if (GameScreen.grid[spots[1][i] + GridPosition.Y + 1, spots[0][i] + GridPosition.X] == true)
                    {
                        FillSpot();
                    }
                    
                }
            }
                
            if (y >= Game1.GridHeight) 
            {
                FillSpot();
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.Down) && InputManager.OldKeyboardState.IsKeyUp(Keys.Down))
            {
                GridPosition.Y += 1;
                elapsedMoveDownTime = TimeSpan.Zero;
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Left) && InputManager.OldKeyboardState.IsKeyUp(Keys.Left))
            {
                for (int i = 0; i < spots[0].Count; i++)
                {
                    if (spots[0][i] + GridPosition.X - 1 < 0 || GridPosition.Y < 0) continue;

                    if (GameScreen.grid[spots[1][i] + GridPosition.Y, spots[0][i] + GridPosition.X - 1] == true)
                    {
                        return;
                    }
                }

                var xSpot = CalculateClosestPointOnShape(RotationOption, Shape) - 1;

                if (xSpot >= 0)
                {
                    GridPosition.X -= 1;
                }

            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Right) && InputManager.OldKeyboardState.IsKeyUp(Keys.Right))
            {
                for (int i = 0; i < spots[0].Count; i++)
                {
                    if (spots[0][i] + GridPosition.X + 1 >= GameScreen.grid.GetLength(1) || GridPosition.Y < 0) continue;

                    if (GameScreen.grid[spots[1][i] + GridPosition.Y, spots[0][i] + GridPosition.X + 1] == true)
                    {
                        return;
                    }
                }

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
                            else
                            {
                                Idk(xSpot + 1, RotationOptions.NintyDegrees);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.NintyDegrees, Shape);
                            if (farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            else
                            {
                                Idk2(farSpot - 1, RotationOptions.NintyDegrees);
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
                            else
                            {
                                Idk(xSpot + 1, RotationOptions.HundredEightyDegrees);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.HundredEightyDegrees, Shape);
                            if (farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            else
                            {
                                Idk2(farSpot - 1, RotationOptions.HundredEightyDegrees);
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
                            else
                            {
                                Idk(xSpot + 1, RotationOptions.TwoHundredSeventyDegrees);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.TwoHundredSeventyDegrees, Shape);
                            if (farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            else
                            {
                                Idk2(farSpot - 1, RotationOptions.TwoHundredSeventyDegrees);
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
                            else
                            {
                                Idk(xSpot + 1, RotationOptions.NoRotation);
                            }
                            var farSpot = CalculateFurthestPointOnShape(RotationOptions.NoRotation, Shape);
                            if (farSpot > Game1.GridWidth)
                            {
                                GridPosition.X -= (farSpot - Game1.GridWidth);
                            }
                            else
                            {
                                Idk2(farSpot - 1, RotationOptions.NoRotation);
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

        private void Idk(int xSpot, RotationOptions rotOption)
        {
            if (GridPosition.Y <= 0) return;

            var ySpot = 0;
            for (int i = 0; i < Shape[rotOption].GetLength(0); i++)
            {
                for (int x = 0; x < Shape[rotOption].GetLength(1); x++)
                {
                    if (x == xSpot)
                    {
                        ySpot = i;
                    }
                }
            }

            if (GameScreen.grid[ySpot + GridPosition.Y, xSpot] == true)
            {
                if (GameScreen.grid[ySpot + GridPosition.Y, xSpot + 1] == true)
                {
                    GridPosition.X += 2;
                }
                else
                {
                    GridPosition.X += 1;
                }
            }
        }

        private void Idk2(int farSpot, RotationOptions rotOption)
        {
            if (GridPosition.Y <= 0) return;
            var farYSpot = 0;
            for (int i = 0; i < Shape[rotOption].GetLength(0); i++)
            {
                for (int x = 0; x < Shape[rotOption].GetLength(1); x++)
                {
                    if (x == farSpot)
                    {
                        farYSpot = i;
                    }
                }
            }

            if (GameScreen.grid[farYSpot + GridPosition.Y, farSpot] == true)
            {
                if (GameScreen.grid[farYSpot + GridPosition.Y, farSpot - 1] == true)
                {
                    GridPosition.X -= 2;
                }
                else
                {
                    GridPosition.X -= 1;
                }
            }
        }
    }
}
