using UnityEngine;
using UnityEngine.Events;

namespace Deliverance.Gameplay.Objective
{
    public abstract class AbstractGoal : MonoBehaviour
    {
        public GoalSO data;
        public UnityEvent onGoalCompleted;
        public bool isActive;
        public bool isCompleted;

        protected void CompleteGoal()
        {
            isCompleted = true;
            onGoalCompleted.Invoke();
            new GoalCompletedEvent(this).Invoke();
        }
    }
}