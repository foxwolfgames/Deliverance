using Deliverance.Input;
using UnityEngine;

namespace Deliverance
{
    public class DeliveranceGameManager : MonoBehaviour
    {
        public static DeliveranceGameManager Instance;
        public LevelManager LevelManager;
        public GameStateManager GameState;
        public InputManager InputSystem;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
