using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Graphics;

namespace SideScroller2D.Code.Particles
{
    class FallingPiecesParticles : ParticleSystem
    {
        public FallingPiecesParticles(Sprite particleSprite, Vector2 position)
            : base(particleSprite, position)
        {
            MinAcceleration = new Vector2(0, 7.5f);
            MaxAcceleration = new Vector2(0, 9.5f);

            MinDirection = MathHelper.ToRadians(180 - 30);
            MaxDirection = MathHelper.ToRadians(180 + 30);

            MinSpeed = 130f;
            MaxSpeed = 158f;

            MinRotateSpeed = -1.25f;
            MaxRotateSpeed = 1.25f;

            MinSpawn = 4;
            MaxSpawn = 4;

            Duration = 0f;
        }
    }
}
