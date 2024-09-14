using Deliverance.Gameplay.UI;
using Deliverance.Input;
using JetBrains.Annotations;
using UnityEngine;

namespace Deliverance.Gameplay.Interactable
{
    public class PlayerInteract : MonoBehaviour
    {
        [Header("References")]
        public Camera playerCamera;

        [Header("Interact Settings")]
        public float interactDistance;
        public float sphereCastRadius;

        [Header("Layer Mask")]
        public LayerMask interactableLayerMask;

        [Header("Debug")]
        [SerializeField] private Interactable currentInteractable;

        private InputManager _inputSystem;

        void Awake()
        {
            _inputSystem = DeliveranceGameManager.Instance.InputSystem;
        }

        void Update()
        {
            // Check if interact system is enabled (inputs)
            if (!_inputSystem.inGameInteractable.enabled)
            {
                // Interact system is disabled, update current interactable to null
                UpdateCurrentInteractable(null);
                return;
            }

            // Raycasting
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            // Prioritize the ray cast hit, otherwise use sphere cast hit
            Interactable finalInteractable = DoRayCast(ray) ?? DoSphereCast(ray);
            UpdateCurrentInteractable(finalInteractable);

            // Input
            if (_inputSystem.inGameInteractable.Interact.WasPressedThisFrame())
            {
                currentInteractable?.Interact();
            }
        }

        [CanBeNull]
        private Interactable DoSphereCast(Ray ray)
        {
            if (Physics.SphereCast(ray, sphereCastRadius, out RaycastHit hit, interactDistance, interactableLayerMask))
            {
                if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable interactable) && interactable.enabled)
                {
                    return interactable;
                }
            }

            return null;
        }

        [CanBeNull]
        private Interactable DoRayCast(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayerMask))
            {
                if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable interactable) && interactable.enabled)
                {
                    return interactable;
                }
            }

            return null;
        }

        private void UpdateCurrentInteractable([CanBeNull] Interactable interactable)
        {
            // Only return early if we are looking at an interactable and it is the same
            if (interactable && currentInteractable == interactable) return;

            // Only set old interactable out of focus if the underlying game object is not destroyed
            if (currentInteractable && !currentInteractable.gameObject.IsDestroyed())
            {
                currentInteractable.SetInFocus(false);
            }

            currentInteractable = interactable;
            currentInteractable?.SetInFocus(true);
            new UpdateInteractableTooltipDisplayEvent(currentInteractable?.interactTooltip).Invoke();
        }
    }
}