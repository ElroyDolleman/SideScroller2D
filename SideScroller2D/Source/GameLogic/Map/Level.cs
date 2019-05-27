using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

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

        void Create()
        {
            tiles = new List<Tile>();

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    
                }
            }
        }
    }
}
