using UnityEngine;

namespace Deliverance.Input
{
    public class InputManager : MonoBehaviour
    {
        private GameInputActions inputActions;
        private GameInputActions.InGameMovementActions inGameMovement;
        private GameInputActions.WeaponInteractionActions weaponInteractions;

        void Awake()
        {
            inputActions = new GameInputActions();
            inGameMovement = inputActions.InGameMovement;
            weaponInteractions = inputActions.WeaponInteraction;
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
                }
            }
        }
    }
}