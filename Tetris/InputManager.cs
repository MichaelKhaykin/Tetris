using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class InputManager
    {
        public static KeyboardState KeyboardState;

        public static KeyboardState OldKeyboardState;

        // NOTE: Not thread safe! I would implement IGameComponent instead,
        //       which does not require passing in the Game instance, and is
        //       therefore a much nicer implementation. 
        //       I can show you later, and contrast it.
    }
}
