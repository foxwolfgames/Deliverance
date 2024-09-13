using Deliverance.Input;
using FWGameLib.Common.AudioSystem;
using FWGameLib.InProject.EventSystem;
using UnityEngine;

namespace Deliverance
{
    public class DeliveranceGameManager : MonoBehaviour
    {
        public static DeliveranceGameManager Instance;
        public AudioManager Audio;
        public EventRegister EventRegister;
        public LevelManager LevelManager;
        public GameStateManager GameState;
        public InputManager InputSystem;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                // Initialize event register
                EventRegister = new();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
