using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Input;
using SideScroller2D.Audio;

namespace SideScroller2D.GameLogic.Player.PlayerStates
{
    class JumpState : InAirState
    {
        protected float jumpPower = 0.44f;

        public JumpState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            player.ChangeAnimation(Player.Animations.Jump);

            fallSpeed = -jumpPower;
            gravity = 0.018f;

            AudioManager.PlaySound(GameSounds.PlayerJump);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.JustReleased(player.Inputs.Jump))
            {
                gravity = 0.04f; // TODO: Get rid of magic number. Need to rethink how gravity should be applied.
            }

            base.Update(gameTime);

            if (fallSpeed > 0)
                player.ChangeState(new FallState(player));
        }
    }
}
