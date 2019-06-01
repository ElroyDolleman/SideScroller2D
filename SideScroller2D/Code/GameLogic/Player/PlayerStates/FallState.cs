using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Collision;

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            player.UpdateHorizontalMovementControls();
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            base.OnCollision(collisionResult, colliders);

            if (player.CurrentState != this)
                return;

            if (collisionResult.Horizontal == CollisionResult.HorizontalResults.OnRight)
            {
                player.WallSlideState.Direction = 1;
                player.ChangeState(player.WallSlideState);
            }
            else if (collisionResult.Horizontal == CollisionResult.HorizontalResults.OnLeft)
            {
                player.WallSlideState.Direction = -1;
                player.ChangeState(player.WallSlideState);
            }
        }
    }
}
