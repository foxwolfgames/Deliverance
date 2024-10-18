using FWGameLib.Common.AudioSystem;

namespace FWGameLib.Common.Audio.Event
{
    public class SoundEndEvent
    {
        public delegate void OnEvent(SoundEndEvent e);
        public static event OnEvent Handler;

        public SoundClip SoundClip { get; private set; }
        public PooledAudioSource AudioSource { get; private set; }

        public SoundEndEvent(SoundClip soundClip, PooledAudioSource audioSource)
        {
            SoundClip = soundClip;
            AudioSource = audioSource;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}