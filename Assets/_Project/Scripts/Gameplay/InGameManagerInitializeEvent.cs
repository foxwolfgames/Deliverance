using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay
{
    /// <summary>
    /// Gameplay: Call when the in-game manager is initialized
    /// </summary>
    public class InGameManagerInitializeEvent : IEvent
    {
        public delegate void OnEvent(InGameManagerInitializeEvent e);
        public static event OnEvent Handler;

        public InGameManager InGameManager;

        public InGameManagerInitializeEvent(InGameManager inGameManager)
        {
            InGameManager = inGameManager;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}