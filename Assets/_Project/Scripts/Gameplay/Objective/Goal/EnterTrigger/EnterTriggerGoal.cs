namespace Deliverance.Gameplay.Objective
{
    public class EnterTriggerGoal : AbstractGoal
    {
        public string triggerId;

        void Awake()
        {
            EnterTriggerCriteriaCompletedEvent.Handler += On;
        }

        void OnDestroy()
        {
            EnterTriggerCriteriaCompletedEvent.Handler -= On;
        }

        private void On(EnterTriggerCriteriaCompletedEvent e)
        {
            if (!isActive) return;
            if (isCompleted) return;
            if (e.TriggerId != triggerId) return;

            CompleteGoal();
        }
    }
}