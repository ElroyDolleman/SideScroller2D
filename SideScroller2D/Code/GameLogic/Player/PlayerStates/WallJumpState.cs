using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Input;
using SideScroller2D.Code.Audio;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class WallJumpState : JumpState
    {
        public int Direction = 1;

        public WallJumpState(Player player)
            : base(player)
        {
            jumpPower = 260f;
        }

        public override void OnEnter()
        {
            player.Speed.X = Player.RunSpeed;
            player.Acceleration.X = 1f * (Direction * -1);

            base.OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            ApplyGravity();

            player.UpdateHorizontalMovementControls(Player.RunSpeed, 0.039f);

            if (player.Speed.Y > 0)
                player.ChangeState(player.FallState);
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            base.OnCollision(collisionResult, colliders);
        }
    }
}
