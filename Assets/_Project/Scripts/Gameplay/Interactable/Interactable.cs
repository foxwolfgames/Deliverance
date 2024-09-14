using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Deliverance.Gameplay.Interactable
{
    public class Interactable : MonoBehaviour
    {
        public string interactTooltip;
        public Outline outline;
        public UnityEvent onInteract;

        void OnDisable()
        {
            outline.enabled = false;
        }

        public void SetInFocus(bool inFocus)
        {
            if (!UnityObjectUtility.IsDestroyed(gameObject))
            {
                outline.enabled = inFocus;
            }
        }

        public void Interact()
        {
            onInteract.Invoke();
        }
    }
}