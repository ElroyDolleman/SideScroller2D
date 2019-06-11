using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Code.Utilities.Time
{
    static class ElapsedTime
    {
        public static float Seconds { get; private set; }
        public static float Milliseconds { get; private set; }

        public static void Update(GameTime gameTime)
        {
            Seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Milliseconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
