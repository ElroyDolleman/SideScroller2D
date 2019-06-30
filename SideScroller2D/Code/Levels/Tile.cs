using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Graphics.Particles;
using SideScroller2D.Code.Graphics;

namespace SideScroller2D.Code.Levels
{
    class Tile
    {
        public AABBCollider Collider { get; private set; }
        public bool Empty { get; private set; }

        public readonly Vector2 Position;

        public bool Visible;
        public bool AttachPositionToCollider = false;

        public ParticleSystem OnDestroyParticles;

        Sprite sprite;

        public Tile(Sprite sprite, AABBCollider collider, Vector2 position)
        {
            Visible = sprite != null;
            Empty = collider == null;

            Collider = collider;
            Position = position;
            this.sprite = sprite;

            if (Collider != null && Collider.CollisionType == AABBCollider.CollisionTypes.Breakable)
                Collider.OnBreak = Destroy;
        }

        public void Destroy()
        {
            Visible = false;
            sprite = null;
            Empty = true;
            Collider = null;

            if (OnDestroyParticles != null)
                OnDestroyParticles.Play();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            sprite.Draw(spriteBatch, AttachPositionToCollider ? Collider.Hitbox.Position : Position);
        }
    }
}
