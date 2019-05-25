using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.GameLogic.Player.PlayerStates
{
    class JumpState : InAirState
    {
        protected float jumpPower = 0.6f;

        public JumpState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            player.ChangeAnimation(Player.Animations.Jump);

            fallSpeed = -jumpPower;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (fallSpeed > 0)
                player.ChangeState(new FallState(player));
        }
    }
}
