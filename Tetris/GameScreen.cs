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
        bool[,] grid = new bool[Game1.GridHeight, Game1.GridWidth];
        public static Vector2 offSet;

        TimeSpan moveDownTimer = TimeSpan.FromMilliseconds(750);
        TimeSpan elapsedMoveDownTime = TimeSpan.Zero;

        StraightPiece straightPiece;
        public GameScreen(ContentManager content, GraphicsDeviceManager graphics) 
            : base(content, graphics)
        {
            offSet = new Vector2((Graphics.GraphicsDevice.Viewport.Width - grid.GetLength(1) * Globals.CellSize) / 2, 0);

      
            straightPiece = new StraightPiece(Content.Load<Texture2D>("straightPiece"), new Point(0, 0), Color.White, Vector2.One, RotationOptions.NintyDegrees);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedMoveDownTime += gameTime.ElapsedGameTime;

            straightPiece.Update(gameTime);

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

            straightPiece.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
