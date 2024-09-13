using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.GameState
{
    /// <summary>
    /// State entered by default when the application starts.
    /// </summary>
    public class ApplicationStartingState : MonoBehaviour, IState
    {
        [SerializeField] private bool isActive;
        [SerializeField] private bool hasMenuSceneLoaded;

        void Start()
        {
            DeliveranceGameManager.Instance.EventRegister.MainMenuSceneLoadedEventHandler += (_, _) => { hasMenuSceneLoaded = true; };
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("ApplicationStartingState enter");
            isActive = true;
        }

        public void OnExit()
        {
            Debug.Log("ApplicationStartingState exit");
            isActive = false;
        }

        public bool CanTransitionMenuSceneLoaded()
        {
            return hasMenuSceneLoaded;
        }
    }
}