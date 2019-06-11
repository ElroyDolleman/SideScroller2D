using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class IdleState : OnGroundState
    {
        public IdleState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            player.Speed.X = 0;

            player.ChangeAnimation(Player.Animations.Idle);
        }

        public override void Update()
        {
            base.Update();

            if (player.CurrentState != this)
                return;

            if (player.Acceleration.X != 0 && player.Speed.X != 0)
            {
                player.ChangeState(player.RunState);
            }
        }
    }
}
