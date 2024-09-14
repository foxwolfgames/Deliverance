using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.Objective
{
    public class GoalCompletedEvent : IEvent
    {
        public AbstractGoal Goal;

        public GoalCompletedEvent(AbstractGoal goal)
        {
            Goal = goal;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}