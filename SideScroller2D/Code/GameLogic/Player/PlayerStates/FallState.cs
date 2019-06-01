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

            player.UpdateMovement();
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnBottom)
            {
                if (player.Speed.X == 0)
                    player.ChangeState(new IdleState(player));

                else
                    player.ChangeState(new RunState(player));
            }

            else if (collisionResult.Horizontal == CollisionResult.HorizontalResults.OnRight)
            {
                player.ChangeState(new WallSlideState(player, 1));
            }
            else if (collisionResult.Horizontal == CollisionResult.HorizontalResults.OnLeft)
            {
                player.ChangeState(new WallSlideState(player, -1));
            }
        }
    }
}
