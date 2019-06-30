using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SideScroller2D.Code.Input;
using SideScroller2D.Code.Audio;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class JumpState : InAirState
    {
        bool holdingJump;
        int frame;

        public JumpState(Player player)
            : base(player)
        {
            
        }

        public override void OnEnter()
        {
            player.ChangeAnimation(PlayerAnimations.Jump);

            player.Speed.Y = -PlayerStats.JumpPower;
            holdingJump = true;

            AudioManager.PlaySound(GameSounds.PlayerJump);

            // Cannot wall jump on the first frame of the jump
            canWallJump = false;
            frame = 0;
        }

        public override void Update()
        {
            if (!canWallJump && frame > 0)
                canWallJump = true;
            frame++;

            base.Update();

            ApplyGravity();

            if (player.Speed.Y > 0)
                player.ChangeState(player.FallState);
        }

        protected override float GetGravity()
        {
            if (holdingJump && InputManager.IsDown(player.Inputs.Jump))
            {
                return PlayerStats.DefaultGravity * PlayerStats.HoldJumpGravityMultiplier;
            }

            holdingJump = false;
            return base.GetGravity() * PlayerStats.ReleaseJumpGravityMultiplier;
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
