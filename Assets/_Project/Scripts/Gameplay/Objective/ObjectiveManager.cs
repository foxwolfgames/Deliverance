using JetBrains.Annotations;
using UnityEngine;

namespace Deliverance.Gameplay.Objective
{
    public class ObjectiveManager : MonoBehaviour
    {
        [CanBeNull] public Objective currentObjective;

        void Awake()
        {
            DeliveranceGameManager.Instance.EventRegister.GoalCompletedEventEventHandler += OnGoalCompletedEvent;
        }

        private void OnDestroy()
        {
            DeliveranceGameManager.Instance.EventRegister.GoalCompletedEventEventHandler -= OnGoalCompletedEvent;
        }

        public void SetCurrentObjective([CanBeNull] Objective objective)
        {
            currentObjective = objective;
            UpdateCurrentObjectiveUI();

            if (!currentObjective) return;

            // Fire objective events
            currentObjective.StartObjective();
        }

        /// <summary>
        /// Goal is completed, check if objective is finished.
        /// </summary>
        private void OnGoalCompletedEvent(object _, GoalCompletedEvent e)
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
    }
}