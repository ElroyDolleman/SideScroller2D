﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;

namespace SideScroller2D.Code
{
    abstract class Entity
    {
        public Vector2 Position { get { return position; } set { position = value; } }

        protected Sprite sprite;
        protected Vector2 position;

        protected Entity()
        {

        }

        public virtual void ChangePositionX(float newX)
        {
            this.position.X = newX;
        }

        public virtual void ChangePositionY(float newY)
        {
            this.position.Y = newY;
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
    }
}
