using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class InAirState : PlayerBaseState
    {
        //public float AirTime { get; private set; }

        protected float maxFallspeed = 280f;
        protected float defaultGravity = 26f;

        protected bool canWallJump = true;

        bool movesRight { get { return player.Speed.X > 0; } }

        public InAirState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        protected virtual void ApplyGravity()
        {
            if (player.Speed.Y < maxFallspeed)
                player.Speed.Y += GetGravity();

            else if (player.Speed.Y > maxFallspeed)
                player.Speed.Y = maxFallspeed;
        }

        public virtual float GetGravity()
        {
            return defaultGravity;
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            base.OnCollision(collisionResult, colliders);

            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnBottom)
            {
                Land();
            }

            else if (canWallJump && InputManager.JustPressed(player.Inputs.Jump))
            {
                foreach (Rectangle collider in colliders)
                {
                    // Check if the player is close enough to the wall for a walljump
                    if (MathHelper.Distance(player.Hitbox.Right, collider.Left) <= 1)
                        player.FacingDirection = 1;

                    else if (MathHelper.Distance(player.Hitbox.Left, collider.Right) <= 1)
                        player.FacingDirection = -1;

                    else
                        continue;

                    if (player.Hitbox.Top < collider.Bottom && player.Hitbox.Bottom > collider.Top)
                    {
                        player.ChangeState(player.WallJumpState);
                        return;
                    }
                }
            }
        }

        protected void Land()
        {
            player.Speed.Y = 0;

            if (player.Speed.X == 0)
                player.ChangeState(player.IdleState);

            else
                player.ChangeState(player.RunState);
        }
    }
}
