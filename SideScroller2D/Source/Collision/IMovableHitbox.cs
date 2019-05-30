using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Collision
{
    interface IMovableHitbox
    {
        Rectangle Hitbox { get; }
    }
}
