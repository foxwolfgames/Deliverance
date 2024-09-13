using UnityEngine;

namespace Deliverance.Gameplay.UI
{
    /// <summary>
    /// In-game HUD script
    /// </summary>
    public class InGameHUD : MonoBehaviour
    {
        public Canvas uiCanvas;

        void Awake()
        {
            DeliveranceGameManager.Instance.EventRegister.ChangeHUDVisibilityEventHandler += OnChangeHUDVisibilityEvent;
            // Do not show the HUD on load
            uiCanvas.gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            DeliveranceGameManager.Instance.EventRegister.ChangeHUDVisibilityEventHandler -= OnChangeHUDVisibilityEvent;
        }

        private void OnChangeHUDVisibilityEvent(object _, ChangeHUDVisibilityEvent e)
        {
            uiCanvas.gameObject.SetActive(e.IsVisible);
        }
    }
}