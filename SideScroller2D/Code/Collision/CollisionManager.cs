using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Code.Collision
{
    static class CollisionManager
    {
        public static CollisionResult MoveEntity(MovableEntity movable, List<Rectangle> colliders)
        {
            CollisionResult result = new CollisionResult();

            movable.UpdateHorizontalMovement();

            foreach (Rectangle collider in colliders)
            {
                if (!movable.Hitbox.Intersects(collider))
                    continue;

                if (movable.Hitbox.Left < collider.Left)
                {
                    movable.ChangePositionX((float)(collider.Left - movable.Hitbox.Width));

                    result.Horizontal = CollisionResult.HorizontalResults.OnRight;
                }

                else if (movable.Hitbox.Right > collider.Right)
                {
                    movable.ChangePositionX((float)collider.Right);

                    result.Horizontal = CollisionResult.HorizontalResults.OnLeft;
                }
            }

            movable.UpdateVerticalMovement();

            foreach (Rectangle collider in colliders)
            {
                if (!movable.Hitbox.Intersects(collider))
                    continue;

                if (movable.Hitbox.Top < collider.Top)
                {
                    movable.ChangePositionY((float)(collider.Top - movable.Hitbox.Height));

                    result.Vertical = CollisionResult.VerticalResults.OnBottom;
                }

                else if (movable.Hitbox.Bottom > collider.Bottom)
                {
                    movable.ChangePositionY((float)collider.Bottom);

                    result.Vertical = CollisionResult.VerticalResults.OnTop;
                }
            }

            return result;
        }
    }
}
