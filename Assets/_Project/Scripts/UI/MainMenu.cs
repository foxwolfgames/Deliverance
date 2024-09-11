using UnityEngine;

namespace Deliverance.UI
{
    public class MainMenu : MonoBehaviour
    {
        void Awake()
        {
            DeliveranceGameManager.Instance.EventRegister.UIButtonPressEventEventHandler += OnButtonPress;
        }

        void StartGame()
        {
            DeliveranceGameManager.Instance.LevelManager.LoadGame();
        }

        void OnButtonPress(object _, UIButtonPressEvent e)
        {
            if (e.EventName == "StartGame")
            {
                StartGame();
            }
        }
    }
}
