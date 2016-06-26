using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    public class SoundObject
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

        public void Waste()
        {
            sound.Dispose();
            soundInstance.Dispose();
        }

        public void setLooped(bool looped)
        {
            soundInstance.IsLooped = looped;
        }

        // FIXME
        public static void LoadSounds(String path, ContentManager content)
        {
            String[] files = Directory.GetFiles("Content/" + path);
            foreach (String file in files)
            {
                String name = Path.GetFileNameWithoutExtension(file);
                SoundObject sound = new SoundObject(content.Load<SoundEffect>(path + "\\" + name));

                // GraphicsObject in Dictionary übernehmen
                if (sound != null)
                {
                    SoundObject.soundObjects.Add(name, sound);
                }
            }
        }

        public static Dictionary<String,SoundObject> soundObjects;
    }
}
