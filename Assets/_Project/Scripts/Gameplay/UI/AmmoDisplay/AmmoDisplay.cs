using TMPro;
using UnityEngine;

namespace Deliverance.Gameplay.UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        public TextMeshProUGUI ammoDisplay;

        public void UpdateAmmoDisplay(int ammo, int reserveAmmo)
        {
            ammoDisplay.text = $"{ammo}/{reserveAmmo}";
        }
    }
}