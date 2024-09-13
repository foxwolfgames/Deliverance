using Deliverance.GameState.Event;
using UnityEngine;

namespace Deliverance.UI
{
    public class MainMenu : MonoBehaviour
    {
        void Awake()
        {
            DeliveranceGameManager.Instance.EventRegister.UIButtonPressEventEventHandler += OnButtonPress;
        }

        void Start()
        {
            // Signal to game state manager that main menu scene has loaded
            new MainMenuSceneLoadedEvent().Invoke();
        }

        private void StartGame()
        {
            DeliveranceGameManager.Instance.LevelManager.LoadGame();
        }

        private void OnButtonPress(object _, UIButtonPressEvent e)
        {
            if (e.EventName == UIButtonEvents.StartGame)
            {
                StartGame();
            }
        }
    }
}
