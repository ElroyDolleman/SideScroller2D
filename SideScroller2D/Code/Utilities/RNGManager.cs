using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Code.Utilities
{
    static class RNGManager
    {
        static Random random;

        public static void Initialize()
        {
            random = new Random();
        }

        public static void Initialize(int seed)
        {
            random = new Random(seed);
        }

        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static float RandomFloat(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        public static Vector2 RandomVector2(Vector2 min, Vector2 max)
        {
            return new Vector2(RandomFloat(min.X, max.X), RandomFloat(min.Y, max.Y));
        }
    }
}
