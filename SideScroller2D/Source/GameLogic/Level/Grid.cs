using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SideScroller2D.GameLogic.Level
{
    class Grid
    {
        public static Point CellSize { get { return new Point(16, 16); } }

        public Vector2 Position { get; private set; }

        public Grid()
            : this(Vector2.Zero)
        {
        }

        public Grid(Vector2 position)
        {
            this.Position = position;
        }

        public Vector2 ToWorldPosition(int x, int y)
        {
            return ToWorldPosition(new Point(x, y));
        }

        public Vector2 ToWorldPosition(Point gridLocation)
        {
            return Position + new Vector2(
                gridLocation.X * CellSize.X,
                gridLocation.Y * CellSize.Y
            );
        }

        public Point ToGridLocation(Vector2 worldPosition)
        {
            return new Point(
                (int)Math.Floor((worldPosition.X - Position.X) / CellSize.X),
                (int)Math.Floor((worldPosition.Y - Position.Y) / CellSize.Y)
            );
        }

        /// <summary>
        /// Calculates the cell number where (x: 0, y: 0) = 0
        /// </summary>
        /// <param name="cellsPerRow">The amount of cells on 1 row</param>
        public int CellNumber(int x, int y, int cellsPerRow)
        {
            if (x >= cellsPerRow || x < 0 || y < 0)
                return -1;

            return y * cellsPerRow + x;
        }

        public int CellNumberToGridLocation(int cellNumber, int cellsPerRow)
        {
            int rows = (int)Math.Floor((float)cellNumber / cellsPerRow) * cellsPerRow;

            return cellNumber % cellsPerRow + rows;
        }
    }
}
