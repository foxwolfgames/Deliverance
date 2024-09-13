using TMPro;
using UnityEngine;

namespace Deliverance.Gameplay.UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        public TextMeshProUGUI ammoDisplay;

        void Awake()
        {
            DeliveranceGameManager.Instance.EventRegister.UpdateAmmoDisplayEventHandler += OnUpdateAmmoDisplayEvent;
        }

        void OnDestroy()
        {
            DeliveranceGameManager.Instance.EventRegister.UpdateAmmoDisplayEventHandler -= OnUpdateAmmoDisplayEvent;
        }

        public void UpdateAmmoDisplay(int ammo, int reserveAmmo)
        {
            ammoDisplay.text = $"{ammo}/{reserveAmmo}";
        }

        private void OnUpdateAmmoDisplayEvent(object _, UpdateAmmoDisplayEvent e)
        {
            UpdateAmmoDisplay(e.AmmoCount, e.ReserveAmmoCount);
        }
    }
}