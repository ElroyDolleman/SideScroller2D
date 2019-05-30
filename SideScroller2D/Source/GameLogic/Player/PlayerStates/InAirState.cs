using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Input;

namespace SideScroller2D.GameLogic.Player.PlayerStates
{
    class InAirState : PlayerBaseState
    {
        protected const float maxFallspeed = 500f;
        protected const float defaultGravity = 30f;

        bool movesRight { get { return player.Speed.X > 0; } }

        public InAirState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            player.Speed += new Vector2(0, GetGravity());

            if (player.Position.Y > 300-16)
            {
                //player.Position = new Vector2(player.Position.X, 300-16);

                if (player.Speed.X == 0)
                    player.ChangeState(new IdleState(player));

                else
                    player.ChangeState(new RunState(player));
            }
        }

        public virtual float GetGravity()
        {
            if (player.Speed.Y > maxFallspeed)
                return 0;

            return defaultGravity;
        }
    }
}
