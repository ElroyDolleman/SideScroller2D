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
        protected float slowGravity = 14f;
        protected bool canMove;

        public JumpState(Player player, bool canMove = true)
            : base(player)
        {
            this.canMove = canMove;
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

            if (canMove)
                player.UpdateMovement();

            if (player.Speed.Y > 0)
                player.ChangeState(new FallState(player));
        }

        public override float GetGravity()
        {
            if (InputManager.IsDown(player.Inputs.Jump))
                return slowGravity;

            slowGravity = defaultGravity;

            return base.GetGravity();
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnTop)
            {
                player.Speed.Y = 0;
            }
        }
    }
}
