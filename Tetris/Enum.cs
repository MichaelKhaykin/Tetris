using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum ScreenStates
    { 
        Menu,
        Game
    }

    public enum TetrominoType
    { 
        Straight,
        LeftL,
        RightL,
        Square,
        RightZigZag,
        LeftZigZag,
        SmallT
    }
}
