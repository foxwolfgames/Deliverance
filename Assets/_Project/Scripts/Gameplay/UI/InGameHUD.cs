using UnityEngine;

namespace Deliverance.Gameplay.UI
{
    /// <summary>
    /// In-game HUD script
    /// </summary>
    public class InGameHUD : MonoBehaviour
    {
        public Canvas uiCanvas;
        public AmmoDisplay ammoDisplay;

        void Awake()
        {
            DeliveranceGameManager.Instance.EventRegister.ChangeHUDVisibilityEventHandler += OnChangeHUDVisibilityEvent;
            DeliveranceGameManager.Instance.EventRegister.UpdateAmmoDisplayEventHandler += OnAmmoDisplayEvent;
            // Do not show the HUD on load
            uiCanvas.gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            DeliveranceGameManager.Instance.EventRegister.ChangeHUDVisibilityEventHandler -= OnChangeHUDVisibilityEvent;
            DeliveranceGameManager.Instance.EventRegister.UpdateAmmoDisplayEventHandler -= OnAmmoDisplayEvent;
        }

        private void OnChangeHUDVisibilityEvent(object _, ChangeHUDVisibilityEvent e)
        {
            uiCanvas.gameObject.SetActive(e.IsVisible);
        }

        private void OnAmmoDisplayEvent(object _, UpdateAmmoDisplayEvent e)
        {
            ammoDisplay.UpdateAmmoDisplay(e.AmmoCount, e.ReserveAmmoCount);
        }
    }
}