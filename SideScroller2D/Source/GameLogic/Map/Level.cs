using System;
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

        public Level(List<Tile> tiles, int sizeX, int sizeY)
            : this(tiles, new Point(sizeX, sizeY))
        {
        }

        public Level(List<Tile> tiles, Point size)
        {
            Grid = new Grid();

            this.Size = size;
            this.tiles = tiles;
        }

        public Tile GetTile(int x, int y)
        {
            int index = Grid.CellNumber(x, y, Size.X);

            if (index >= tiles.Count)
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
                }
            }
        }
    }
}
