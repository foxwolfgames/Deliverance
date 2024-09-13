using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay
{
    public class InGameManagerInitializeEvent : IEvent
    {
        public InGameManager InGameManager;

        public InGameManagerInitializeEvent(InGameManager inGameManager)
        {
            InGameManager = inGameManager;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}