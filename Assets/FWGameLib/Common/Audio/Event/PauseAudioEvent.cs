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
        public delegate void OnEvent(PauseAudioEvent e);
        public static event OnEvent Handler;

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}