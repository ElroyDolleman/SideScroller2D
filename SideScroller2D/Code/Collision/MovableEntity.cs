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

        public Rectangle Hitbox { get { return new Rectangle((int)Math.Round(Position.X) + hitbox.Location.X, (int)Math.Round(Position.Y) + hitbox.Location.Y, hitbox.Width, hitbox.Height); } }
        public Rectangle NextHitbox { get { return new Rectangle((int)Math.Round(NextPostion.X) + hitbox.Location.X, (int)Math.Round(NextPostion.Y) + hitbox.Location.Y, hitbox.Width, hitbox.Height); } }

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
