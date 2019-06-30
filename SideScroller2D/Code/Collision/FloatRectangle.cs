using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Code.Collision
{
    struct FloatRectangle
    {
        public static Rectangle IntRectangle { get { return new Rectangle(); } }
        public static FloatRectangle Empty { get { return new FloatRectangle(0, 0, 0, 0); } }

        public float Top { get { return Y; } }
        public float Left { get { return X; } }
        public float Right { get { return X + Width; } }
        public float Bottom { get { return Y + Height; } }

        public Vector2 Position { get { return new Vector2(X, Y); } }
        public Vector2 Center { get { return new Vector2(X + Width / 2, Y + Height / 2); } }
        public Vector2 Size { get { return new Vector2(Width, Height); } }

        public float X;
        public float Y;
        public float Width;
        public float Height;

        public FloatRectangle(Vector2 position, Vector2 size)
            : this(position.X, position.Y, size.X, size.Y)
        {
        }

        public FloatRectangle(Rectangle intRect)
            : this(intRect.X, intRect.Y, intRect.Width, intRect.Height)
        {
        }

        public FloatRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Intersects(FloatRectangle value)
        {
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }

        public bool Contains(Vector2 point)
        {
            return Contains(point.X, point.Y);
        }

        public bool Contains(float x, float y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        public void Offset(float offsetX, float offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        public static bool operator ==(FloatRectangle a, FloatRectangle b)
        {
            return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(FloatRectangle a, FloatRectangle b)
        {
            return a != b;
        }
    }
}
