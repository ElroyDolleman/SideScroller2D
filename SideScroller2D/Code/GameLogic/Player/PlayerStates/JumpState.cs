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
        protected float jumpPower = 332f;
        protected float gravitySlowDownMultiplier = 0.4f;

        public JumpState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            player.ChangeAnimation(Player.Animations.Jump);

            player.Speed.Y = -jumpPower;

            AudioManager.PlaySound(GameSounds.PlayerJump);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

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
        }

        protected void HeadBonk()
        {
            player.Speed.Y = 0;
        }
    }
}
