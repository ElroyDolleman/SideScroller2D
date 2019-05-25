using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.GameLogic.Player.PlayerStates
{
    class InAirState : PlayerBaseState
    {
        protected const float maxFallspeed = 2.0f;
        protected const float gravity = 0.04f;

        protected float fallSpeed = 0f;

        public InAirState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            fallSpeed = 0;
        }

        public override void Update(GameTime gameTime)
        {
            fallSpeed = Math.Min(fallSpeed + gravity, maxFallspeed);

            Console.WriteLine("FallSpeed {0}", fallSpeed);
            player.Move(0, fallSpeed * gameTime.ElapsedGameTime.Milliseconds);

            if (player.Position.Y > 300-16)
            {
                player.Position = new Vector2(player.Position.X, 300-16);
                player.ChangeState(new IdleState(player));
            }
        }
    }
}
