using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.Graphics
{
    class SpriteSheetAnimation
    {
        public SpriteSheet SpriteSheet;
        public bool Paused = false;

        float timer = 0;
        int[] frames;
        int currentFrameIndex = 0;

        public SpriteSheetAnimation(SpriteSheet spriteSheet, int[] frames)
        {
            this.SpriteSheet = spriteSheet;
            this.frames = frames;
        }

        public SpriteSheetAnimation(SpriteSheet spriteSheet, int startFrame = 0, int endFrame = 0)
        {
            this.SpriteSheet = spriteSheet;

            int length = endFrame + 1 - startFrame;
            frames = new int[length];

            for (int i = 0; i < length; i++)
                frames[i] = startFrame + i;
        }

        public void Update(float elapsedMilliseconds)
        {
            if (Paused)
                return;

            timer += elapsedMilliseconds;

            if (timer < 0)
                return;

            timer -= elapsedMilliseconds;

            if (currentFrameIndex >= frames.Length)
                currentFrameIndex = 0;

            SpriteSheet.SetFrame(frames[currentFrameIndex]);
        }
    }
}
