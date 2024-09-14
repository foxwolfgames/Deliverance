using Deliverance.Gameplay;
using Deliverance.Gameplay.Objective;
using UnityEngine;

namespace Deliverance.Development
{
    public class StartThisObjectiveRightAway : MonoBehaviour
    {
        void Start()
        {
            if (TryGetComponent<Objective>(out Objective objective))
            {
                InGameManager inGameManager = DeliveranceGameManager.Instance.GameState.inGameState.InGameManager;
                if (!inGameManager) return;
                inGameManager.ObjectiveManager.SetCurrentObjective(objective);
            }
        }
    }
}