using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;

namespace SideScroller2D.Code.Collision
{
    abstract class MovableEntity : Entity
    { 
        public Vector2 NextPostion { get { return Position + Speed * Acceleration * Main.DeltaTime; } }

        public Rectangle Hitbox { get { return new Rectangle(Position.ToPoint() + hitbox.Location, hitbox.Size); } }
        public Rectangle NextHitbox { get { return new Rectangle(NextPostion.ToPoint() + hitbox.Location, hitbox.Size); } }

        public Vector2 Speed = Vector2.Zero;
        public Vector2 Acceleration = Vector2.One;

        protected Rectangle hitbox;

        protected MovableEntity()
        {

        }

        public virtual void UpdateHorizontalMovement()
        {
            position.X = NextPostion.X;
        }

        public virtual void UpdateVerticalMovement()
        {
            position.Y = NextPostion.Y;
        }

        public virtual void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            
        }
    }
}
