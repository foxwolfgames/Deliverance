using FWGameLib.Common.EventSystem;

namespace Deliverance.GameState.Event
{
    public class MainMenuSceneLoadedEvent : IEvent
    {
        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}