using FWGameLib.Common.EventSystem;

namespace Deliverance.GameState.Event
{
    /// <summary>
    /// GameState: Main menu scene finished loading
    /// </summary>
    public class MainMenuSceneLoadedEvent : IEvent
    {
        public delegate void OnEvent(MainMenuSceneLoadedEvent e);
        public static event OnEvent Handler;

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}