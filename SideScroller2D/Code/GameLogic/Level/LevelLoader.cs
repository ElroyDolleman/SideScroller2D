using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Utilities;

namespace SideScroller2D.Code.GameLogic.Level
{
    static class LevelLoader
    {
        public static Level LoadLevel()
        {
            JObject levelJson = JsonLoader.LoadJson("test_map02.json");
            JObject tilesetJson = JsonLoader.LoadJson("tileset01.json");

            SpriteSheet tileset = AssetsManager.GetTileset("tileset01");

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
                    bool solid = false;

                    if (overlayFrame >= 0)
                    {
                        for (int i = 0; i < tilesetJson["tiles"].Count(); i++)
                        {
                            int id = tilesetJson["tiles"][i]["id"].Value<int>();

                            if (id != overlayFrame)
                                continue;

                            solid = tilesetJson["tiles"][i]["properties"][0]["value"].Value<bool>();
                        }

                        overlaySprite = new Sprite(tileset.Texture);
                        tileset.CropSpriteByFrame(overlaySprite, overlayFrame);
                    }

                    var tile = new Tile(position, null, overlaySprite, null, solid);

                    tiles.Add(tile);
                }
            }

            return new Level(tiles, width, height, new Vector2(0, startPosY));
        }
    }
}
