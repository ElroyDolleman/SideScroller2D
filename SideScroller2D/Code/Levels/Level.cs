using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.Levels
{
    class Level
    {
        public readonly Grid Grid;

        private List<Tile> tiles;

        public Level(List<Tile> tiles, Grid grid)
        {
            Grid = grid;
            this.tiles = tiles;
        }

        public List<AABBCollider> GetColliders(Point from, Point to)
        {
            var colliders = new List<AABBCollider>();

            for (int y = from.Y; y <= to.Y; y++)
            {
                for (int x = from.X; x <= to.X; x++)
                {
                    var tile = GetTile(x, y);

                    if (tile != null && !tile.Empty)
                        colliders.Add(tile.Collider);
                }
            }

            return colliders;
        }

        public Tile GetTile(int x, int y)
        {
            int index = Grid.CellNumber(x, y);

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

        public List<Tile> GetTiles(Point from, Point to)
        {
            var tiles = new List<Tile>();

            for (int y = from.Y; y <= to.Y; y++)
                for (int x = from.X; x <= to.X; x++)
                    tiles.Add(GetTile(x, y));

            return tiles;
        }

        public void DrawTiles(SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch, new Point(0, 0), Grid.LastCell);
        }

        public void DrawTiles(SpriteBatch spriteBatch, Point from, Point to)
        {
            for (int y = from.Y; y <= to.Y; y++)
            {
                for (int x = from.X; x <= to.X; x++)
                {
                    GetTile(x, y).Draw(spriteBatch);
                }
            }
        }
    }
}
