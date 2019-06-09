using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.GameLogic;
using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Utilities;

namespace SideScroller2D.Code.Particles
{
    class DustEffect : Entity
    {
        SpriteSheet spriteSheet;
        SpriteSheetAnimation animation;

        public DustEffect()
        {
            sprite = new Sprite(AssetsManager.GetTexture("smoke_effect"));
            sprite.Origin = new Vector2(8, 16);

            spriteSheet = new SpriteSheet(sprite.Texture, 16, 16);
            animation = new SpriteSheetAnimation(sprite, spriteSheet, 0, 4, 80.0f);

            animation.Loop = false;
        }

        public override void Update(GameTime gameTime)
        {
            animation.Update(Main.DeltaTimeMiliseconds);

            if (animation.Done && sprite.Visible)
                sprite.Visible = false;
        }

        public void Reset()
        {
            sprite.Visible = true;
            animation.ResetAnimation();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
