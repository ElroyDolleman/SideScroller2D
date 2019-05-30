﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.GameLogic.Map
{
    class Level
    {
        public Grid Grid { get; private set; }
        public Point Size { get; private set; }

        public int TotalTiles { get { return Size.X * Size.Y; } }

        List<Tile> tiles;

        public Level(List<Tile> tiles, int sizeX, int sizeY, Vector2 position)
            : this(tiles, new Point(sizeX, sizeY), position)
        {
        }

        public Level(List<Tile> tiles, Point size, Vector2 position)
        {
            Grid = new Grid(position);

            this.Size = size;
            this.tiles = tiles;
        }

        public List<Tile> GetSolidTiles(Point from, Point to)
        {
            var ouput = new List<Tile>();

            for (int y = from.Y; y <= to.Y; y++)
            {
                for (int x = from.X; x <= to.X; x++)
                {
                    var tile = GetTile(x, y);

                    if (tile.Solid)
                        ouput.Add(tile);
                }
            }

            return ouput;
        }

        public Tile GetTile(int x, int y)
        {
            int index = Grid.CellNumber(x, y, Size.X);

            if (index >= tiles.Count || index < 0)
                return null;

            return tiles[index];
        }

        public Tile GetTile(Point gridPosition)
        {
            return GetTile(gridPosition.X, gridPosition.Y);
        }

        public Tile GetTileByPosition(Vector2 worldPosition)
        {
            return GetTile(Grid.ToGridLocation(worldPosition));
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            // TODO: Make sure to only draw tiles on the screen
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    int index = Grid.CellNumber(x, y, Size.X);

                    tiles[index].DrawBackground(spriteBatch);
                }
            }
        }

        public void DrawForeground(SpriteBatch spriteBatch)
        {
            // TODO: Make sure to only draw tiles on the screen
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    int index = Grid.CellNumber(x, y, Size.X);

                    tiles[index].DrawForeground(spriteBatch);

                    if (x == 2 && y == 17)
                    {
                        tiles[index].DrawFilledTile(spriteBatch, Color.Green * 0.45f);
                        var test = Grid.ToWorldPosition(x, y);
                        var test2 = Grid.ToGridLocation(test);

                        int index2 = Grid.CellNumber(test2.X, test2.Y, Size.X);

                        tiles[index2].DrawFilledTile(spriteBatch, Color.Green * 0.45f);
                    }
                }
            }
        }
    }
}
