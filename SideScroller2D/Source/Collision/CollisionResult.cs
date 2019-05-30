using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller2D.Collision
{
    struct CollisionResult
    {
        public enum HorizontalResults
        {
            None,
            OnRight,
            OnLeft
        }

        public enum VerticalResults
        {
            None,
            OnTop,
            OnBottom
        }

        public HorizontalResults Horizontal;
        public VerticalResults Vertical;
    }
}
