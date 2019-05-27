using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SideScroller2D.GameLogic.Map
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
                (int)(Position.X + Math.Floor(worldPosition.X / CellSize.X)),
                (int)(Position.Y + Math.Floor(worldPosition.Y / CellSize.Y))
            );
        }

        /// <summary>
        /// Calculates the cell number where (x: 0, y: 0) = 0
        /// </summary>
        /// <param name="cellsPerRow">The amount of cells on 1 </param>
        public int CellNumber(int x, int y, int cellsPerRow)
        {
            return y * cellsPerRow + x;
        }

        public int CellNumberToGridLocation(int cellNumber, int cellsPerRow)
        {
            int rows = (int)Math.Floor((float)cellNumber / cellsPerRow) * cellsPerRow;

            return cellNumber % cellsPerRow + rows;
        }
    }
}
