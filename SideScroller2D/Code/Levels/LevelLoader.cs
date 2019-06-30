using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Utilities;
using SideScroller2D.Code.Graphics.Particles;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Levels
{
    static class LevelLoader
    {
        public const string TestLevel01 = "test_map01.json";
        public const string TestLevel02 = "test_map02.json";
        public const string TestLevel03 = "test_map03.json";
        public const string TestLevel04 = "test_map04.json";

        public const string TilesetDefault = "tileset01.json";

        /// <summary>
        /// Creates a Level object based on the data of a json file generated from Tiled.
        /// </summary>
        /// <param name="levelName">The name of the json file that contains the level data.</param>
        /// <param name="tilesetName">The name of the json file that contains the tileset data.</param>
        public static Level LoadLevel(string levelName, string tilesetName)
        {
            JObject levelJson = JsonLoader.LoadJson(levelName);
            JObject tilesetJson = JsonLoader.LoadJson(tilesetName);

            // TODO: Get name based on tileset json
            SpriteSheet tileset = AssetsManager.GetTileset("tileset01");

            var gridSize = new Point(levelJson["width"].Value<int>(), levelJson["height"].Value<int>());
            var cellSize = new Point(levelJson["tilewidth"].Value<int>(), levelJson["tileheight"].Value<int>());

            var levelHeight = gridSize.Y * cellSize.Y;
            var position = new Vector2(0, Main.TargetHeight - levelHeight);

            var grid = new Grid(gridSize, position, cellSize);

            var backgroundLayer = levelJson["layers"][0];
            var overlayLayer = levelJson["layers"][1];
            var foregroundLayer = levelJson["layers"][2];

            var backgroundArray = backgroundLayer["data"].Values<int>().ToArray();
            var overlayArray = overlayLayer["data"].Values<int>().ToArray();
            var foregroundArray = foregroundLayer["data"].Values<int>().ToArray();

            List<Tile> tiles = new List<Tile>();


            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    int cellNumber = grid.CellNumber(x, y);

                    int overlayFrame = overlayArray[cellNumber] - 1;

                    Sprite overlaySprite = null;

                    if (overlayFrame >= 0)
                    {
                        overlaySprite = new Sprite(tileset.Texture);
                        tileset.CropSpriteByFrame(overlaySprite, overlayFrame);
                    }

                    var collisionType = GetCollisionType(tilesetJson, overlayFrame);
                    var size = collisionType == AABBCollider.CollisionTypes.SemiSolid ? new Vector2(grid.CellSize.X, 4) : grid.CellSize.ToVector2();

                    var tilePosition = grid.ToWorldPosition(x, y);
                    var hitbox = new FloatRectangle(tilePosition, size);

                    AABBCollider collider = collisionType.HasValue ? new AABBCollider(hitbox, collisionType.Value) : null;

                    var tile = new Tile(overlaySprite, collider, tilePosition);
                    tiles.Add(tile);

                    if (collisionType == AABBCollider.CollisionTypes.Breakable)
                        tile.OnDestroyParticles = new FallingPiecesParticles(new Sprite(AssetsManager.GetTexture("brick_piece")), tilePosition + (grid.CellSize.ToVector2() * 0.5f));
                }
            }


            return new Level(tiles, grid);
        }

        private static AABBCollider.CollisionTypes? GetCollisionType(JObject tilesetJson, int frame)
        {
            string type = "";

            // Find the collision type which depends on the frame of the spritesheet
            for (int i = 0; i < tilesetJson["tiles"].Count(); i++)
            {
                if (tilesetJson["tiles"][i]["id"].Value<int>() == frame)
                {
                    type = tilesetJson["tiles"][i]["type"].Value<string>();
                    break;
                }
            }

            switch (type)
            {
                case "ground":
                    return AABBCollider.CollisionTypes.Solid;
                case "semisolid":
                    return AABBCollider.CollisionTypes.SemiSolid;
                case "breakable":
                    return AABBCollider.CollisionTypes.Breakable;
            }

            return null;
        }
    }
}
