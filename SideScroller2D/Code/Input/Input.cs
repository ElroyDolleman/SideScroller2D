using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SideScroller2D.Code.Input
{
    struct Input
    {
        public Keys Key;
        public Buttons Button;
        public PlayerIndex PlayerIndex;

        public Input(Keys key, Buttons button, PlayerIndex playerIndex)
        {
            this.Key = key;
            this.Button = button;
            this.PlayerIndex = playerIndex;
        }
    }
}
