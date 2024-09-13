using Deliverance.GameState;
using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] public ApplicationStartingState applicationStartingState;
        [SerializeField] public MainMenuState mainMenuState;
        [SerializeField] public GameLoadingState gameLoadingState;
        [SerializeField] public InGameState inGameState;

        private StateMachine _stateMachine;

        void Awake()
        {
            _stateMachine = new StateMachine();

            // Transitions
            _stateMachine.AddTransition(applicationStartingState, mainMenuState, applicationStartingState.CanTransitionMenuSceneLoaded);
            _stateMachine.AddTransition(mainMenuState, gameLoadingState, mainMenuState.CanTransitionGameIsStarting);
            _stateMachine.AddTransition(gameLoadingState, inGameState, gameLoadingState.CanTransitionGameLoaded);

            // Set initial state
            _stateMachine.SetState(applicationStartingState);
        }

        void Update()
        {
            _stateMachine.Tick();
        }
    }
}