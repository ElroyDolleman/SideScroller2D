using System;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Collision
{
    struct Circle
    {
        public float Diameter { get { return Radius * 2; } set { Radius = value / 2; } }

        public float X { get { return Position.X; } set { Position.X = value; } }
        public float Y { get { return Position.Y; } set { Position.Y = value; } }

        public float Radius;
        public Vector2 Position;

        public bool Intersects(Circle otherCircle)
        {
            return Vector2.Distance(this.Position, otherCircle.Position) < this.Radius + otherCircle.Radius;
        }
    }
}
