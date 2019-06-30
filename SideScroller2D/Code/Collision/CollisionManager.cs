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
        public static void MoveActor(Actor actor, List<AABBCollider> colliders)
        {
            var result = new CollisionResult();
            result.HitboxOnOverlap = actor.NextHitbox;

            if (actor.Speed.X != 0)
                actor.MoveX();

            foreach(AABBCollider collider in colliders)
            {
                if (!actor.Hitbox.Intersects(collider.Hitbox) || collider.CollisionType == AABBCollider.CollisionTypes.SemiSolid)
                    continue;

                actor.OnCollision(collider);

                if (collider == null || !actor.Hitbox.Intersects(collider.Hitbox))
                    continue;

                if (actor.Hitbox.Left < collider.Hitbox.Left)
                {
                    actor.SetX(collider.Hitbox.Left - actor.Hitbox.Width);
                    result.OnRight = true;
                }

                else if (actor.Hitbox.Right > collider.Hitbox.Right)
                {
                    actor.SetX(collider.Hitbox.Right);
                    result.OnLeft = true;
                }
            }

            var oldBottom = actor.Hitbox.Bottom;
            if (actor.Speed.Y != 0)
                actor.MoveY();

            for (int i = 0; i < colliders.Count; i++)
            {
                var collider = colliders[i];

                if (!actor.Hitbox.Intersects(collider.Hitbox))
                    continue;

                actor.OnCollision(collider);

                if (collider == null || !actor.Hitbox.Intersects(collider.Hitbox))
                    continue;

                if (actor.Hitbox.Top < collider.Hitbox.Top)
                {
                    if ((actor.Speed.Y <= 0 || oldBottom > collider.Hitbox.Top) && collider.CollisionType == AABBCollider.CollisionTypes.SemiSolid)
                        continue;

                    actor.SetY(collider.Hitbox.Top - actor.Hitbox.Height);
                    result.OnBottom = true;
                }

                else if (actor.Hitbox.Bottom > collider.Hitbox.Bottom && collider.CollisionType != AABBCollider.CollisionTypes.SemiSolid)
                {
                    float adjustedX = actor.Position.X;

                    // TODO: Let the actor determine how corner adjustment works
                    if (MathHelper.Distance(actor.Hitbox.Right, collider.Hitbox.Left) < 5)
                    {
                        adjustedX = collider.Hitbox.Left - actor.Hitbox.Width;
                    }
                    else if (MathHelper.Distance(actor.Hitbox.Left, collider.Hitbox.Right) < 5)
                    {
                        adjustedX = collider.Hitbox.Right;
                    }

                    if (adjustedX != actor.Position.X)
                    {
                        for (int j = 0; j < colliders.Count; j++)
                        {
                            if (i == j)
                                continue;

                            if (colliders[j].Hitbox.Bottom == collider.Hitbox.Bottom && actor.Hitbox.Intersects(colliders[j].Hitbox))
                            {
                                adjustedX = actor.Position.X;
                                break;
                            }
                        }
                    }

                    if (adjustedX == actor.Position.X)
                    {
                        actor.SetY(collider.Hitbox.Bottom);
                        result.OnTop = true;
                    }
                    else
                    {
                        result.OnRight = adjustedX == collider.Hitbox.Right;
                        result.OnLeft = adjustedX == collider.Hitbox.Left - actor.Hitbox.Width;

                        actor.SetX(adjustedX);
                    }
                }
            }

            actor.OnCollisionResolution(result, colliders);
        }
    }
}
