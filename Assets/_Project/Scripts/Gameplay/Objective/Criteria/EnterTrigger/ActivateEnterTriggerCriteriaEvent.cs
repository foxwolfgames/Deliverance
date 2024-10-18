using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.Objective
{
    /// <summary>
    /// Objectives: Activate a EnterTriggerCriteria.
    /// You should only call this once an objective is ready to listen for the trigger, or the player
    /// can end up in a state where they can't progress.
    /// </summary>
    public class ActivateEnterTriggerCriteriaEvent : IEvent
    {
        public delegate void OnEvent(ActivateEnterTriggerCriteriaEvent e);
        public static event OnEvent Handler;

        public string TriggerId;

        public ActivateEnterTriggerCriteriaEvent(string triggerId)
        {
            TriggerId = triggerId;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}