using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Utilities;

namespace SideScroller2D.Code.Graphics.Particles
{
    class DustParticles : ParticleSystem
    {
        public DustParticles(Vector2 position)
            : base(Sprite.CreatePixel(Color.White), position)
        {

        }

        public void PlayLand()
        {
            MinDirection = MathHelper.ToRadians(180);
            MaxDirection = MathHelper.ToRadians(180);

            MinSpeed = 5f;
            MaxSpeed = 6f;

            MinSpawn = 48;
            MaxSpawn = 64;

            MinLifetime = 0.1f;
            MaxLifetime = 0.36f;

            MinOffset = new Vector2(-5, -4);
            MaxOffset = new Vector2(5, 0);

            Duration = 0f;

            Play();
        }

        public void PlayJump()
        {
            MinDirection = MathHelper.ToRadians(180);
            MaxDirection = MathHelper.ToRadians(180);

            MinSpeed = 14f;
            MaxSpeed = 18f;

            MinSpawn = 26;
            MaxSpawn = 36;

            MinLifetime = 0.16f;
            MaxLifetime = 0.38f;

            MinOffset = new Vector2(-3, -5f);
            MaxOffset = new Vector2(3, 0);

            Duration = 0f;

            Play();
        }

        public void PlayWallSlide(int wallDirection)
        {
            float dir = wallDirection == 1 ? 270 - 45 : 90 + 45;
            MinDirection = MathHelper.ToRadians(dir - 8f);
            MaxDirection = MathHelper.ToRadians(dir + 8f);

            MinSpeed = 8f;
            MaxSpeed = 9f;

            MinSpawn = 18;
            MaxSpawn = 24;

            MinLifetime = 0.2f;
            MaxLifetime = 0.25f;

            MinOffset = new Vector2(wallDirection == 1 ? 0 : -3, -1f);
            MaxOffset = new Vector2(wallDirection == -1 ? 0 : 3, 3);

            Duration = 0f;

            Play();
        }

        public void PlayWallJump(int wallDirection)
        {
            float dir = wallDirection == 1 ? 270 - 50 : 90 + 50;
            MinDirection = MathHelper.ToRadians(dir - 8f);
            MaxDirection = MathHelper.ToRadians(dir + 8f);

            MinSpeed = 14f;
            MaxSpeed = 17f;

            MinSpawn = 22;
            MaxSpawn = 28;

            MinLifetime = 0.21f;
            MaxLifetime = 0.26f;

            MinOffset = new Vector2(wallDirection == 1 ? 0 : -3, -5f);
            MaxOffset = new Vector2(wallDirection == -1 ? 0 : 3, 3);

            Duration = 0f;

            Play();
        }
    }
}
