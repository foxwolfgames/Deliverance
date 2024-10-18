using FWGameLib.Common.EventSystem;
using FWGameLib.InProject.AudioSystem;

namespace FWGameLib.Common.Audio.Event
{
    /// <summary>
    /// Stop all playing audio sources with the given sound.
    /// </summary>
    public class StopSoundEvent : IEvent
    {
        public delegate void OnEvent(StopSoundEvent e);
        public static event OnEvent Handler;

        public Sounds Sound { get; private set; }

        public StopSoundEvent(Sounds sound)
        {
            Sound = sound;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}