using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.Code.Graphics.Particles
{
    static class ParticleSystemsManager
    {
        private static List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        public static void AddSystem(ParticleSystem particleSystem)
        {
            particleSystems.Add(particleSystem);
        }

        public static void RemoveSystem(ParticleSystem particleSystem)
        {
            particleSystems.Remove(particleSystem);
        }

        public static void Update()
        {
            for (int i = 0; i < particleSystems.Count; i++)
            {
                particleSystems[i].UpdateParticles();
            }
        }

        public static void UpdateAndDraw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particleSystems.Count; i++)
            {
                particleSystems[i].UpdateAndDraw(spriteBatch);
            }
        }
    }
}
