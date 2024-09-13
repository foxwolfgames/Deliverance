using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.UI
{
    public class ChangeHUDVisibilityEvent : IEvent
    {
        public bool IsVisible;

        public ChangeHUDVisibilityEvent(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}