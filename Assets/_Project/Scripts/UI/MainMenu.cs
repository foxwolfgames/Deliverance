using Deliverance.GameState.Event;
using UnityEngine;

namespace Deliverance.UI
{
    public class MainMenu : MonoBehaviour
    {
        public RectTransform mainMenuPanel;
        public RectTransform optionsPanel;

        void Awake()
        {
            UIButtonPressEvent.Handler += On;
        }

        void Start()
        {
            // Signal to game state manager that main menu scene has loaded
            new MainMenuSceneLoadedEvent().Invoke();
        }

        void OpenOptions()
        {
            // Set top/bottom of rect transform to 0,0
            optionsPanel.anchoredPosition = new Vector2(0, 0);
            // Set top/bottom of main menu to 1080,-1080
            mainMenuPanel.anchoredPosition = new Vector2(0, 1080);
        }

        void CloseOptions()
        {
            // Set top/bottom of rect transform to 1080,-1080
            optionsPanel.anchoredPosition = new Vector2(0, 1080);
            // Set top/bottom of main menu to 0,0
            mainMenuPanel.anchoredPosition = new Vector2(0, 0);
        }

        private void StartGame()
        {
            DeliveranceGameManager.Instance.LevelManager.LoadGame();
        }

        private void On(UIButtonPressEvent e)
        {
            switch (e.EventName)
            {
                case UIButtonEvents.StartGame:
                    StartGame();
                    break;
                case "MainMenuOptions":
                    OpenOptions();
                    break;
                case "OptionsBack":
                    CloseOptions();
                    break;
                case "QuitGame":
                    Application.Quit();
                    break;
            }
        }
    }
}
