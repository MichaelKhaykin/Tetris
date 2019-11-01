using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    public class GameScreen : Screen
    {
        bool[,] grid = new bool[20, 10];
        Vector2 offSet;

        Queue<Tetromino> nextPieces = new Queue<Tetromino>();

        Tetromino lPiece;
        Tetromino rPiece;

        TimeSpan moveDownTimer = TimeSpan.FromMilliseconds(750);
        TimeSpan elapsedMoveDownTime = TimeSpan.Zero;
        public GameScreen(ContentManager content, GraphicsDeviceManager graphics) 
            : base(content, graphics)
        {
            offSet = new Vector2((Graphics.GraphicsDevice.Viewport.Width - grid.GetLength(1) * Globals.CellSize) / 2, 0);

            lPiece = new Tetromino(content.Load<Texture2D>("leftLPiece"), new Vector2(0, 0), Color.White, Vector2.One, TetrominoType.LeftL);
            //rPiece = new Tetromino(content.Load<Texture2D>("rightLPiece"), new Vector2(2, 2), Color.White, Vector2.One, TetrominoType.RightL);

            AddToDrawList(lPiece);
         //   AddToDrawList(rPiece);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedMoveDownTime += gameTime.ElapsedGameTime;

            if(elapsedMoveDownTime >= moveDownTimer)
            {
                elapsedMoveDownTime = TimeSpan.Zero;
                lPiece.GridPosition = new Vector2(lPiece.GridPosition.X, lPiece.GridPosition.Y + 1);
            }

            lPiece.Update(gameTime, offSet);
       //     rPiece.Update(gameTime, offSet);

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            for(int i = 0; i < grid.GetLength(0) + 1; i++)
            {
                spriteBatch.Draw(Game1.Pixel, new Rectangle((int)offSet.X, i * Globals.CellSize, grid.GetLength(1) * Globals.CellSize, 1), Color.White);
            }

            for(int j = 0; j < grid.GetLength(1) + 1; j++)
            {
                spriteBatch.Draw(Game1.Pixel, new Rectangle(j * Globals.CellSize + (int)offSet.X, 0, 1,  Graphics.GraphicsDevice.Viewport.Height), Color.White); 
            }
            

            base.Draw(spriteBatch);
        }
    }
}
