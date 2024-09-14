using Deliverance.Gameplay;
using Deliverance.Gameplay.UI;
using Deliverance.Input;
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
        public InGameManager InGameManager;

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
            DeliveranceGameManager.Instance.InputSystem.ToggleInputMap(InputMaps.WeaponInteractions, true);
            DeliveranceGameManager.Instance.InputSystem.ToggleInputMap(InputMaps.InGameMovement, true);
            DeliveranceGameManager.Instance.InputSystem.ToggleInputMap(InputMaps.InGameInteractable, true);
        }

        public void OnExit()
        {
            Debug.Log("InGameState exit");
            isActive = false;
            new ChangeHUDVisibilityEvent(false).Invoke();
            DeliveranceGameManager.Instance.InputSystem.ToggleInputMap(InputMaps.WeaponInteractions, false);
            DeliveranceGameManager.Instance.InputSystem.ToggleInputMap(InputMaps.InGameMovement, false);
            DeliveranceGameManager.Instance.InputSystem.ToggleInputMap(InputMaps.InGameInteractable, false);
            // Clean up state
            InGameManager = null;
        }

        private void OnInGameManagerInitializeEvent(object _, InGameManagerInitializeEvent e)
        {
            InGameManager = e.InGameManager;
        }
    }
}