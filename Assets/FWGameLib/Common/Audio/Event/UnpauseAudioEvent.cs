using System;
using FWGameLib.Common.EventSystem;

namespace FWGameLib.Common.Audio.Event
{
    /// <summary>
    /// Unpause audio sources that were paused by PauseAudioEvent.
    /// </summary>
    public class UnpauseAudioEvent : IEvent
    {
        public void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}