using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Graphics;

namespace SideScroller2D.Collision
{
    abstract class MovableEntity
    { 
        public Vector2 Position { get { return position; } }
        public Vector2 NextPostion { get { return Position + Speed * Acceleration * Main.DeltaTime; } }

        public Rectangle Hitbox { get { return new Rectangle(Position.ToPoint() + hitbox.Location, hitbox.Size); } }
        public Rectangle NextHitbox { get { return new Rectangle(NextPostion.ToPoint() + hitbox.Location, hitbox.Size); } }

        public Vector2 Speed = Vector2.Zero;
        public Vector2 Acceleration = Vector2.One;

        protected Sprite sprite;
        protected Rectangle hitbox;
        protected Vector2 position;

        protected MovableEntity()
        {

        }

        public virtual void Update(GameTime gameTime)
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

        public virtual void ChangePosition(Vector2 position)
        {
            this.position = position;
        }

        public virtual void ChangePositionX(float newX)
        {
            this.position.X = newX;
        }

        public virtual void ChangePositionY(float newY)
        {
            this.position.Y = newY;
        }

        public virtual void OnCollision(CollisionResult collisionResult)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
    }
}
