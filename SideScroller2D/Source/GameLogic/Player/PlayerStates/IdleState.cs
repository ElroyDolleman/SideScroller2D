using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Input;

namespace SideScroller2D.GameLogic.Player.PlayerStates
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

            player.ChangeAnimation(Player.Animations.Idle);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.JustPressed(player.Inputs.Jump))
            {
                player.ChangeState(new JumpState(player));
            }
            else if (InputManager.IsDown(player.Inputs.Left) || InputManager.IsDown(player.Inputs.Right))
            {
                player.ChangeState(new RunState(player));
            }
        }
    }
}
