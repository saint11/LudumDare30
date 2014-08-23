using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class MusicPlayer
    {
        private static Music music;
        private static string playing;

        public static void PlayMusic(string source)
        {
            if (playing != source)
            {
                playing = source;
                if (music != null) music.Stop();

                music = new Music(Bones.GAME_PATH + "Music/" + source + ".ogg");

                music.Play();
            }
        }
    }
}
