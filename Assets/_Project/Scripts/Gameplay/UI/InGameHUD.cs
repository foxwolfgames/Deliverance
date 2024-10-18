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
        public InteractableTooltipDisplay interactableTooltip;

        void Awake()
        {
            ChangeHUDVisibilityEvent.Handler += On;
            UpdateAmmoDisplayEvent.Handler += On;
            UpdateInteractableTooltipDisplayEvent.Handler += On;
            // Do not show the HUD on load
            uiCanvas.gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            ChangeHUDVisibilityEvent.Handler -= On;
            UpdateAmmoDisplayEvent.Handler -= On;
            UpdateInteractableTooltipDisplayEvent.Handler -= On;
        }

        private void On(ChangeHUDVisibilityEvent e)
        {
            uiCanvas.gameObject.SetActive(e.IsVisible);
        }

        private void On(UpdateAmmoDisplayEvent e)
        {
            ammoDisplay.UpdateAmmoDisplay(e.AmmoCount, e.ReserveAmmoCount);
        }

        private void On(UpdateInteractableTooltipDisplayEvent e)
        {
            interactableTooltip.UpdateTooltip(e.Text);
        }
    }
}