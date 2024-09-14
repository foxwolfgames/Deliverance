using Deliverance.Input;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Deliverance.Gameplay.UI
{
    public class InteractableTooltipDisplay : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI interactableTooltip;

        void Awake()
        {
            // Clear any placeholder text leftover from editor
            interactableTooltip.text = "";
        }

        public void UpdateTooltip([CanBeNull] string action)
        {
            if (action == null)
            {
                interactableTooltip.text = "";
                return;
            }

            InputManager inputManager = DeliveranceGameManager.Instance.InputSystem;
            InputAction inputAction = inputManager.inGameInteractable.Interact;
            string keybind = inputAction.GetBindingDisplayString(0);

            interactableTooltip.text = $"Press {keybind} to {action}";
        }
    }
}