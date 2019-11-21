using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tetris.TestOfEachPiece;

namespace Tetris
{
    public class GameScreen : Screen
    {
        public static bool[,] grid = new bool[Game1.GridHeight, Game1.GridWidth];
        public static Vector2 offSet;

        Queue<BaseTetromino> nextTiles = new Queue<BaseTetromino>();

        public GameScreen(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            offSet = new Vector2((Graphics.GraphicsDevice.Viewport.Width - grid.GetLength(1) * Game1.GridCellSize) / 2, 0);

            for(int i = 0; i < 3; i++)
            {
                var pieceTypeToCreate = (PieceTypes)Game1.Random.Next(0, 6);
                var startPoint = new Point(-4, i * 4);
                BaseTetromino newPiece = null;
                switch (pieceTypeToCreate)
                {
                    case PieceTypes.LL:
                        newPiece = new LeftL(Content.Load<Texture2D>("leftLPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                        break;
                    
                    case PieceTypes.RL:
                        newPiece = new RightL(Content.Load<Texture2D>("rightLPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                        break;
                    
                    case PieceTypes.T:
                        newPiece = new TPiece(Content.Load<Texture2D>("smallTPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                        break;
                    
                    case PieceTypes.LZZ:
                        newPiece = new LeftZigZag(Content.Load<Texture2D>("leftZigZagPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                        break;
                    
                    case PieceTypes.RZZ:
                        newPiece = new RightZigZag(Content.Load<Texture2D>("rightZigZagPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                        break;
            
                    case PieceTypes.Square:
                        newPiece = new Square(Content.Load<Texture2D>("squarePiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                        break;

                    case PieceTypes.Straight:
                        newPiece = new StraightPiece(Content.Load<Texture2D>("straightPiece"), startPoint, Color.White, Vector2.One, RotationOptions.NoRotation);
                        break;
                }

                nextTiles.Enqueue(newPiece);
            }
        }   

        public override void Update(GameTime gameTime)
        { 


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

            base.Draw(spriteBatch);
        }
    }
}
