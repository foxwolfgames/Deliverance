using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.Objective
{
    public class ActivateEnterTriggerCriteriaEvent : IEvent
    {
        public string TriggerId;

        public ActivateEnterTriggerCriteriaEvent(string triggerId)
        {
            TriggerId = triggerId;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}