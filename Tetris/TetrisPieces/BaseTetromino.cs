﻿using System;
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

        private TimeSpan timeToMoveBeforeBeingCompletelyDisabled = TimeSpan.FromMilliseconds(500);

        private TimeSpan elapsedTimeBeforeBeingCompletelyDisabled = TimeSpan.Zero;

        private bool shouldEnableDisableTimer = false;

        public bool shouldProject = false;

        public int PieceHeight
        {
            get
            {
                int lowest1 = 0;
                for (int i = 0; i < Shape[RotationOption].GetLength(0); i++)
                {
                    for (int j = 0; j < Shape[RotationOption].GetLength(1); j++)
                    {
                        if (Shape[RotationOption][i, j] == 1)
                        {
                            lowest1 = i;
                        }
                    }
                }

                return lowest1;
            }
        }
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



            if (shouldEnableDisableTimer)
            {
                elapsedTimeBeforeBeingCompletelyDisabled += gameTime.ElapsedGameTime;

                if (elapsedTimeBeforeBeingCompletelyDisabled >= timeToMoveBeforeBeingCompletelyDisabled)
                {
                    //recheck to make sure newposition is still valid for disabling the piece
                    if (ShouldDisablePiece())
                    {
                        FillSpot();
                    }
                    else
                    {
                        shouldEnableDisableTimer = false;
                    }
                }
            }

            if (ShouldDisablePiece() && !shouldEnableDisableTimer)
            {
                shouldEnableDisableTimer = true;
            }

            var spots = GetMarkedSpots(RotationOption, Shape);

            if ((InputManager.KeyboardState.IsKeyDown(Keys.Down) && InputManager.OldKeyboardState.IsKeyUp(Keys.Down))
                || (InputManager.KeyboardState.IsKeyDown(Keys.S) && InputManager.OldKeyboardState.IsKeyUp(Keys.S)) && !shouldEnableDisableTimer)
            {
                if (!ShouldDisablePiece())
                {
                    GridPosition.Y += 1;
                    elapsedMoveDownTime = TimeSpan.Zero;
                }
            }
            if ((InputManager.KeyboardState.IsKeyDown(Keys.Left) && InputManager.OldKeyboardState.IsKeyUp(Keys.Left))
                || (InputManager.KeyboardState.IsKeyDown(Keys.A) && InputManager.OldKeyboardState.IsKeyUp(Keys.A)))
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
            if ((InputManager.KeyboardState.IsKeyDown(Keys.Right) && InputManager.OldKeyboardState.IsKeyUp(Keys.Right))
                || (InputManager.KeyboardState.IsKeyDown(Keys.D) && InputManager.OldKeyboardState.IsKeyUp(Keys.D)))
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
                var ogGridPosition = GridPosition;
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
                            if (WillPieceFit(RotationOptions.NintyDegrees, Shape))
                            {
                                RotationOption = RotationOptions.NintyDegrees;
                            }
                            else
                            {
                                GridPosition = ogGridPosition;
                            }
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

                            if (WillPieceFit(RotationOptions.HundredEightyDegrees, Shape))
                            {
                                RotationOption = RotationOptions.HundredEightyDegrees;
                            }
                            else
                            {
                                GridPosition = ogGridPosition;
                            }
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

                            if (WillPieceFit(RotationOptions.TwoHundredSeventyDegrees, Shape))
                            {
                                RotationOption = RotationOptions.TwoHundredSeventyDegrees;
                            }
                            else
                            {
                                GridPosition = ogGridPosition;
                            }
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

                            if (WillPieceFit(RotationOptions.NoRotation, Shape))
                            {
                                RotationOption = RotationOptions.NoRotation;
                            }
                            else
                            {
                                //change everything back 
                                GridPosition = ogGridPosition;
                            }
                        }
                        break;
                }
            }

            elapsedMoveDownTime += gameTime.ElapsedGameTime;
            if (elapsedMoveDownTime >= moveDownTimer && !shouldEnableDisableTimer)
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

            if (ySpot + GridPosition.Y >= GameScreen.grid.GetLength(0))
            {
                var offsetAmount = GameScreen.grid.GetLength(0) - (ySpot + GridPosition.Y) + 1;
                GridPosition.Y -= offsetAmount;
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

            if (farYSpot + GridPosition.Y >= GameScreen.grid.GetLength(0))
            {
                var offsetAmount = GameScreen.grid.GetLength(0) - (farYSpot
                    + GridPosition.Y) + 1;
                GridPosition.Y -= offsetAmount;
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

        public bool ShouldDisablePiece()
        {
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
                        return true;
                    }
                }
            }

            return y >= Game1.GridHeight;
        }

        public bool WillPieceFit(RotationOptions rotationOption, Dictionary<RotationOptions, int[,]> shape)
        {
            var spots = GetMarkedSpots(rotationOption, shape);
            for (int i = 0; i < spots[0].Count; i++)
            {
                if (GridPosition.Y < 0) continue;
                if (GameScreen.grid[spots[1][i] + GridPosition.Y, spots[0][i] + GridPosition.X] == true)
                {
                    return false;
                }
            }

            return true;
        }

        public int LowestGridPosition()
        {
            var currentMarkedSpots = GetMarkedSpots(RotationOption, Shape);
            var lowestPoint = GridPosition.Y + PieceHeight;

            bool didFindLowestGround = false;

            for (int i = 0; i < GameScreen.grid.GetLength(0) - lowestPoint; i++)
            {
                for (int j = 0; j < currentMarkedSpots[0].Count; j++)
                {
                    if (GridPosition.Y < 0) break;

                    if (GameScreen.grid[GridPosition.Y + i + currentMarkedSpots[1][j], GridPosition.X + currentMarkedSpots[0][j]] == true)
                    {
                        didFindLowestGround = true;
                        break;
                    }
                }

                if (didFindLowestGround)
                {
                    return GridPosition.Y + i - 1;
                }
            }

            if (!didFindLowestGround)
            {
                return (GameScreen.grid.GetLength(0) - 1) - PieceHeight;
            }

            //oof it broke
            return -100;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (!shouldProject) return;

            var oldGridPosition = GridPosition;
            GridPosition.Y = LowestGridPosition();
            var newPosition = Position;
            GridPosition = oldGridPosition;

            spriteBatch.Draw(Texture, newPosition, null, Color * 0.3f, MathHelper.ToRadians(Rotation), Origin, Scale, Effects, LayerDepth);

        }
    }
}
