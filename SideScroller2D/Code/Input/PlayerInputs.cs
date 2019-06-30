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
        public Input Action;

        public Input Left;
        public Input Right;
        public Input Up;
        public Input Down;

        public Input? GetDirectionalInputX(int direction)
        {
            if (direction == 1)
                return Right;
            else if (direction == -1)
                return Left;

            return null;
        }

        public Input? GetDirectionalInputY(int direction)
        {
            if (direction == 1)
                return Up;
            else if (direction == -1)
                return Down;

            return null;
        }
    }
}
