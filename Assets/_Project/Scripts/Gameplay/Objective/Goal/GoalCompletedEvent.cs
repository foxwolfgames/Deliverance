using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.Objective
{
    /// <summary>
    /// Objectives: Fired when a goal's criteria is completed.
    /// </summary>
    public class GoalCompletedEvent : IEvent
    {
        public delegate void OnEvent(GoalCompletedEvent e);
        public static event OnEvent Handler;

        public AbstractGoal Goal;

        public GoalCompletedEvent(AbstractGoal goal)
        {
            Goal = goal;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}