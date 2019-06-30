using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class IdleState : OnGroundState
    {
        public IdleState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            player.ChangeAnimation(PlayerAnimations.Idle);

            player.Speed.X = 0;

            base.OnEnter();
        }

        public override void Update()
        {
            base.Update();

            if (player.Speed.X != 0)
                player.ChangeState(player.RunState);
        }

        public override void OnCollision(AABBCollider collider)
        {
            base.OnCollision(collider);
        }

        public override void OnCollisionResolution(CollisionResult collisionResult, List<AABBCollider> surroundingColliders)
        {
            base.OnCollisionResolution(collisionResult, surroundingColliders);
        }
    }
}
