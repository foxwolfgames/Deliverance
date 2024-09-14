using Deliverance.Gameplay.Objective;
using UnityEngine;

namespace Deliverance.Gameplay
{
    public class InGameManager : MonoBehaviour
    {
        public ObjectiveManager ObjectiveManager;

        void Awake()
        {
            new InGameManagerInitializeEvent(this).Invoke();
        }
    }
}