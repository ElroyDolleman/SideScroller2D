using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Input;

namespace SideScroller2D.GameLogic.Player.PlayerStates
{
    class RunState : OnGroundState
    {
        protected float runSpeed = 2.0f;

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
            if (!InputManager.IsDown(player.Inputs.Right) && !InputManager.IsDown(player.Inputs.Left))
            {
                player.ChangeState(new IdleState(player));
            }

            base.Update(gameTime);

            if (InputManager.IsDown(player.Inputs.Right))
                player.SetXSpeed(runSpeed);

            else if (InputManager.IsDown(player.Inputs.Left))
                player.SetXSpeed(-runSpeed);

            else
                player.SetXSpeed(0);
        }
    }
}
