using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.GameLogic.Level;
using SideScroller2D.Code.Input;
using SideScroller2D.Code.Audio;
using SideScroller2D.Code.Particles;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class WallJumpState : JumpState
    {
        float inputDisabledTimer = 0;

        public WallJumpState(Player player)
            : base(player)
        {
            jumpPower = 260f;
        }

        public override void OnEnter()
        {
            bool pressingDirectionTowardsWall = player.HoldsDirectionButtonTowardsFacingDirection;

            DustManager.AddWallJumpDustEffect(new Vector2(player.FacingDirection == 1 ? player.Hitbox.Right : player.Hitbox.Left, player.Hitbox.Center.Y), player.FacingDirection);

            // Turn Around
            player.FacingDirection *= -1;

            if (pressingDirectionTowardsWall || player.PreviousState == player.WallSlideState)
            {
                player.Speed.X = 132f;
                player.Acceleration.X = 1f * player.FacingDirection;

                inputDisabledTimer = 1f / 60f * 9;
            }
            else
            {
                player.Speed.X = 132f;
                player.Acceleration.X = 0.9f * player.FacingDirection;

                inputDisabledTimer = 1f / 60f * 6;
            }

            base.OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            ApplyGravity();

            // Enable wall jump on first frame
            if (!canWallJump)
                canWallJump = true;

            if (inputDisabledTimer <= 0)
                player.UpdateHorizontalMovementControls(Player.RunSpeed, 0.07f);
            else
                inputDisabledTimer -= Main.DeltaTime;

            if (player.Speed.Y > 0)
                player.ChangeState(player.FallState);
        }

        public override void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            base.OnCollision(collisionResult, tiles);
        }
    }
}
