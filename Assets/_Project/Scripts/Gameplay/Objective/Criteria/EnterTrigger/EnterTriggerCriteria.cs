using System;
using UnityEngine;
using UnityEngine.Events;

namespace Deliverance.Gameplay.Objective
{
    [Serializable]
    public class EnterTriggerCriteria : MonoBehaviour
    {
        public string triggerId;
        public bool isActive;
        public UnityEvent onActivation;
        public UnityEvent onDeactivation;

        void Awake()
        {
            ActivateEnterTriggerCriteriaEvent.Handler += On;
        }

        void OnDestroy()
        {
            ActivateEnterTriggerCriteriaEvent.Handler -= On;
        }

        void OnTriggerEnter(Collider other)
        {
            if (!isActive || !other.CompareTag("Player")) return;

            isActive = false;

            // Fire event to signal completion
            new EnterTriggerCriteriaCompletedEvent(triggerId).Invoke();

            onDeactivation.Invoke();
        }

        private void On(ActivateEnterTriggerCriteriaEvent e)
        {
            if (e.TriggerId != triggerId) return;
            isActive = true;
            onActivation.Invoke();
        }
    }
}