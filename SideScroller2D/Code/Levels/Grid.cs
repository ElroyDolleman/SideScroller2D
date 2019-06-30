using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SideScroller2D.Code.Levels
{
    class Grid
    {
        public int Width { get { return Dimensions.X; } }
        public int Height { get { return Dimensions.Y; } }
        public int TotalCells { get { return Width * Height; } }
        public Point LastCell { get { return new Point(Width - 1, Height - 1); } }

        public readonly Point Dimensions;
        public readonly Point CellSize;
        public readonly Vector2 Position;

        public Grid(Point size)
            : this(size, Vector2.Zero)
        {
        }

        public Grid(Point size, Vector2 position)
            : this(size, Vector2.Zero, new Point(16, 16))
        {
        }

        public Grid(Point size, Vector2 position, Point cellSize)
        {
            Position = position;
            Dimensions = size;
            CellSize = cellSize;
        }

        public Vector2 ToWorldPosition(int x, int y)
        {
            return Position + new Vector2(
                x * CellSize.X,
                y * CellSize.Y
            );
        }

        public Vector2 ToWorldPosition(Point gridLocation)
        {
            return ToWorldPosition(gridLocation.X, gridLocation.Y);
        }

        public Point ToGridLocation(float worldPositionX, float worldPositionY)
        {
            return new Point(
                (int)Math.Floor((worldPositionX - Position.X) / CellSize.X),
                (int)Math.Floor((worldPositionY - Position.Y) / CellSize.Y)
            );
        }

        public Point ToGridLocation(Vector2 worldPosition)
        {
            return ToGridLocation(worldPosition.X, worldPosition.Y);
        }

        /// <summary>
        /// Calculates the cell number where (x: 0, y: 0) = 0. Returns -1 when the cell is invalid.
        /// </summary>
        public int CellNumber(int x, int y)
        {
            if (x >= Width || x < 0 || y < 0)
                return -1;

            return y * Width + x;
        }

        public int CellNumberToGridLocation(int cellNumber)
        {
            int rows = (int)Math.Floor((float)cellNumber / Width) * Width;

            return cellNumber % Width + rows;
        }
    }
}
