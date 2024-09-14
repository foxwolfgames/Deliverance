using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Deliverance.Gameplay.Objective
{
    public class Objective : MonoBehaviour
    {
        public ObjectiveSO data;
        public AbstractGoal[] goals;
        public UnityEvent onObjectiveStarted;
        public UnityEvent onObjectiveCompleted;

        [CanBeNull] public Objective nextObjective;

        public bool AllGoalsCompleted()
        {
            return goals.All(goal => goal.isCompleted);
        }

        public void StartObjective()
        {
            onObjectiveStarted.Invoke();
            foreach (var goal in goals)
            {
                goal.isActive = true;
            }
        }

        public void CompleteObjective()
        {
            onObjectiveCompleted.Invoke();
            foreach (var goal in goals)
            {
                goal.isActive = false;
            }
        }
    }
}