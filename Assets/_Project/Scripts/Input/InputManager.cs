using UnityEngine;

namespace Deliverance.Input
{
    public class InputManager : MonoBehaviour
    {
        private GameInputActions inputActions;
        public GameInputActions.InGameMovementActions inGameMovement;
        public GameInputActions.WeaponInteractionActions weaponInteractions;
        public GameInputActions.InGameInteractableActions inGameInteractable;

        void Awake()
        {
            inputActions = new GameInputActions();
            inGameMovement = inputActions.InGameMovement;
            weaponInteractions = inputActions.WeaponInteraction;
            inGameInteractable = inputActions.InGameInteractable;
        }

        public void ToggleInputMap(InputMaps inputMap, bool status)
        {
            if (status)
            {
                switch (inputMap)
                {
                    case InputMaps.InGameMovement:
                        inGameMovement.Enable();
                        break;
                    case InputMaps.WeaponInteractions:
                        weaponInteractions.Enable();
                        break;
                    case InputMaps.InGameInteractable:
                        inGameInteractable.Enable();
                        break;
                }
            }
            else
            {
                switch (inputMap)
                {
                    case InputMaps.InGameMovement:
                        inGameMovement.Disable();
                        break;
                    case InputMaps.WeaponInteractions:
                        weaponInteractions.Disable();
                        break;
                    case InputMaps.InGameInteractable:
                        inGameInteractable.Disable();
                        break;
                }
            }
        }
    }
}