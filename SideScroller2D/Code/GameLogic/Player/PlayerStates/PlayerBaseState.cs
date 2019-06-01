using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.GameLogic.Player;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    abstract class PlayerBaseState
    {
        protected Player player;

        protected PlayerBaseState(Player player)
        {
            this.player = player;
        }

        virtual public void OnEnter() { }
        virtual public void Update(GameTime gameTime) { }

        virtual public void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders) { }
    }
}
