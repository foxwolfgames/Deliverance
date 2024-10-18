using FWGameLib.Common.EventSystem;

namespace Deliverance.GameState.Event
{
    /// <summary>
    /// GameState: Call when game has finished loading to indicate transition into the InGameState
    /// Not necessarily indicative of when we enter the game scene, but instead when all steps in the LevelManager have been completed
    /// </summary>
    public class GameLoadingCompletedEvent : IEvent
    {
        public delegate void OnEvent(GameLoadingCompletedEvent e);
        public static event OnEvent Handler;

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}