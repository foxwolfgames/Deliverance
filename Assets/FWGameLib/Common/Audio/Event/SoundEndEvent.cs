using System;
using FWGameLib.Common.AudioSystem;

namespace FWGameLib.Common.Audio.Event
{
    public class SoundEndEvent
    {
        public SoundClip SoundClip { get; private set; }
        public PooledAudioSource AudioSource { get; private set; }

        public SoundEndEvent(SoundClip soundClip, PooledAudioSource audioSource)
        {
            SoundClip = soundClip;
            AudioSource = audioSource;
        }

        public void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}