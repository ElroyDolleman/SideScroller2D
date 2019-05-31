using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Collision;
using SideScroller2D.Input;

namespace SideScroller2D.GameLogic.Player.PlayerStates
{
    class WallSlideState : InAirState
    {
        protected int direction;

        public WallSlideState(Player player, int direction)
            : base(player)
        {
            this.direction = direction;
        }

        public override void OnEnter()
        {
            player.Speed = Vector2.Zero;
            player.Acceleration.X = 1;

            player.ChangeAnimation(Player.Animations.WallSlide);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.JustPressed(player.Inputs.Jump))
            {
                player.Speed.X = Player.RunSpeed * (direction * -1);
                player.Acceleration.X = 1;

                player.ChangeState(new JumpState(player, false));
            }
        }

        public override float GetGravity()
        {
            if (player.Speed.Y > maxFallspeed / 12)
            {
                player.Speed.Y = maxFallspeed / 12;
                return 0;
            }

            return defaultGravity;
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnBottom)
            {
                if (player.Speed.X == 0)
                    player.ChangeState(new IdleState(player));

                else
                    player.ChangeState(new RunState(player));

                return;
            }

            bool hold = (direction == 1 && InputManager.IsDown(player.Inputs.Right)) || (direction == -1 && InputManager.IsDown(player.Inputs.Left));

            if (hold)
            {
                foreach (Rectangle collider in colliders)
                {
                    int side = direction == 1 ? player.Hitbox.Right : player.Hitbox.Left;
                    int colliderSide = direction == 1 ? collider.Left : collider.Right;

                    // As long as there is a wall next to the player prevent changing state
                    if (side == colliderSide && player.Hitbox.Top < collider.Bottom && player.Hitbox.Bottom > collider.Top)
                        return;
                }
            }

            player.ChangeState(new FallState(player));
        }
    }
}
