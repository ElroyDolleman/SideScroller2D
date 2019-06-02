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
        protected float jumpPower = 280f;
        protected float gravitySlowDownMultiplier = 0.39f;

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

            return base.GetGravity() * 1.4f;
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnTop)
            {
                bool doHeadBonk = true;
                int count = 0;
                float newXPos = player.Position.X;

                foreach (Rectangle collider in colliders)
                {
                    if (player.Hitbox.Top == collider.Bottom)
                        count++;

                    if (count == 1 && (MathHelper.Distance(player.Hitbox.Left, collider.Left) > 4 && MathHelper.Distance(player.Hitbox.Right, collider.Right) > 4))
                    {
                        doHeadBonk = false;

                        if (player.Hitbox.Left < collider.Left)
                            newXPos = collider.Left - player.Hitbox.Width;

                        else if (player.Hitbox.Right > collider.Right)
                            newXPos = collider.Right;
                    }
                }

                if (doHeadBonk || count > 1)
                    HeadBonk();
                else
                {
                    player.ChangePosition(new Vector2(newXPos, player.Position.Y - 1));
                }
            }

            base.OnCollision(collisionResult, colliders);
        }

        protected virtual void HeadBonk()
        {
            player.Speed.Y = 0;
        }
    }
}
