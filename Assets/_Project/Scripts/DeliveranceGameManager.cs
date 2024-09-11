using Deliverance.GameState;
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

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
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
