using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.Objective
{
    public class EnterTriggerCriteriaCompletedEvent : IEvent
    {
        public string TriggerId;

        public EnterTriggerCriteriaCompletedEvent(string triggerId)
        {
            TriggerId = triggerId;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}