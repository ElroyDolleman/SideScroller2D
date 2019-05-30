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
    class OnGroundState : PlayerBaseState
    {
        public OnGroundState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            player.Speed.Y = 0;
        }

        public override void Update(GameTime gameTime)
        {
            //if (InputManager.JustPressed(player.Inputs.Jump))
            //{
            //    player.ChangeState(new JumpState(player));
            //}
            //else if (InputManager.IsDown(player.Inputs.Left) || InputManager.IsDown(player.Inputs.Right))
            //{
            //    player.ChangeState(new RunState(player));
            //}
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            foreach (Rectangle collider in colliders)
            {
                // Check if a collider is directly underneath the player, if so stop this function to prevent going in the FallState
                if (player.Hitbox.Bottom == collider.Top && player.Hitbox.Left < collider.Right && player.Hitbox.Right > collider.Left)
                    return;
            }
            
            player.ChangeState(new FallState(player));
        }
    }
}
