﻿using System;
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
        float accelSpeed = 0.09f;

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
            if (InputManager.JustPressed(player.Inputs.Jump))
            {
                player.ChangeState(new JumpState(player));
            }
            else if (player.Acceleration.X == 0)
            {
                player.Speed.X = 0;
                player.ChangeState(new IdleState(player));
            }
        }
    }
}