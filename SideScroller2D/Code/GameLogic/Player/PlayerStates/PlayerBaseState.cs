using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.GameLogic.Player;
using SideScroller2D.Code.GameLogic.Level;
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
        virtual public void Update() { }

        virtual public void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            if (collisionResult.Horizontal == CollisionResult.HorizontalResults.OnRight || collisionResult.Horizontal == CollisionResult.HorizontalResults.OnLeft)
            {
                player.Acceleration.X = 0;
                player.Speed.X = 0;
            }
        }
    }
}
