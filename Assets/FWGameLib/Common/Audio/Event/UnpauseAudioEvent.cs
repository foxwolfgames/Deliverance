using FWGameLib.Common.EventSystem;

namespace FWGameLib.Common.Audio.Event
{
    /// <summary>
    /// Unpause audio sources that were paused by PauseAudioEvent.
    /// </summary>
    public class UnpauseAudioEvent : IEvent
    {
        public delegate void OnEvent(UnpauseAudioEvent e);
        public static event OnEvent Handler;

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}