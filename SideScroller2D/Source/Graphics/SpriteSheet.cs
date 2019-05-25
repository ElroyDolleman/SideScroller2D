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
        public int CurrentFrame { get => currentFrame; }

        public readonly int FramesPerRow;

        public readonly int FrameWidth;
        public readonly int FrameHeight;

        public readonly Rectangle SheetArea;
        public readonly Point Spacing;

        public Sprite Sprite;

        private int currentFrame;

        public SpriteSheet(Sprite sprite, int frameWidth, int frameHeight)
            : this(sprite, frameWidth, frameHeight, Point.Zero, sprite.Bounds)
        {
        }

        public SpriteSheet(Sprite sprite, int frameWidth, int frameHeight, Point spacing)
            : this(sprite, frameWidth, frameHeight, spacing, sprite.Bounds)
        {
        }

        public SpriteSheet(Sprite sprite, int frameWidth, int frameHeight, int spacing)
            : this(sprite, frameWidth, frameHeight, new Point(spacing, spacing), sprite.Bounds)
        {
        }

        public SpriteSheet(Sprite sprite, int frameWidth, int frameHeight, int spacing, int margin)
            : this(sprite, frameWidth, frameHeight, new Point(spacing, spacing), new Rectangle(margin, margin, sprite.Texture.Width - margin * 2, sprite.Texture.Height - margin * 2))
        {
        }

        public SpriteSheet(Sprite sprite, int frameWidth, int frameHeight, int spacing, Point margin)
            : this(sprite, frameWidth, frameHeight, new Point(spacing, spacing), new Rectangle(margin.X, margin.Y, sprite.Texture.Width - margin.X * 2, sprite.Texture.Height - margin.Y * 2))
        {
        }

        public SpriteSheet(Sprite sprite, int frameWidth, int frameHeight, int spacing, Rectangle sheetArea)
            : this(sprite, frameWidth, frameHeight, new Point(spacing, spacing), sheetArea)
        {
        }

        public SpriteSheet(Sprite sprite, int frameWidth, int frameHeight, Point spacing, Rectangle sheetArea)
        {
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;

            this.SheetArea = sheetArea;
            this.Spacing = spacing;

            this.Sprite = sprite;

            FramesPerRow = SheetArea.Size.X / FrameWidth;

            SetFrame(0);
        }

        public void SetFrame(int frame)
        {
            int x = frame % FramesPerRow;
            int y = (int)Math.Floor((float)frame / FramesPerRow);

            Sprite.Crop = new Rectangle(x * FrameWidth, y * FrameHeight, FrameWidth, FrameHeight);

            currentFrame = frame;
        }

        public void SetFrame(int x, int y)
        {
            Sprite.Crop = new Rectangle(x * FrameWidth, y * FrameHeight, FrameWidth, FrameHeight);

            currentFrame = y * FramesPerRow + x;
        }
    }
}
