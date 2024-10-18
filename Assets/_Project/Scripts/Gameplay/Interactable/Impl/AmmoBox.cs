using FWGameLib.Common.Audio;
using FWGameLib.InProject.AudioSystem;
using UnityEngine;

namespace Deliverance.Gameplay.Interactable
{
    public class AmmoBox : MonoBehaviour
    {
        public void Pickup()
        {
            // Add code to give player ammo
            print("Ammo box picked up, giving player 20 ammo");

            AudioSystem.Instance.Play(Sounds.SFX_GAMEPLAY_COLT1911_RELOAD, transform);

            // Destroy the box
            Destroy(gameObject);
        }
    }
}
