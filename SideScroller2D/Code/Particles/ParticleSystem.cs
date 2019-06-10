using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Utilities;

namespace SideScroller2D.Code.Particles
{
    class ParticleSystem
    {
        public bool Playing { get; private set; }
        public bool Done { get; private set; }
        public float Time { get; private set; }

        public Sprite ParticleSprite;

        public Vector2 Position = Vector2.Zero;

        public float MinLifetime = 3.0f;
        public float MaxLifetime = 3.0f;

        public float MaxSpeed = 100;
        public float MinSpeed = 100;

        public float MaxDirection = (float)Math.PI * 2;
        public float MinDirection = 0;

        public Vector2 MaxAcceleration = Vector2.Zero;
        public Vector2 MinAcceleration = Vector2.Zero;

        public float MaxRotateSpeed = 0f;
        public float MinRotateSpeed = 0f;

        public int MaxSpawn = 6;
        public int MinSpawn = 4;

        public float MinEmitInterval = 1.0f;
        public float MaxEmitInterval = 1.0f;

        public float Duration = 5.0f;
        public bool Loop = false;

        private List<Particle> particles;
        private float emitTimer;

        public ParticleSystem(Sprite particleSprite, Vector2 position)
        {
            particles = new List<Particle>();

            Position = position;
            ParticleSprite = particleSprite;

            Done = false;
            Time = 0;
        }

        public virtual void Play()
        {
            Playing = true;
            Done = false;
            Time = 0;

            Emit();
            emitTimer = RNGManager.RandomFloat(MinEmitInterval, MaxEmitInterval);
        }

        public virtual void Stop()
        {
            Playing = false;
        }

        public virtual void Emit()
        {
            int spawnAmount = RNGManager.RandomInt(MinSpawn, MaxSpawn);

            for (int i = 0; i < spawnAmount; i++)
            {
                float lifetime = RNGManager.RandomFloat(MinLifetime, MaxLifetime);

                var particle = new Particle(ParticleSprite.Clone(), Position, lifetime);
                particles.Add(particle);

                particle.SetVelocity(
                    RNGManager.RandomFloat(MinSpeed, MaxSpeed),
                    RNGManager.RandomFloat(MinDirection, MaxDirection)
                );
                particle.Acceleration = RNGManager.RandomVector2(MinAcceleration, MaxAcceleration);
                particle.RotationSpeed = RNGManager.RandomFloat(MinRotateSpeed, MaxRotateSpeed);
            }
        }

        public virtual void Update()
        {
            if (!Playing || Done)
                return;

            if (Loop || Time < Duration)
                Time += Main.DeltaTime;

            UpdateParticles();

            if (!Loop && Time >= Duration)
            {
                Time = Duration;

                if (particles.Count == 0)
                    Done = true;

                return;
            }

            emitTimer -= Main.DeltaTime;

            if (emitTimer <= 0)
            {
                emitTimer += RNGManager.RandomFloat(MinEmitInterval, MaxEmitInterval);

                Emit();
            }
        }

        protected virtual void UpdateParticles()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();

                if (particles[i].IsDead)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in particles)
                particle.Draw(spriteBatch);
        }
    }
}
