using System;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Code.Collision
{
    struct CollisionResult
    {
        public bool OnTop;
        public bool OnLeft;
        public bool OnRight;
        public bool OnBottom;

        public FloatRectangle HitboxOnOverlap;
    }
}
