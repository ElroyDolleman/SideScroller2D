using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class RunState : OnGroundState
    {
        public RunState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            player.ChangeAnimation(Player.Animations.Walk);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (player.Acceleration.X == 0 && player.CurrentState == this)
            {
                player.Speed.X = 0;
                player.ChangeState(player.IdleState);
            }
        }
    }
}
