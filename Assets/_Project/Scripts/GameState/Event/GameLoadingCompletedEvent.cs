using FWGameLib.Common.EventSystem;

namespace Deliverance.GameState.Event
{
    public class GameLoadingCompletedEvent : IEvent
    {
        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}