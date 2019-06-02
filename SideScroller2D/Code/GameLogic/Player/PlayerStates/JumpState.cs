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
    class JumpState : InAirState
    {
        protected float jumpPower = 290f;
        protected float gravitySlowDownMultiplier = 0.4f;

        public JumpState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            player.ChangeAnimation(Player.Animations.Jump);

            player.Speed.Y = -jumpPower;

            // Cannot wall jump on first frame
            canWallJump = false;

            AudioManager.PlaySound(GameSounds.PlayerJump);
        }

        public override void Update(GameTime gameTime)
        {
            // Enable wall jump on first frame
            if (!canWallJump)
                canWallJump = true;

            ApplyGravity();

            player.UpdateHorizontalMovementControls();

            if (player.Speed.Y > 0)
                player.ChangeState(player.FallState);
        }

        public override float GetGravity()
        {
            if (InputManager.IsDown(player.Inputs.Jump))
                return defaultGravity * gravitySlowDownMultiplier;

            return base.GetGravity();
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnTop)
            {
                HeadBonk();
            }

            base.OnCollision(collisionResult, colliders);
        }

        protected virtual void HeadBonk()
        {
            player.Speed.Y = 0;
        }
    }
}
