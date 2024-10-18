using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.UI
{
    /// <summary>
    /// UI: Change the visibility of the in-game HUD
    /// </summary>
    public class ChangeHUDVisibilityEvent : IEvent
    {
        public delegate void OnEvent(ChangeHUDVisibilityEvent e);
        public static event OnEvent Handler;

        public bool IsVisible;

        public ChangeHUDVisibilityEvent(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}