using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Utilities.Time;

namespace SideScroller2D.Code
{
    abstract class Actor
    {
        public Vector2 Position { get; private set; }
        public Vector2 NextPostion { get { return Position + Speed * ElapsedTime.Seconds; } }

        public FloatRectangle Hitbox { get { return new FloatRectangle(Position.X, Position.Y, hitbox.Width, hitbox.Height); } protected set { hitbox = value; } }
        public FloatRectangle NextHitbox { get { return new FloatRectangle(NextPostion.X, NextPostion.Y, hitbox.Width, hitbox.Height); } }

        public Vector2 Speed;

        private FloatRectangle hitbox;

        protected Sprite sprite;

        protected Actor()
        {
            
        }

        public void SetX(float x)
        {
            Position = new Vector2(x, Position.Y);
        }

        public void SetY(float y)
        {
            Position = new Vector2(Position.X, y);
        }

        public void ChangePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void MoveX()
        {
            MoveX(Speed.X);
        }

        public void MoveY()
        {
            MoveY(Speed.Y);
        }

        public void MoveX(float amount)
        {
            SetX(Position.X + amount * ElapsedTime.Seconds);
        }

        public void MoveY(float amount)
        {
            SetY(Position.Y + amount * ElapsedTime.Seconds);
        }

        public virtual void OnCollision(AABBCollider collider)
        {

        }

        public virtual void OnCollisionResolution(CollisionResult collisionResult, List<AABBCollider> surroundingColliders)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
    }
}
