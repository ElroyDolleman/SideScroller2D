using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using SideScroller2D.Collision;

namespace SideScroller2D.GameLogic.Player.PlayerStates
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
        }

        public override void OnCollision(CollisionResult collisionResult)
        {
            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnBottom)
            {
                if (player.Speed.X == 0)
                    player.ChangeState(new IdleState(player));

                else
                    player.ChangeState(new RunState(player));
            }
        }
    }
}
