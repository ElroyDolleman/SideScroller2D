using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class WallSlideState : InAirState
    {
        public WallSlideState(Player player)
            : base(player)
        {
            maxFallspeed = 40f;
        }

        public override void OnEnter()
        {
            player.Speed.X = 0;
            player.Acceleration.X = 0;

            player.ChangeAnimation(Player.Animations.WallSlide);
        }

        public override void Update(GameTime gameTime)
        {
            ApplyGravity();
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            base.OnCollision(collisionResult, colliders);

            if (player.CurrentState != this)
                return;

            bool hold = (player.FacingDirection == 1 && InputManager.IsDown(player.Inputs.Right)) || (player.FacingDirection == -1 && InputManager.IsDown(player.Inputs.Left));

            if (hold)
            {
                foreach (Rectangle collider in colliders)
                {
                    int side = player.FacingDirection == 1 ? player.Hitbox.Right : player.Hitbox.Left;
                    int colliderSide = player.FacingDirection == 1 ? collider.Left : collider.Right;

                    // As long as there is a wall next to the player prevent changing state
                    if (side == colliderSide && player.Hitbox.Top < collider.Bottom && player.Hitbox.Bottom > collider.Top)
                        return;
                }
            }

            player.ChangeState(player.FallState);
        }
    }
}
