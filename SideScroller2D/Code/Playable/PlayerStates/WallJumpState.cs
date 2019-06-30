using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SideScroller2D.Code.Utilities.Time;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    class WallJumpState : JumpState
    {
        public float InputDisabledTimer { get; set; }

        public WallJumpState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            canWallJump = true;

            player.FacingDirection = Math.Sign(player.Speed.X);

            if (Math.Abs(player.Speed.X) == PlayerStats.WallJumpHorizontalForce)
            {
                InputDisabledTimer = PlayerStats.WallJumpInputDisabledTime;
            }
        }

        public override void Update()
        {
            if (InputDisabledTimer > 0)
            {
                InputDisabledTimer -= ElapsedTime.Seconds;

                ApplyGravity();

                if (player.Speed.Y > 0)
                    player.ChangeState(player.FallState);
            }
            else
                base.Update();
        }
    }
}
