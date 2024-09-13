using Deliverance.Gameplay;
using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.GameState
{
    public class InGameState : MonoBehaviour, IState
    {
        [SerializeField] private bool isActive;
        [SerializeField] private bool isPaused;

        // Assigned via event
        private InGameManager InGameManager;

        void Start()
        {
            DeliveranceGameManager.Instance.EventRegister.InGameManagerInitializeEventHandler += OnInGameManagerInitializeEvent;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("InGameState enter");
            isActive = true;
        }

        public void OnExit()
        {
            Debug.Log("InGameState exit");
            isActive = false;
            // Clean up state
            InGameManager = null;
        }

        private void OnInGameManagerInitializeEvent(object _, InGameManagerInitializeEvent e)
        {
            InGameManager = e.InGameManager;
        }
    }
}