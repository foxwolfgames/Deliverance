using FWGameLib.Common.Audio;
using FWGameLib.InProject.AudioSystem;
using UnityEngine;

namespace Deliverance.Gameplay.Interactable
{
    public class FirstAidKit : MonoBehaviour
    {
        public void PickupFirstAidKit() {
            // Add code to heal player
            print("First aid kit picked up, healing player for 25 health");

            AudioSystem.Instance.Play(Sounds.SFX_UI_BUTTON_CLICK, transform);

            // Destroy the kit
            Destroy(gameObject);
        }
    }
}
