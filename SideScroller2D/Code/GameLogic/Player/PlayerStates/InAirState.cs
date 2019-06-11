using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.GameLogic.Level;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Particles;
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

        public override void Update()
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

        public override void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            base.OnCollision(collisionResult, tiles);

            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnBottom)
            {
                Land();
            }

            else if (canWallJump && InputManager.JustPressed(player.Inputs.Jump))
            {
                foreach (Tile tile in tiles)
                {
                    if (tile == null || !tile.Solid)
                        continue;

                    // Check if the player is close enough to the wall for a walljump
                    if (MathHelper.Distance(player.Hitbox.Right, tile.Hitbox.Left) <= 1)
                        player.FacingDirection = 1;

                    else if (MathHelper.Distance(player.Hitbox.Left, tile.Hitbox.Right) <= 1)
                        player.FacingDirection = -1;

                    else
                        continue;

                    if (player.Hitbox.Top < tile.Hitbox.Bottom && player.Hitbox.Bottom > tile.Hitbox.Top)
                    {
                        player.ChangeState(player.WallJumpState);
                        return;
                    }
                }
            }
        }

        protected void Land()
        {
            if (player.Speed.Y > 278f)
                DustManager.AddOnLandingDustEffect(new Vector2(player.Hitbox.Center.X, player.Hitbox.Bottom));

            player.Speed.Y = 0;

            if (player.Speed.X == 0)
                player.ChangeState(player.IdleState);

            else
                player.ChangeState(player.RunState);
        }
    }
}
