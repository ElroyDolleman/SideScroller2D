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
        protected const float maxFallspeed = 5.0f;
        protected float defaultGravity = 0.04f;

        protected float airSpeed = 2.0f;
        protected float airResitance = 0.1f;

        public InAirState(Player player)
            : base(player)
        {
        }

        public override void OnEnter()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            float xSpeed = 0;

            if (player.Speed.X < airSpeed && InputManager.IsDown(player.Inputs.Right))
                xSpeed = airSpeed * airResitance;

            else if (player.Speed.X > -airSpeed && InputManager.IsDown(player.Inputs.Left))
                xSpeed = -airSpeed * airResitance;

            player.AddSpeed(xSpeed, GetGravity() * gameTime.ElapsedGameTime.Milliseconds);

            if (player.Position.Y > 300-16)
            {
                player.Position = new Vector2(player.Position.X, 300-16);

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
