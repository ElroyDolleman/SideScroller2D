using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace SideScroller2D.Code.Audio
{
    enum GameSounds
    {
        PlayerJump,
    }

    static class AudioManager
    {
        private static Dictionary<GameSounds, SoundEffect> soundEffects;

        public static void LoadAllSounds(ContentManager content)
        {
            soundEffects = new Dictionary<GameSounds, SoundEffect>();

            soundEffects.Add(GameSounds.PlayerJump, content.Load<SoundEffect>("player_jump"));
        }

        public static void PlaySound(GameSounds sound, float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
        {
            soundEffects[sound].Play(volume, pitch, pan);
        }
    }
}
