using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.GameState
{
    public class GameLoadingState : MonoBehaviour, IState
    {
        [SerializeField] private bool isActive;
        [SerializeField] private bool gameLoaded;

        void Start()
        {
            DeliveranceGameManager.Instance.EventRegister.GameLoadingCompletedEventHandler += (_, _) => { gameLoaded = true; };
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