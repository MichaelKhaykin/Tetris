using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tetris.TestOfEachPiece;
using Newtonsoft.Json;
using System.IO;

namespace Tetris
{
    public class GameScreen : Screen
    {
        public static bool[,] grid = new bool[Game1.GridHeight, Game1.GridWidth];
        public static Vector2 offSet;

        List<BaseTetromino> nextTiles = new List<BaseTetromino>();

        List<BaseTetromino> boomers = new List<BaseTetromino>();

        BaseTetromino current;

        public bool isPaused = false;
        public GameScreen(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            offSet = new Vector2((Graphics.GraphicsDevice.Viewport.Width - grid.GetLength(1) * Game1.GridCellSize) / 2, 0);

            for (int i = 0; i < 3; i++)
            {
                nextTiles.Add(GeneratePiece(i));
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
                    newPiece = new LeftL(Content.Load<Texture2D>("leftLPiece"), startPoint, Color.Orange, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.RL:
                    newPiece = new RightL(Content.Load<Texture2D>("rightLPiece"), startPoint, Color.Blue, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.T:
                    newPiece = new TPiece(Content.Load<Texture2D>("smallTPiece"), startPoint, Color.Purple, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.LZZ:
                    newPiece = new LeftZigZag(Content.Load<Texture2D>("leftZigZagPiece"), startPoint, Color.Green, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.RZZ:
                    newPiece = new RightZigZag(Content.Load<Texture2D>("rightZigZagPiece"), startPoint, Color.Red, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.Square:
                    newPiece = new Square(Content.Load<Texture2D>("squarePiece"), startPoint, Color.Yellow, Vector2.One, RotationOptions.NoRotation);
                    break;

                case PieceTypes.Straight:
                    newPiece = new StraightPiece(Content.Load<Texture2D>("straightPiece"), startPoint, Color.Blue, Vector2.One, RotationOptions.NoRotation);
                    break;
            }

            return newPiece;
        }
        public override void Update(GameTime gameTime)
        {
            if(InputManager.KeyboardState.IsKeyDown(Keys.P))
            {
                isPaused = !isPaused;
                string content = "";
                for(int i = 0; i < grid.GetLength(0); i++)
                {
                    for(int j = 0; j < grid.GetLength(1); j++)
                    {
                        if(grid[i, j] == true)
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

            current.Update(gameTime);

           // for(int i = 0; i < )

            if(current.IsEnabled == false)
            {
                boomers.Add(current);
         
                current = nextTiles[0];
                current.GridPosition = new Point(4, -3);

                nextTiles.RemoveAt(0);

                foreach (var tile in nextTiles)
                {
                    tile.GridPosition.Y -= 4;
                }
              
                nextTiles.Add(GeneratePiece(2));
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            for(int i = 0; i < grid.GetLength(0) + 1; i++)
            {
                spriteBatch.Draw(Game1.Pixel, new Rectangle((int)offSet.X, i * Game1.GridCellSize, grid.GetLength(1) * Game1.GridCellSize, 1), Color.White);
            }

            for(int j = 0; j < grid.GetLength(1) + 1; j++)
            {
                spriteBatch.Draw(Game1.Pixel, new Rectangle(j * Game1.GridCellSize + (int)offSet.X, 0, 1,  Graphics.GraphicsDevice.Viewport.Height), Color.White); 
            }

            foreach(var tile in nextTiles)
            {
                tile.Draw(spriteBatch);
            }

            current.Draw(spriteBatch);
            foreach(var boomer in boomers)
            {
                boomer.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }
    }
}
