using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.Objective
{
    /// <summary>
    /// Objectives: Fired when an active EnterTriggerCriteria is completed.
    /// </summary>
    public class EnterTriggerCriteriaCompletedEvent : IEvent
    {
        public delegate void OnEvent(EnterTriggerCriteriaCompletedEvent e);
        public static event OnEvent Handler;

        public string TriggerId;

        public EnterTriggerCriteriaCompletedEvent(string triggerId)
        {
            TriggerId = triggerId;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}