using JetBrains.Annotations;
using UnityEngine;

namespace Deliverance.Gameplay.Objective
{
    public class ObjectiveManager : MonoBehaviour
    {
        [CanBeNull] public Objective currentObjective;

        void Awake()
        {
            GoalCompletedEvent.Handler += On;
        }

        private void OnDestroy()
        {
            GoalCompletedEvent.Handler -= On;
        }

        public void SetCurrentObjective([CanBeNull] Objective objective)
        {
            currentObjective = objective;
            UpdateCurrentObjectiveUI();

            if (!currentObjective) return;

            // Fire objective events
            currentObjective.StartObjective();
        }

        private void UpdateCurrentObjectiveUI()
        {
            if (!currentObjective)
            {
                // TODO: Clear the objective UI
                return;
            }

            // TODO: Do UI/FX stuff pertaining to updating the goals displayed on screen
        }

        private void CompleteCurrentObjective()
        {
            // Fire objective events
            currentObjective.CompleteObjective();

            // TODO: Do UI/FX stuff pertaining to completing objective

            SetCurrentObjective(currentObjective.nextObjective);
        }

        /// <summary>
        /// Goal is completed, check if objective is finished.
        /// </summary>
        private void On(GoalCompletedEvent e)
        {
            if (!currentObjective) return;

            if (currentObjective.AllGoalsCompleted())
            {
                CompleteCurrentObjective();
            }
            else
            {
                UpdateCurrentObjectiveUI();
            }
        }
    }
}