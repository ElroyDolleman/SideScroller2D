using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using SideScroller2D.Code.Input;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class InAirState : PlayerBaseState
    {
        protected bool canWallJump = true;

        public InAirState(Player player)
            : base(player)
        {
            InAir = true;
        }

        public override void OnEnter()
        {
            
        }

        public override void Update()
        {
            player.UpdateXMovementControls(PlayerStats.AirAcceleration, PlayerStats.RunSpeed);
        }

        protected virtual void ApplyGravity()
        {
            if (player.Speed.Y < PlayerStats.MaxFallSpeed)
                player.Speed.Y += GetGravity();

            else if (player.Speed.Y > PlayerStats.MaxFallSpeed)
                player.Speed.Y = PlayerStats.MaxFallSpeed;
        }

        protected virtual float GetGravity()
        {
            return PlayerStats.DefaultGravity;
        }

        public override void OnCollision(AABBCollider collider)
        {
            
        }

        public override void OnCollisionResolution(CollisionResult collisionResult, List<AABBCollider> surroundingColliders)
        {
            base.OnCollisionResolution(collisionResult, surroundingColliders);

            if (collisionResult.OnBottom)
            {
                Land();
                return;
            }

            if (collisionResult.OnTop && player.Speed.Y < 0)
            {
                foreach(AABBCollider collider in surroundingColliders)
                {
                    if (collider.CollisionType == AABBCollider.CollisionTypes.Breakable && collider.Hitbox.Bottom == player.Hitbox.Top && collisionResult.HitboxOnOverlap.Intersects(collider.Hitbox))
                        collider.OnBreak();
                }

                HeadBonk();
            }

            if (canWallJump && InputManager.JustPressed(player.Inputs.Jump))
            {
                bool doWallJump = (collisionResult.OnLeft || collisionResult.OnRight);
                int direction = collisionResult.OnLeft ? 1 : -1;

                // When there was no collision resolution, check if there is a wall next to the player
                if (!doWallJump)
                {
                    foreach (AABBCollider collider in surroundingColliders)
                    {
                        if (collider.Hitbox.Right == player.Hitbox.Left)
                        {
                            doWallJump = true;
                            direction = 1;
                            break;
                        }
                        else if (collider.Hitbox.Left == player.Hitbox.Right)
                        {
                            doWallJump = true;
                            direction = -1;
                            break;
                        }
                    }

                    // If a walljump is still not possible, cancel the function
                    if (!doWallJump)
                        return;
                }

                // Do a normal wall jump with full force when pressing the directional button towards the wall
                if (Input.InputManager.IsDown(player.Inputs.GetDirectionalInputX(direction * -1).Value))
                {
#if DEBUG
                    //Console.WriteLine("InAirState::NormalWallJump  CurrentState = {0}", player.CurrentState);
#endif
                    WallJump(PlayerStats.WallJumpHorizontalForce * direction);
                }
                // Do a weak wall jump when releasing the directional buttons
                else
                {
#if DEBUG
                    //Console.WriteLine("InAirState::WeakWallJump  CurrentState = {0}", player.CurrentState);
#endif
                    WallJump(PlayerStats.WeakWallJumpHorizontalForce * direction);
                    player.WallJumpState.InputDisabledTimer = PlayerStats.WeakWallJumpInputDisabledTime;
                }
            }
        }

        protected virtual void Land()
        {
#if DEBUG
            //Console.WriteLine("InAirState::Land");
#endif
            if (player.Speed.Y >= PlayerStats.MinFallSpeedToShowDust)
            {
                player.DustParticles.Position = new Vector2(player.Hitbox.Center.X + player.FacingDirection, player.Hitbox.Bottom);
                player.DustParticles.PlayLand();
            }

            player.Speed.Y = 0;

            if (player.Speed.X == 0)
                player.ChangeState(player.IdleState);
            else
                player.ChangeState(player.RunState);
        }

        protected virtual void HeadBonk()
        {
#if DEBUG
            //Console.WriteLine("InAirState::HeadBonk");
#endif
            player.Speed.Y = 0;

            player.ChangeState(player.FallState);
        }

        protected virtual void WallJump(float speedX)
        {
            int dir = Math.Sign(speedX) * -1;
            float x = dir == 1 ? player.Hitbox.Right : player.Hitbox.Left;

            player.DustParticles.Position = new Vector2(x, player.Hitbox.Center.Y);
            player.DustParticles.PlayWallJump(dir);

            player.Speed.X = speedX;

            player.ChangeState(player.WallJumpState);
        }
    }
}
