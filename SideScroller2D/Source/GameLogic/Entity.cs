using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Graphics;

namespace SideScroller2D
{
    abstract class Entity
    {
        public Vector2 Position { get; set; }

        public Vector2 Speed = Vector2.Zero;// { get; set; }
        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = new Vector2(MathHelper.Clamp(value.X, 0, 1), MathHelper.Clamp(value.Y, 0, 1)); }
        }

        protected Sprite sprite;

        private Vector2 acceleration = Vector2.One;


        protected Entity()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            Move(Speed * Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public virtual void Move(float x, float y)
        {
            Move(new Vector2(x, y));
        }

        public virtual void Move(Vector2 amount)
        {
            Position += amount;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
    }
}
