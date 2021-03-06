﻿using System;

namespace SideScroller2D.Code.Graphics
{
    class SpriteSheetAnimation
    {
        /// <summary>
        /// The time it takes to get to the next frame in milliseconds
        /// </summary>
        public float Interval { get => interval; set => interval = value; }

        /// <summary>
        /// The amount of frames that are played per second
        /// </summary>
        public float FrameRate
        {
            get { return interval * frames.Length / 1000f; }
            set { interval = value * 1000f / frames.Length; }
        }

        public int CurrentFrame { get { return frames[currentFrameIndex]; } }

        public SpriteSheet SpriteSheet;
        public Sprite Sprite;

        public bool Paused = false;
        public bool Loop = true;
        public bool Done = false;

        private float interval;
        private float timer = 0;
        private int[] frames;
        private int currentFrameIndex = 0;

        public SpriteSheetAnimation(Sprite sprite, SpriteSheet spriteSheet, int[] frames, float interval = 160.0f)
        {
            this.SpriteSheet = spriteSheet;
            this.Sprite = sprite;
            this.frames = frames;
            this.interval = interval;
        }

        public SpriteSheetAnimation(Sprite sprite, SpriteSheet spriteSheet, int startFrame = 0, int endFrame = 0, float interval = 160.0f)
        {
            this.SpriteSheet = spriteSheet;
            this.Sprite = sprite;
            this.interval = interval;

            int length = endFrame + 1 - startFrame;
            frames = new int[length];

            for (int i = 0; i < length; i++)
                frames[i] = startFrame + i;
        }

        public void Update(float elapsedMilliseconds)
        {
            if (Paused || Done || frames.Length <= 1)
                return;

            timer += elapsedMilliseconds;

            if (timer < interval)
                return;

            if (!Loop && currentFrameIndex == frames.Length - 1)
            {
                timer = interval;
                Done = true;
                return;
            }

            timer -= interval;
            currentFrameIndex++;

            if (currentFrameIndex >= frames.Length)
                currentFrameIndex = 0;

            SpriteSheet.CropSpriteByFrame(Sprite, frames[currentFrameIndex]);
        }

        /// <summary>
        /// Goes back to the first frame
        /// </summary>
        public void ResetAnimation()
        {
            Done = false;
            timer = 0;
            currentFrameIndex = 0;
            SpriteSheet.CropSpriteByFrame(Sprite, frames[0]);
        }
    }
}
