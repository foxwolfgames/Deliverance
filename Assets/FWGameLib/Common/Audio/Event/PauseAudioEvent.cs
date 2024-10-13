using System;
using FWGameLib.Common.EventSystem;

namespace FWGameLib.Common.Audio.Event
{
    /// <summary>
    /// Pause audio sources that are playing and can be paused.
    /// Use when the game is being paused.
    /// Certain sounds such as UI element can choose to ignore this event.
    /// </summary>
    public class PauseAudioEvent : IEvent
    {
        public void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}