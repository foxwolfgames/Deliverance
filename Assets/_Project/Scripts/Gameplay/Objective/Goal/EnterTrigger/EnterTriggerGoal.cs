namespace Deliverance.Gameplay.Objective
{
    public class EnterTriggerGoal : AbstractGoal
    {
        public string triggerId;

        void Awake()
        {
            DeliveranceGameManager.Instance.EventRegister.EnterTriggerCriteriaCompletedEventHandler += OnEnterTriggerCriteriaCompletedEvent;
        }

        void OnDestroy()
        {
            DeliveranceGameManager.Instance.EventRegister.EnterTriggerCriteriaCompletedEventHandler -= OnEnterTriggerCriteriaCompletedEvent;
        }

        private void OnEnterTriggerCriteriaCompletedEvent(object _, EnterTriggerCriteriaCompletedEvent e)
        {
            if (!isActive) return;
            if (isCompleted) return;
            if (e.TriggerId != triggerId) return;

            CompleteGoal();
        }
    }
}