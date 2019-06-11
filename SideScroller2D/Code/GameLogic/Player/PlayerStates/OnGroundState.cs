using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.GameLogic.Level;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
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

        public override void Update()
        {
            player.UpdateHorizontalMovementControls();

            if (InputManager.JustPressed(player.Inputs.Jump))
            {
                player.ChangeState(player.JumpState);
            }
        }

        public override void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            base.OnCollision(collisionResult, tiles);

            foreach (Tile tile in tiles)
            {
                if (tile == null || !tile.CanStandOn)
                    continue;

                // Check if a collider is directly underneath the player, if so stop this function to prevent going in the FallState
                if (player.Hitbox.Bottom == tile.Hitbox.Top && player.Hitbox.Left < tile.Hitbox.Right && player.Hitbox.Right > tile.Hitbox.Left)
                    return;
            }
            
            player.ChangeState(player.FallState);
        }
    }
}
