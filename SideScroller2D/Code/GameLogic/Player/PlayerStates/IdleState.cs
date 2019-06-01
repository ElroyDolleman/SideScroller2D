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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.JustPressed(player.Inputs.Jump))
            {
                player.ChangeState(new JumpState(player));
            }
            else if (player.Acceleration.X != 0 && player.Speed.X != 0)
            {
                player.ChangeState(new RunState(player));
            }
        }
    }
}
