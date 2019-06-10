using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;

namespace SideScroller2D.Code.Particles
{
    class Particle
    {
        public bool IsDead { get { return Lifetime <= 0; } }

        public Vector2 Velocity { get; private set; }

        public Sprite Sprite;

        public float Lifetime;

        public Vector2 Position;
        public Vector2 Acceleration;

        public float RotationSpeed = 0f;

        public Particle(Sprite sprite, Vector2 position, float lifetime)
        {
            Lifetime = lifetime;
            Sprite = sprite;

            Position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.Velocity = velocity;
        }

        public void SetVelocity(float speed, float direction)
        {
            Velocity = new Vector2(
                (float)Math.Sin(direction) * speed,
                (float)Math.Cos(direction) * speed
            );
        }

        public void Update()
        {
            Lifetime -= Main.DeltaTime;

            Sprite.Rotation += RotationSpeed * Main.DeltaTime;

            Velocity += Acceleration;
            Position += Velocity * Main.DeltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }
    }
}
