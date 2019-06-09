using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.GameLogic.Level;

namespace SideScroller2D.Code.Collision
{
    static class CollisionManager
    {
        public static CollisionResult MoveEntity(MovableEntity movable, List<Tile> tiles)
        {
            CollisionResult result = new CollisionResult();

            movable.UpdateHorizontalMovement();

            foreach (Tile tile in tiles)
            {
                if (tile == null || !tile.Solid || !movable.Hitbox.Intersects(tile.Hitbox))
                    continue;

                if (movable.Hitbox.Left < tile.Hitbox.Left)
                {
                    movable.ChangePositionX((float)(tile.Hitbox.Left - movable.Hitbox.Width));

                    result.Horizontal = CollisionResult.HorizontalResults.OnRight;
                }

                else if (movable.Hitbox.Right > tile.Hitbox.Right)
                {
                    movable.ChangePositionX((float)tile.Hitbox.Right);

                    result.Horizontal = CollisionResult.HorizontalResults.OnLeft;
                }
            }

            float prevBottom = movable.Hitbox.Bottom;
            movable.UpdateVerticalMovement();

            foreach (Tile tile in tiles)
            {
                if (tile == null || !movable.Hitbox.Intersects(tile.Hitbox))
                    continue;

                if (tile.TileType == TileTypes.SemiSolid)
                {
                    if (movable.Speed.Y > 0 && prevBottom <= tile.Hitbox.Top && movable.Hitbox.Bottom >= tile.Hitbox.Top)
                    {
                        movable.ChangePositionY((float)(tile.Hitbox.Top - movable.Hitbox.Height));
                        result.Vertical = CollisionResult.VerticalResults.OnBottom;
                    }

                    continue;
                }

                if (!tile.Solid)
                    continue;

                if (movable.Hitbox.Top < tile.Hitbox.Top)
                {
                    movable.ChangePositionY((float)(tile.Hitbox.Top - movable.Hitbox.Height));

                    result.Vertical = CollisionResult.VerticalResults.OnBottom;
                }

                else if (movable.Hitbox.Bottom > tile.Hitbox.Bottom)
                {
                    movable.ChangePositionY((float)tile.Hitbox.Bottom);

                    result.Vertical = CollisionResult.VerticalResults.OnTop;
                }
            }

            return result;
        }

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
