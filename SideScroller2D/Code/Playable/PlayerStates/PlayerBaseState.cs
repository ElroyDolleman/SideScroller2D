using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Playable.PlayerStates
{
    abstract class PlayerBaseState
    {
        private static int nextID = 0;

        public bool InAir { get; protected set; } = false;

        public readonly int ID;

        protected Player player;

        protected PlayerBaseState(Player player)
        {
            this.player = player;

            ID = nextID++;
        }

        virtual public void OnEnter() { }
        virtual public void Update() { }

        virtual public void OnCollision(AABBCollider collider)
        {
        }

        virtual public void OnCollisionResolution(CollisionResult collisionResult, List<AABBCollider> surroundingColliders)
        {
            if (collisionResult.OnLeft || collisionResult.OnRight)
            {
                player.Speed.X = 0;
            }
        }

        public static bool operator ==(PlayerBaseState state1, PlayerBaseState state2)
        {
            return state1.ID == state2.ID;
        }

        public static bool operator !=(PlayerBaseState state1, PlayerBaseState state2)
        {
            return state1.ID != state2.ID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return base.ToString().Replace("SideScroller2D.Code.Playable.PlayerStates.", "");
        }
    }
}
