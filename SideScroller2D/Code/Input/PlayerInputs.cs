using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SideScroller2D.Code.Input
{
    struct PlayerInputs
    {
        public bool UsesJoystick;

        //public GamePadState CurrentGamePadState;
        //public KeyboardState CurrentKeyboardState;

        //public PlayerIndex PlayerIndex;

        public Input Jump;

        public Input Left;
        public Input Right;
        public Input Up;
        public Input Down;
    }
}
