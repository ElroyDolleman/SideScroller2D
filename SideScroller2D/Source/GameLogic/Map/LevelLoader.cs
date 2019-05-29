using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.Xna.Framework;

using SideScroller2D.Graphics;
using SideScroller2D.Managers;
using SideScroller2D.Utilities;

namespace SideScroller2D.GameLogic.Map
{
    static class LevelLoader
    {
        public static Level LoadLevel()
        {
            JObject levelJson = JsonLoader.LoadJson("test_map01.json");
            SpriteSheet tileset = new SpriteSheet(AssetsManager.GetTexture("tileset01"), 16, 16);

            var width = levelJson["width"].Value<int>();
            var height = levelJson["height"].Value<int>();
            var tilewidth = levelJson["tilewidth"].Value<int>();
            var tileheight = levelJson["tileheight"].Value<int>();

            int totalHeight = height * tileheight;
            int startPosY = Main.TargetHeight - totalHeight;

            var backgroundLayer = levelJson["layers"][0];
            var overlayLayer = levelJson["layers"][1];
            var foregroundLayer = levelJson["layers"][2];

            var backgroundArray = backgroundLayer["data"].Values<int>().ToArray();
            var overlayArray = overlayLayer["data"].Values<int>().ToArray();
            var foregroundArray = foregroundLayer["data"].Values<int>().ToArray();

            List<Tile> tiles = new List<Tile>();

            for (int y = 0; y < height; y++)
            {
                Console.WriteLine("");

                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    int overlayFrame = overlayArray[index] - 1;

                    var position = new Vector2(x * tilewidth, startPosY + y * tileheight);

                    Sprite overlaySprite = null; 
                    if (overlayFrame >= 0)
                    {
                        overlaySprite = new Sprite(tileset.Texture);
                        tileset.CropSpriteByFrame(overlaySprite, overlayFrame);
                    }

                    var tile = new Tile(position, null, overlaySprite, null);

                    tiles.Add(tile);
                }
            }

            return new Level(tiles, width, height);
        }
    }
}
