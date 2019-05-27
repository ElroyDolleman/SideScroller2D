using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SideScroller2D.Utilities
{
    class JsonLoader
    {
        public static string RootDirectory { get; private set; }

        public static void SetRootDirectory(string rootDirectory)
        {
            JsonLoader.RootDirectory = rootDirectory;
        }

        public static JObject LoadJson(string jsonFile)
        {
            StreamReader file = File.OpenText(RootDirectory + "\\" + jsonFile);
            JsonTextReader reader = new JsonTextReader(file);

            return (JObject)JToken.ReadFrom(reader);
        }
    }
}
