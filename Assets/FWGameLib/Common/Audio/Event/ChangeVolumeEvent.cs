using System;
using FWGameLib.Common.AudioSystem;
using FWGameLib.Common.EventSystem;

namespace FWGameLib.Common.Audio.Event
{
    public class ChangeVolumeEvent : IEvent
    {
        public AudioVolumeType AudioType { get; private set; }
        public float Volume { get; private set; }

        public ChangeVolumeEvent(AudioVolumeType audioType, float volume)
        {
            AudioType = audioType;
            Volume = volume;
        }

        public void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}