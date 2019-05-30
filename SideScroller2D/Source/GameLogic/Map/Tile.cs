using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Graphics;

namespace SideScroller2D.GameLogic.Map
{
    class Tile
    {
        public readonly Rectangle Hitbox;

        public readonly Vector2 Position;
        public readonly bool Solid;

        private Sprite background;
        private Sprite overlay;
        private Sprite foreground;

        private bool hasBackground;
        private bool hasOverlay;
        private bool hasForeground;

        public Tile(Vector2 position, Sprite background = null, Sprite overlay = null, Sprite foreground = null, bool solid = false)
        {
            hasBackground = background != null;
            hasOverlay = overlay != null;
            hasForeground = foreground != null;

            if (hasBackground)
                this.background = background;
            if (hasOverlay)
                this.overlay = overlay;
            if (hasForeground)
                this.foreground = foreground;

            this.Position = position;
            this.Solid = solid;

            Hitbox = new Rectangle(position.ToPoint(), new Point(16, 16));
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            if (hasBackground)
                background.Draw(spriteBatch, Position);
            if (hasOverlay)
                overlay.Draw(spriteBatch, Position);
        }

        public void DrawForeground(SpriteBatch spriteBatch)
        {
            if (hasForeground)
                foreground.Draw(spriteBatch, Position);
        }

        public void DrawFilledTile(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.FillRectangle(Hitbox, color);
        }
    }
}
