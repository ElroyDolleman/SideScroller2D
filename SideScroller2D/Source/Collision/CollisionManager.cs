using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Collision
{
    static class CollisionManager
    {
        /// <summary>
        /// Checks if a movable hitbox collides with a collider and calculates the position if it were to be pushed out of the collider.
        /// </summary>
        /// <param name="movable">The movable object</param>
        /// <param name="collider">The collider the movable object interacts with</param>
        /// <returns>The new location for the movable object</returns>
        public static Point ResolveAABB(IMovableHitbox movable, Rectangle collider)
        {
            Point newPos = movable.Hitbox.Location;

            if (!movable.Hitbox.Intersects(collider))
                return newPos;

            if (movable.Hitbox.Left < collider.Left)
            {
                newPos.X = collider.Left - movable.Hitbox.Width;
            }
            else if (movable.Hitbox.Right > collider.Right)
            {
                newPos.X = collider.Right;
            }

            if (movable.Hitbox.Top < collider.Top)
            {
                newPos.Y = collider.Top - movable.Hitbox.Height;
            }
            else if (movable.Hitbox.Bottom > collider.Bottom)
            {
                newPos.Y = collider.Bottom;
            }

            return newPos;
        }
    }
}
