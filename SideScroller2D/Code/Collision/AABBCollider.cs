using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Code.Collision
{
    class AABBCollider
    {
        public enum CollisionTypes
        {
            Solid,
            SemiSolid,
            Breakable
        }

        public Action OnBreak;
        public FloatRectangle Hitbox { get; private set; }
        public CollisionTypes CollisionType;

        public AABBCollider(FloatRectangle hitbox, CollisionTypes collisionType)
        {
            Hitbox = hitbox;
            CollisionType = collisionType;
        }
    }
}
