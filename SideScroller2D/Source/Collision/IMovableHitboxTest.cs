using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Collision
{
    interface IMovableHitboxTest
    {
        Rectangle Hitbox { get; }

        void MoveX(float amount);
        void MoveY(float amount);

        void ChangePosition(Vector2 newPosition);
    }
}
