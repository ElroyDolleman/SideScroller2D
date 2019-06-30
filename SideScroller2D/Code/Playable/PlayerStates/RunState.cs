using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class RunState : OnGroundState
    {
        public RunState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            player.ChangeAnimation(PlayerAnimations.Run);

            base.OnEnter();
        }

        public override void Update()
        {
            base.Update();

            if (player.Speed.X == 0 && player.CurrentState == this)
                player.ChangeState(player.IdleState);
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
