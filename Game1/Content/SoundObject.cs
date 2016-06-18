using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    class SoundObject
    {
        private SoundEffect sound;
        private SoundEffectInstance soundInstance;

        public SoundObject(SoundEffect sound)
        {
            this.sound = sound;
            this.soundInstance = sound.CreateInstance();
        }

        public void startPlaying()
        {
            soundInstance.Play();
        }

        public void stopPlaying()
        {
            soundInstance.Stop();
        }

        public static Dictionary<String,SoundObject> soundObjects;
    }
}
