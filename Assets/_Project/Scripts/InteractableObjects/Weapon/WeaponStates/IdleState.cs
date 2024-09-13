using Deliverance.Input;
using FWGameLib.Common.StateMachine;

namespace Deliverance.InteractableObjects.Weapon
{
    /// <summary>
    /// State where player can do any actions
    /// </summary>
    public class IdleState : IState
    {
        private global::Weapon weapon;
        private readonly InputManager input;

        public IdleState(global::Weapon weapon)
        {
            this.weapon = weapon;
            input = DeliveranceGameManager.Instance.InputSystem;
        }

        public void Tick()
        {
            if (input.weaponInteractions.Shoot.WasPressedThisFrame() && weapon.bulletsLeft <= 0)
            {
                weapon.PlayEmptyMagazine();
            }
        }

        public void OnEnter()
        {
            // Reset burst bullets (if burst mode)
            weapon.burstBulletsLeft = weapon.data.bulletsPerBurst;
        }

        public void OnExit()
        {
        }

        public bool CanTransitionReloading()
        {
            bool reloadPressed = input.weaponInteractions.Reload.IsPressed();
            bool magazineHasRoomForBullets = weapon.bulletsLeft < weapon.data.magazineSize;
            bool weaponHasReserveAmmo = InventoryManager.Instance.CheckAmmoLeft() > 0;

            return reloadPressed && magazineHasRoomForBullets && weaponHasReserveAmmo;
        }

        public bool CanTransitionShooting()
        {
            bool shootPressed = input.weaponInteractions.Shoot.WasPressedThisFrame();
            bool hasBulletsInMagazine = weapon.bulletsLeft > 0;

            return shootPressed && hasBulletsInMagazine;
        }
    }
}