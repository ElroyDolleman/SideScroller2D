using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class FallState : InAirState
    {
        public FallState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            player.ChangeAnimation(PlayerAnimations.Fall);
        }

        public override void Update()
        {
            base.Update();

            ApplyGravity();
        }

        public override void OnCollision(AABBCollider collider)
        {
            base.OnCollision(collider);
        }

        public override void OnCollisionResolution(CollisionResult collisionResult, List<AABBCollider> surroundingColliders)
        {
            base.OnCollisionResolution(collisionResult, surroundingColliders);

            if (player.CurrentState != this)
                return;

            if (collisionResult.OnLeft || collisionResult.OnRight)
            {
                player.ChangeState(player.WallSlideState);
            }
        }
    }
}
