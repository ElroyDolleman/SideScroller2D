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

        protected Sprite sprite;


        protected Entity()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

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
