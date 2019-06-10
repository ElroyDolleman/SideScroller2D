﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.Code.Graphics
{
    class Sprite
    {
        public Vector2 Origin { get => origin; set => origin = value; }

        /// <summary>
        /// A rectangle that defines the area of the texture that should be showns. Shows the entire texture when null.
        /// </summary>
        public Rectangle? Crop { get => crop; set => crop = value; }

        /// <summary>
        /// A rectangle surrounding the sprite
        /// </summary>
        public Rectangle Bounds { get => Crop.HasValue ? Crop.Value : Texture.Bounds; }

        public SpriteEffects SpriteEffect { get => spriteEffect; set => spriteEffect = value; }
        public Color Color { get => color; set => color = value; }
        public float Alpha { get => color.A; set => color *= value; }

        public float Rotation { get => rotation; set => rotation = value; }
        public float Degrees { get => MathHelper.ToDegrees(rotation); set => rotation = MathHelper.ToRadians(value); }

        public float Depth { get => depth; set => depth = value; }

        public bool Visible { get; set; }

        public Texture2D Texture;


        private Vector2 origin = Vector2.Zero;

        private Vector2 scale = Vector2.One;
        private float rotation = 0f;
        
        private Rectangle? crop;
        private Color color = Color.White;
        private SpriteEffects spriteEffect;
        private float depth = 1f;


        public Sprite(Texture2D texture)
        {
            Visible = true;
            this.Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (Visible)
                spriteBatch.Draw(Texture, position, crop, color, rotation, origin, scale, SpriteEffect, depth);
        }

        public Sprite Clone()
        {
            Sprite clone = new Sprite(Texture);

            clone.Visible = Visible;

            clone.scale = scale;
            clone.origin = origin;
            clone.rotation = rotation;

            clone.crop = crop;
            clone.color = color;
            clone.spriteEffect = spriteEffect;
            clone.depth = depth;

            return clone;
        }
    }
}
