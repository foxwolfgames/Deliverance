using System.Linq.Expressions;
using Deliverance.Gameplay;
using Deliverance.Gameplay.UI;
using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.GameState
{
    public class InGameState : MonoBehaviour, IState
    {
#pragma warning disable CS0414 // Field is assigned but its value is never used
        [SerializeField] private bool isActive;
#pragma warning restore CS0414 // Field is assigned but its value is never used

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
            new ChangeHUDVisibilityEvent(true).Invoke();
        }

        public void OnExit()
        {
            Debug.Log("InGameState exit");
            isActive = false;
            new ChangeHUDVisibilityEvent(false).Invoke();
            // Clean up state
            InGameManager = null;
        }

        private void OnInGameManagerInitializeEvent(object _, InGameManagerInitializeEvent e)
        {
            InGameManager = e.InGameManager;
        }
    }
}