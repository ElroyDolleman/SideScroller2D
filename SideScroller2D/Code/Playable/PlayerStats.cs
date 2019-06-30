using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideScroller2D.Code.Playable
{
    static class PlayerStats
    {
        public const float RunSpeed = 128f;
        public const float RunAcceleration = 12f;
        public const float AirAcceleration = 8f;

        public const float WallJumpHorizontalForce = 128f;
        public const float WallJumpInputDisabledTime = 11f / 60f;
        public const float WeakWallJumpInputDisabledTime = 8f / 60f;
        public const float WeakWallJumpHorizontalForce = 108f;

        public const float JumpPower = 280f;
        public const float DefaultGravity = 26f;
        public const float HoldJumpGravityMultiplier = 0.39f;
        public const float ReleaseJumpGravityMultiplier = 1.4f;
        public const float WallSlideGravityMultiplier = 0.5f;

        public const float MinFallSpeedToShowDust = 290f;
        public const float MaxFallSpeed = 300f;
        public const float WallSlideMaxFallSpeed = 42f;
    }
}
