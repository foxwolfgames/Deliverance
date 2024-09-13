using UnityEngine;

namespace Deliverance.Gameplay
{
    public class InGameManager : MonoBehaviour
    {
        void Awake()
        {
            new InGameManagerInitializeEvent(this).Invoke();
        }
    }
}