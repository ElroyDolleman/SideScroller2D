using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Input;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class OnGroundState : PlayerBaseState
    {
        protected bool canJump = true;

        public OnGroundState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            player.Speed.Y = 0;
        }

        public override void Update()
        {
            player.UpdateXMovementControls();

            if (canJump && InputManager.JustPressed(player.Inputs.Jump))
            {
                Jump();
            }
        }

        protected virtual void Jump()
        {
            player.ChangeState(player.JumpState);

            player.DustParticles.Position = new Vector2(player.Hitbox.Center.X + player.FacingDirection, player.Hitbox.Bottom);
            player.DustParticles.PlayJump();
        }

        public override void OnCollision(AABBCollider collider)
        {
            
        }

        public override void OnCollisionResolution(CollisionResult collisionResult, List<AABBCollider> surroundingColliders)
        {
            base.OnCollisionResolution(collisionResult, surroundingColliders);

            foreach(AABBCollider collider in surroundingColliders)
            {
                if (collider.Hitbox.Top == player.Hitbox.Bottom && collider.Hitbox.Left < player.Hitbox.Right && collider.Hitbox.Right > player.Hitbox.Left)
                    return;
            }

            player.ChangeState(player.FallState);
        }
    }
}
