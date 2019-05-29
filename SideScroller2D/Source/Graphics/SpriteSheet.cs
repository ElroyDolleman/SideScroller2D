using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.Graphics
{
    class SpriteSheet
    {
        public int CurrentFrame { get; private set; }

        public readonly int FramesPerRow;

        public readonly int FrameWidth;
        public readonly int FrameHeight;

        public readonly Rectangle SheetArea;
        public readonly Point Spacing;

        public readonly Texture2D Texture;

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight)
            : this(texture, frameWidth, frameHeight, Point.Zero, texture.Bounds)
        {
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, Point spacing)
            : this(texture, frameWidth, frameHeight, spacing, texture.Bounds)
        {
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, int spacing)
            : this(texture, frameWidth, frameHeight, new Point(spacing, spacing), texture.Bounds)
        {
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, int spacing, int margin)
            : this(texture, frameWidth, frameHeight, new Point(spacing, spacing), new Rectangle(margin, margin, texture.Width - margin * 2, texture.Height - margin * 2))
        {
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, int spacing, Point margin)
            : this(texture, frameWidth, frameHeight, new Point(spacing, spacing), new Rectangle(margin.X, margin.Y, texture.Width - margin.X * 2, texture.Height - margin.Y * 2))
        {
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, int spacing, Rectangle sheetArea)
            : this(texture, frameWidth, frameHeight, new Point(spacing, spacing), sheetArea)
        {
        }

        public SpriteSheet(Texture2D texture, int frameWidth, int frameHeight, Point spacing, Rectangle sheetArea)
        {
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;

            this.SheetArea = sheetArea;
            this.Spacing = spacing;

            this.Texture = texture;

            FramesPerRow = SheetArea.Size.X / FrameWidth;
        }

        public void CropSpriteByFrame(Sprite sprite, int frame)
        {
            int x = frame % FramesPerRow;
            int y = (int)Math.Floor((float)frame / FramesPerRow);

            sprite.Crop = new Rectangle(x * FrameWidth, y * FrameHeight, FrameWidth, FrameHeight);
        }

        //public void CropSpriteByTileLocation(Sprite sprite, int x, int y)
        //{
        //    sprite.Crop = new Rectangle(x * FrameWidth, y * FrameHeight, FrameWidth, FrameHeight);
        //}
    }
}
