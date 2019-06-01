using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;

namespace SideScroller2D.Code.Utilities
{
    static class AssetsManager
    {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteSheet> tilesets = new Dictionary<string, SpriteSheet>();

        public static void LoadTexture2D(ContentManager content, string key)
        {
            textures.Add(key, content.Load<Texture2D>(key));
        }

        /// <summary>
        /// Loads a tileset that was exported through Tiled Map Editor
        /// </summary>
        /// <param name="jsonFile">The file name of the json containing the correct tileset information. (.json extension must be included)</param>
        /// <param name="key">The name to use to retrieve the tileset later</param>
        public static void LoadTileset(ContentManager content, string jsonFile, string key)
        {
            JObject json = JsonLoader.LoadJson(jsonFile);

            int width = json["tilewidth"].Value<int>();
            int height = json["tileheight"].Value<int>();
            string textureFile = json["image"].Value<string>();

            textureFile = Path.GetFileNameWithoutExtension(textureFile);

            tilesets.Add(key, new SpriteSheet(content.Load<Texture2D>(textureFile), width, height));
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

        public static SpriteSheet GetTileset(string key)
        {
#if DEBUG
            if (!tilesets.ContainsKey(key))
            {
                Console.WriteLine("Warning: There is no tileset loaded with the key {0}", key);
                return null;
            }
#endif
            return tilesets[key];
        }
    }
}
