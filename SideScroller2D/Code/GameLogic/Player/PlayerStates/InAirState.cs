using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class InAirState : PlayerBaseState
    {
        protected float maxFallspeed = 280f;
        protected float defaultGravity = 26f;

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
            if (player.Speed.Y < maxFallspeed)
                player.Speed += new Vector2(0, GetGravity());

            else if (player.Speed.Y > maxFallspeed)
                player.Speed.Y = maxFallspeed;
        }

        public virtual float GetGravity()
        {
            return defaultGravity;
        }

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            base.OnCollision(collisionResult, colliders);

            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnBottom)
            {
                Land();
            }
        }

        protected void Land()
        {
            player.Speed.Y = 0;

            if (player.Speed.X == 0)
                player.ChangeState(player.IdleState);

            else
                player.ChangeState(player.RunState);
        }
    }
}
