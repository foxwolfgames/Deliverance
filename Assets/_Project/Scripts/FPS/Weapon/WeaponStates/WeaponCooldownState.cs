using Deliverance.Input;
using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.InteractableObjects.Weapon
{
    /// <summary>
    /// State where player is in a cooldown after shooting
    /// </summary>
    public class WeaponCooldownState : IState
    {
        private readonly global::Weapon weapon;
        private readonly InputManager input;
        private float inactionTimer;

        public WeaponCooldownState(global::Weapon weapon)
        {
            this.weapon = weapon;
            input = DeliveranceGameManager.Instance.InputSystem;
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

        /// NOTE: This transition should be checked first!
        public bool CanTransitionIdleOutOfBullets()
        {
            bool noBulletsInMagazine = weapon.bulletsLeft <= 0;
            bool noBulletsInBurstRemaining = weapon.data.shootingMode == ShootingMode.Burst && weapon.burstBulletsLeft <= 0;

            return noBulletsInMagazine || noBulletsInBurstRemaining;
        }

        public bool CanTransitionIdleSingleShot()
        {
            bool shootingModeSingle = weapon.data.shootingMode == ShootingMode.Single;

            return shootingModeSingle && IsInactionTimerCompleted();
        }

        public bool CanTransitionShootingContinueBurst()
        {
            bool isBurst = weapon.data.shootingMode == ShootingMode.Burst;
            bool bulletsInBurstRemaining = weapon.burstBulletsLeft > 0;

            return isBurst && IsInactionTimerCompleted() && bulletsInBurstRemaining;
        }

        public bool CanTransitionIdleStoppedShootingAuto()
        {
            bool isAuto = weapon.data.shootingMode == ShootingMode.Auto;
            bool notPressed = !input.weaponInteractions.Shoot.IsPressed();

            return isAuto && IsInactionTimerCompleted() && notPressed;
        }

        public bool CanTransitionShootingContinueAuto()
        {
            bool isAuto = weapon.data.shootingMode == ShootingMode.Auto;
            bool pressed = input.weaponInteractions.Shoot.IsPressed();

            return isAuto && IsInactionTimerCompleted() && pressed;
        }

        public bool CanTransitionReloading()
        {
            bool reloadPressed = input.weaponInteractions.Reload.IsPressed();
            bool magazineHasRoomForBullets = weapon.bulletsLeft < weapon.data.magazineSize;
            bool weaponHasReserveAmmo = InventoryManager.Instance.CheckAmmoLeft() > 0;

            return reloadPressed && magazineHasRoomForBullets && weaponHasReserveAmmo;
        }
    }
}