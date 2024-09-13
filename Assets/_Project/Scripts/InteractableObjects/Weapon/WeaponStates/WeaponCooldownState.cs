using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.InteractableObjects.Weapon
{
    /// <summary>
    /// State where player is in a cooldown after shooting
    /// </summary>
    public class WeaponCooldownState : IState
    {
        private global::Weapon weapon;
        private float inactionTimer;

        public WeaponCooldownState(global::Weapon weapon)
        {
            this.weapon = weapon;
        }

        public void Tick()
        {
            inactionTimer += Time.deltaTime;
        }

        public void OnEnter()
        {
            inactionTimer = 0;
        }

        public void OnExit()
        {
        }

        private bool IsInactionTimerCompleted()
        {
            return inactionTimer > weapon.data.shootingDelay;
        }

        // Check this transition FIRST!
        public bool CanTransitionIdleOutOfBullets()
        {
            return weapon.bulletsLeft <= 0 || (weapon.data.shootingMode == ShootingMode.Burst && weapon.burstBulletsLeft <= 0);
        }

        public bool CanTransitionIdleSingleShot()
        {
            if (weapon.data.shootingMode != ShootingMode.Single) return false;

            return IsInactionTimerCompleted();
        }

        public bool CanTransitionShootingContinueBurst()
        {
            if (weapon.data.shootingMode != ShootingMode.Burst) return false;

            return IsInactionTimerCompleted() && weapon.burstBulletsLeft > 0;
        }

        public bool CanTransitionIdleStoppedShootingAuto()
        {
            if (weapon.data.shootingMode != ShootingMode.Auto) return false;

            return !UnityEngine.Input.GetKey(KeyCode.Mouse0);
        }

        public bool CanTransitionShootingContinueAuto()
        {
            if (weapon.data.shootingMode != ShootingMode.Auto) return false;

            return IsInactionTimerCompleted() && UnityEngine.Input.GetKey(KeyCode.Mouse0);
        }

        public bool CanTransitionReloading()
        {
            return UnityEngine.Input.GetKey(KeyCode.R) && weapon.bulletsLeft < weapon.data.magazineSize && InventoryManager.Instance.CheckAmmoLeft() > 0;
        }
    }
}