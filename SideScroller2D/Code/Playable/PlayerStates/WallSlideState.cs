using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Input;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Utilities.Time;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class WallSlideState : InAirState
    {
        protected const float dustInterval = 0.1f;
        protected float dustTimer = 0;

        public WallSlideState(Player player)
            : base(player)
        {
            
        }

        public override void OnEnter()
        {
            player.ChangeAnimation(PlayerAnimations.WallSlide);
        }

        public override void Update()
        {
            base.Update();

            ApplyGravity();

            dustTimer += ElapsedTime.Seconds;
            if (dustTimer >= dustInterval)
            {
                dustTimer -= dustInterval;

                float x = player.FacingDirection == 1 ? player.Hitbox.Right : player.Hitbox.Left;

                player.DustParticles.Position = new Vector2(x, player.Hitbox.Bottom - 2);
                player.DustParticles.PlayWallSlide(player.FacingDirection);
            }
        }

        protected override void ApplyGravity()
        {
            if (player.Speed.Y < PlayerStats.WallSlideMaxFallSpeed)
                player.Speed.Y += GetGravity();

            else if (player.Speed.Y > PlayerStats.WallSlideMaxFallSpeed)
                player.Speed.Y = PlayerStats.WallSlideMaxFallSpeed;
        }

        protected override float GetGravity()
        {
            return base.GetGravity() * PlayerStats.WallSlideGravityMultiplier;
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

            if (!collisionResult.OnLeft && !collisionResult.OnRight)
            {
                player.ChangeState(player.FallState);
            }
        }
    }
}
