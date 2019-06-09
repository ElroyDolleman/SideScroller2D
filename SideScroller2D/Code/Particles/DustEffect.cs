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

        public SpriteSheetAnimation Animation { get; private set; }

        public DustEffect(Texture2D texture, Vector2 position, int startFrame, int endFrame, Vector2 origin, int frameWidth = 16, int frameHeight = 16)
        {
            sprite = new Sprite(texture);
            sprite.Origin = origin;

            spriteSheet = new SpriteSheet(sprite.Texture, frameWidth, frameHeight);
            Animation = new SpriteSheetAnimation(sprite, spriteSheet, startFrame, endFrame, 70.0f);

            spriteSheet.CropSpriteByFrame(sprite, startFrame);

            Animation.Loop = false;

            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            Animation.Update(Main.DeltaTimeMiliseconds);

            if (Animation.Done && sprite.Visible)
                sprite.Visible = false;
        }

        public void Reset()
        {
            sprite.Visible = true;
            Animation.ResetAnimation();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
