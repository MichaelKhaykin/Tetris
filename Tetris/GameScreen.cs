using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tetris.TestOfEachPiece;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System;

namespace Tetris
{
    public class GameScreen : Screen
    {
        public static bool[,] grid = new bool[Game1.GridHeight, Game1.GridWidth];
        public static Vector2 offSet;

        List<BaseTetromino> nextTiles = new List<BaseTetromino>();

        List<Cell> boomers = new List<Cell>();

        BaseTetromino current;

        BaseTetromino holdPiece = null;

        public bool isPaused = false;

        int highestRowAffected = 0;
        int amountOfRowsToDrop = 0;

        int amountOfFades = 0;
        public GameScreen(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            offSet = new Vector2((Graphics.GraphicsDevice.Viewport.Width - grid.GetLength(1) * Game1.GridCellSize) / 2, 0);

            for (int i = 0; i < 3; i++)
            {
                var tile = GeneratePiece(i);
                while(tile.PieceType != PieceTypes.Straight)
                {
                    tile = GeneratePiece(i);
                }
                nextTiles.Add(tile);
            }

            current = GeneratePiece(0);
            current.GridPosition = new Point(4, -3);
        }

        private BaseTetromino GeneratePiece(int i)
        {
            var pieceTypeToCreate = (PieceTypes)Game1.Random.Next(0, 7);
            var startPoint = new Point(-4, i * 4);
            BaseTetromino newPiece = null;

            switch (pieceTypeToCreate)
            {
                case PieceTypes.LL:
                    newPiece = new LeftL(Content.Load<Texture2D>("LeftL"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.RL:
                    newPiece = new RightL(Content.Load<Texture2D>("RightL"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.T:
                    newPiece = new TPiece(Content.Load<Texture2D>("TPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.LZZ:
                    newPiece = new LeftZigZag(Content.Load<Texture2D>("LeftZigZag"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.RZZ:
                    newPiece = new RightZigZag(Content.Load<Texture2D>("RightZigZag"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.Square:
                    newPiece = new Square(Content.Load<Texture2D>("Square"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.Straight:
                    newPiece = new StraightPiece(Content.Load<Texture2D>("StraightPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                    break;
            }

            return newPiece;
        }
        public override void Update(GameTime gameTime)
        {
            /* This is debug to see that the grid matches with the visualization */
            if (InputManager.KeyboardState.IsKeyDown(Keys.P))
            {
                isPaused = !isPaused;
                string content = "";
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] == true)
                        {
                            content += "1, ";
                        }
                        else
                        {
                            content += "0, ";
                        }
                    }
                    content += '\n';
                }
                File.WriteAllText("gridRepresentation.txt", content);
            }

            if (isPaused == true) return;

            if (InputManager.KeyboardState.IsKeyDown(Keys.H) && InputManager.OldKeyboardState.IsKeyUp(Keys.H))
            {
                if (holdPiece == null)
                {
                    holdPiece = current;
                    holdPiece.GridPosition = new Point(11, 0);


                    current = nextTiles[0];
                    current.GridPosition = new Point(4, -3);

                    nextTiles.RemoveAt(0);

                    foreach (var tile in nextTiles)
                    {
                        tile.GridPosition.Y -= 4;
                    }

                    nextTiles.Add(GeneratePiece(2));
                }
                else
                {
                    var temp = current;

                    current = holdPiece;
                    current.GridPosition = new Point(MathHelper.Clamp(temp.GridPosition.X, 0, grid.GetLength(1) - current.Shape[current.RotationOption].GetLength(1)), temp.GridPosition.Y);

                    holdPiece = temp;
                    holdPiece.GridPosition = new Point(11, 0);
                }
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Space) && InputManager.OldKeyboardState.IsKeyUp(Keys.Space)
                && current.GridPosition.Y > 0)
            {
                current.elapsedMoveDownTime = TimeSpan.Zero;

                var currentMarkedSpots = current.GetMarkedSpots(current.RotationOption, current.Shape);
                var lowestPoint = current.GridPosition.Y + current.PieceHeight;//current.Shape[current.RotationOption].GetLength(0);

                bool didFindLowestGround = false;
                var height = 0;
                
                for(int i = 0; i < grid.GetLength(0) - lowestPoint; i++)
                {
                    for(int j = 0; j < currentMarkedSpots[0].Count; j++)
                    {
                        if (current.GridPosition.Y < 0) break;

                        if(grid[current.GridPosition.Y + i + currentMarkedSpots[1][j], current.GridPosition.X + currentMarkedSpots[0][j]] == true)
                        {
                            didFindLowestGround = true;
                            height = j;
                            break;
                        }
                    }

                    if (didFindLowestGround)
                    {
                        current.GridPosition.Y += i - 1;
                        break;
                    }  
                }

                if (!didFindLowestGround)
                {
                    current.GridPosition.Y = (grid.GetLength(0) - 1) - current.PieceHeight;
                }
            }


            current.Update(gameTime);

            // for(int i = 0; i < )

            if (current.IsEnabled == false)
            {
                var spots = current.GetMarkedSpots(current.RotationOption, current.Shape);
                Texture2D textureToUse = null;
                switch (current.PieceType)
                {
                    case PieceTypes.LL:
                        textureToUse = Content.Load<Texture2D>("OrangeCell");
                        break;

                    case PieceTypes.RL:
                        textureToUse = Content.Load<Texture2D>("BlueCell");
                        break;

                    case PieceTypes.T:
                        textureToUse = Content.Load<Texture2D>("PurpleCell");
                        break;

                    case PieceTypes.LZZ:
                        textureToUse = Content.Load<Texture2D>("GreenCell");
                        break;

                    case PieceTypes.RZZ:
                        textureToUse = Content.Load<Texture2D>("RedCell");
                        break;

                    case PieceTypes.Square:
                        textureToUse = Content.Load<Texture2D>("YellowCell");
                        break;

                    case PieceTypes.Straight:
                        textureToUse = Content.Load<Texture2D>("LightBlueCell");
                        break;
                }
                for (int i = 0; i < spots[0].Count; i++)
                {
                    var point = new Point(current.GridPosition.X + spots[0][i], current.GridPosition.Y + spots[1][i]);
                    boomers.Add(new Cell(textureToUse, point, Color.White, Vector2.One));
                }

                current = nextTiles[0];
                current.GridPosition = new Point(4, -3);

                nextTiles.RemoveAt(0);

                foreach (var tile in nextTiles)
                {
                    tile.GridPosition.Y -= 4;
                }

                nextTiles.Add(GeneratePiece(2));
            }


            List<int> rowFilledYs = new List<int>();
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                int count = 0;
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == true)
                    {
                        count++;
                    }
                }
                if (count >= 10)
                {
                    rowFilledYs.Add(y);
                }
            }

            if (rowFilledYs.Count > 0 && amountOfFades == 0)
            {
                highestRowAffected = rowFilledYs.OrderByDescending(x => x).First();
                amountOfRowsToDrop = rowFilledYs.Count;

                for(int i = 0; i < rowFilledYs.Count; i++)
                {
                    for(int j = 0; j < boomers.Count; j++)
                    {
                        if(boomers[j].GridPosition.Y == rowFilledYs[i])
                        {
                            boomers[j].isFadingOut = true;
                            amountOfFades++;
                        }
                    }
                }
            }

            int doneCount = 0;
            for(int i = 0; i < boomers.Count; i++)
            {
                boomers[i].Update(gameTime);
                if(boomers[i].TravelPercentage >= 1f)
                {
                    doneCount++;
                }
            }

            if (doneCount == amountOfFades && amountOfFades != 0)
            {
                //reset amountOfFades to 0
                for (int i = 0; i < boomers.Count; i++)
                {
                    if (boomers[i].isFadingOut == true)
                    {
                        boomers.RemoveAt(i);
                        i--;
                        continue;
                    }

                    if (boomers[i].GridPosition.Y <= highestRowAffected)
                    {
                        boomers[i].GridPosition.Y += amountOfRowsToDrop;
                    }
                }

                amountOfFades = 0;

                for (int w = 0; w < grid.GetLength(0); w++)
                {
                    for (int z = 0; z < grid.GetLength(1); z++)
                    {
                        grid[w, z] = false;
                        for (int i = 0; i < boomers.Count; i++)
                        {
                            if (boomers[i].GridPosition.X == z &&
                                boomers[i].GridPosition.Y == w)
                            {
                                grid[w, z] = true;
                            }
                        }
                    }
                }
            }

            base.Update(gameTime);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < grid.GetLength(0) + 1; i++)
            {
                spriteBatch.Draw(Game1.Pixel, new Rectangle((int)offSet.X, i * Game1.GridCellSize, grid.GetLength(1) * Game1.GridCellSize, 1), Color.White);
            }

            for (int j = 0; j < grid.GetLength(1) + 1; j++)
            {
                spriteBatch.Draw(Game1.Pixel, new Rectangle(j * Game1.GridCellSize + (int)offSet.X, 0, 1, Graphics.GraphicsDevice.Viewport.Height), Color.White);
            }

            foreach (var tile in nextTiles)
            {
                tile.Draw(spriteBatch);
            }

            current.Draw(spriteBatch);
            foreach (var boomer in boomers)
            {
                boomer.Draw(spriteBatch);
            }

            holdPiece?.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
