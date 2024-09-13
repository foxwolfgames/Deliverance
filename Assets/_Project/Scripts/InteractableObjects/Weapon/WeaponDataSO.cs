using FWGameLib.InProject.AudioSystem;
using UnityEngine;

namespace Deliverance.InteractableObjects.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "Deliverance/Weapon Data")]
    public class WeaponDataSO : ScriptableObject
    {
        [Header("Background")]
        public Weapons weaponKey;
        public string weaponName;

        [Header("Bullet")]
        public int damage;
        public float bulletVelocity;
        public float bulletMaxLifetime;

        [Header("Shooting")]
        public ShootingMode shootingMode;
        public float shootingDelay;
        public int bulletsPerBurst;

        [Header("Ammo and Reloading")]
        public float reloadTime;
        public int magazineSize;

        [Header("FX")]
        public Sounds shootingSound;
        public Sounds reloadingSound;
        public Sounds emptyMagazineSound;
    }
}