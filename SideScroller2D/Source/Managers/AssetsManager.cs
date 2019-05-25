using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.Managers
{
    static class AssetsManager
    {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static void LoadTexture2D(ContentManager contentManager, string key)
        {
            textures.Add(key, contentManager.Load<Texture2D>(key));
        }

        public static Texture2D GetTexture(string key)
        {
#if DEBUG
            if (!textures.ContainsKey(key))
            {
                Console.WriteLine("Warning: There is no texture loaded with the key {0}", key);
                return null;
            }
#endif
            return textures[key];
        }
    }
}
