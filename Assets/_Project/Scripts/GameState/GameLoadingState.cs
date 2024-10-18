using Deliverance.GameState.Event;
using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.GameState
{
    public class GameLoadingState : MonoBehaviour, IState
    {
#pragma warning disable CS0414 // Field is assigned but its value is never used
        [SerializeField] private bool isActive;
#pragma warning restore CS0414 // Field is assigned but its value is never used

        [SerializeField] private bool gameLoaded;

        void Start()
        {
            GameLoadingCompletedEvent.Handler += _ => gameLoaded = true;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("PreInGameState enter");
            isActive = true;
            gameLoaded = false;
        }

        public void OnExit()
        {
            Debug.Log("PreInGameState exit");
            isActive = false;
        }

        public bool CanTransitionGameLoaded()
        {
            return gameLoaded;
        }
    }
}