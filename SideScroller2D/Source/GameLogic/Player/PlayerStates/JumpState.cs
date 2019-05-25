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
        protected float jumpPower = 6.44f;
        protected float slowGravity = 0.017f;

        public JumpState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            player.ChangeAnimation(Player.Animations.Jump);

            player.SetYSpeed(-jumpPower);

            AudioManager.PlaySound(GameSounds.PlayerJump);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (player.Speed.Y > 0)
                player.ChangeState(new FallState(player));
        }

        public override float GetGravity()
        {
            if (InputManager.IsDown(player.Inputs.Jump))
                return slowGravity;

            slowGravity = defaultGravity;

            return base.GetGravity();
        }
    }
}
