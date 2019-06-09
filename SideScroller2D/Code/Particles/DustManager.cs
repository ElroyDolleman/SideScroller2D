using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Utilities;

namespace SideScroller2D.Code.Particles
{
    class DustManager
    {
        static List<DustEffect> activeDustEffects = new List<DustEffect>();

        public static void AddOnLandingDustEffect(Vector2 position)
        {
            activeDustEffects.Add(new DustEffect(AssetsManager.GetTexture("player_dust_particles"), position, 0, 3, new Vector2(8, 16)));
        }

        public static void AddWallJumpDustEffect(Vector2 position, int wallDirection)
        {
            var dustEffect = new DustEffect(AssetsManager.GetTexture("player_dust_particles"), position, 4, 7, new Vector2(wallDirection == 1 ? 16 : 0, 8));
            dustEffect.Animation.Sprite.SpriteEffect = wallDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            activeDustEffects.Add(dustEffect);
        }

        public static void AddWallSlideDustEffect(Vector2 position, int wallDirection)
        {
            var dustEffect = new DustEffect(AssetsManager.GetTexture("player_dust_particles"), position, 8, 11, new Vector2(wallDirection == 1 ? 16 : 0, 8));

            dustEffect.Animation.Sprite.SpriteEffect = wallDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            dustEffect.Position = position;

            activeDustEffects.Add(dustEffect);
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < activeDustEffects.Count; i++)
            {
                activeDustEffects[i].Update(gameTime);

                if (activeDustEffects[i].Animation.Done)
                {
                    activeDustEffects.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(DustEffect dustEffect in activeDustEffects)
            {
                dustEffect.Draw(spriteBatch);
            }
        }
    }
}
