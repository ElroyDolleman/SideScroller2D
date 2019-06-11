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
    class FallState : InAirState
    {
        public FallState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            player.ChangeAnimation(Player.Animations.Fall);
        }

        public override void Update()
        {
            ApplyGravity();

            player.UpdateHorizontalMovementControls();
        }

        public override void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            base.OnCollision(collisionResult, tiles);

            if (player.CurrentState != this)
                return;

            if ((collisionResult.Horizontal == CollisionResult.HorizontalResults.OnRight && InputManager.IsDown(player.Inputs.Right)) ||
                (collisionResult.Horizontal == CollisionResult.HorizontalResults.OnLeft && InputManager.IsDown(player.Inputs.Left)))
            {
                player.ChangeState(player.WallSlideState);
            }
        }
    }
}
