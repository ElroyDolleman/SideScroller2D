using System;
using System.Timers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Particles;

namespace SideScroller2D.Code.GameLogic.Level
{
    public enum TileTypes
    {
        Empty,
        Ground,
        SemiSolid,
        Breakable
    }

    class Tile
    {
        public bool Solid { get { return TileType == TileTypes.Ground || TileType == TileTypes.Breakable; } }
        public bool CanStandOn { get { return Solid || TileType == TileTypes.SemiSolid; } }

        public readonly Rectangle Hitbox;
        public readonly Vector2 Position;

        public TileTypes TileType { get; private set; }

        public ParticleSystem ParticleSystem;

        private Sprite background;
        private Sprite overlay;
        private Sprite foreground;

        private bool hasBackground;
        private bool hasOverlay;
        private bool hasForeground;

        public Tile(Vector2 position, Sprite background = null, Sprite overlay = null, Sprite foreground = null, TileTypes tileType = TileTypes.Empty)
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
            this.TileType = tileType;

            Hitbox = new Rectangle(position.ToPoint(), new Point(16, 16));
        }

        public void Break()
        {
            TileType = TileTypes.Ground;

            hasOverlay = false;
            overlay = null;

            // TODO: Build custom timer system so it will work with frame by frame advancement
            var timer = new Timer(1000f / 60f * 8f);

            timer.Elapsed += MakeEmpty;
            timer.AutoReset = false;
            timer.Enabled = true;

            ParticleSystem.Play();
        }

        private void MakeEmpty(Object source, ElapsedEventArgs e)
        {
            TileType = TileTypes.Empty;
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
