using System;
using FWGameLib.Common.EventSystem;
using FWGameLib.InProject.AudioSystem;

namespace FWGameLib.Common.Audio.Event
{
    /// <summary>
    /// Stop all playing audio sources with the given sound.
    /// </summary>
    public class StopSoundEvent : IEvent
    {
        public Sounds Sound { get; private set; }

        public StopSoundEvent(Sounds sound)
        {
            Sound = sound;
        }

        public void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}