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

        private Vector2 gridPosition;
        public ref Vector2 GridPosition { get => ref gridPosition; }

        public RotationOptions RotationOption { get; set; }

        public override float Rotation { get => (int)RotationOption;  }
        public BaseTetromino(Texture2D texture, Vector2 gridPosition, Color color, Vector2 scale, RotationOptions rotationOption) 
            : base(texture, Vector2.Zero, color, scale, (int)rotationOption, SpriteEffects.None, 0)
        {
            GridPosition = gridPosition;
            RotationOption = rotationOption;
        }

        public new virtual void Update(GameTime gameTime)
        { 
            if(InputManager.KeyboardState.IsKeyDown(Keys.Down) && InputManager.OldKeyboardState.IsKeyUp(Keys.Down))
            {
                GridPosition.Y += 1;
            }
            if(InputManager.KeyboardState.IsKeyDown(Keys.Left) && InputManager.OldKeyboardState.IsKeyUp(Keys.Left))
            {
                GridPosition.X -= 1;
            }
            if(InputManager.KeyboardState.IsKeyDown(Keys.Right) && InputManager.OldKeyboardState.IsKeyUp(Keys.Right))
            {
                GridPosition.X += 1;
            }
            if(InputManager.KeyboardState.IsKeyDown(Keys.Up) && InputManager.OldKeyboardState.IsKeyUp(Keys.Up))
            {
                switch (RotationOption)
                {
                    case RotationOptions.NoRotation:
                        RotationOption = RotationOptions.NintyDegrees;
                        break;
                    case RotationOptions.NintyDegrees:
                        RotationOption = RotationOptions.HundredEightyDegrees;
                        break;
                    case RotationOptions.HundredEightyDegrees:
                        RotationOption = RotationOptions.TwoHundredSeventyDegrees;
                        break;
                    case RotationOptions.TwoHundredSeventyDegrees:
                        RotationOption = RotationOptions.NoRotation;
                        break;
                }
            }

            elapsedMoveDownTime += gameTime.ElapsedGameTime;
            if(elapsedMoveDownTime >= moveDownTimer)
            {
                GridPosition.Y += 1;
                elapsedMoveDownTime = TimeSpan.Zero;
            }
        }
    }
}
