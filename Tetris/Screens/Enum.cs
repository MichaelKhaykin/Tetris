using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum ScreenStates
    { 
        None,
        Menu,
        Settings,
        Game
    }

    public enum RotationOptions
    {
        NoRotation = 0,
        NintyDegrees = 90,
        HundredEightyDegrees = 180,
        TwoHundredSeventyDegrees = 270
    }

    public enum PieceTypes
    { 
        LL,
        RL,
        T,
        LZZ,
        RZZ,
        Square,
        Straight
    }
}
