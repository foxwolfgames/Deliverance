using TMPro;
using UnityEngine;

namespace Deliverance.Gameplay.UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        public TextMeshProUGUI ammoDisplay;

        void Awake()
        {
            // Clear any placeholder text leftover from editor
            ammoDisplay.text = "";
        }

        public void UpdateAmmoDisplay(int? ammo, int? reserveAmmo)
        {
            if (ammo == null)
            {
                ammoDisplay.text = "";
                return;
            }

            string reserveAmmoString = reserveAmmo == null ? "" : $"/{reserveAmmo}";
            ammoDisplay.text = ammo + reserveAmmoString;
        }
    }
}