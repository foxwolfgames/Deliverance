using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.InteractableObjects.Weapon
{
    /// <summary>
    /// State where player can do any actions
    /// </summary>
    public class IdleState : IState
    {
        private global::Weapon weapon;

        public IdleState(global::Weapon weapon)
        {
            this.weapon = weapon;
        }

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0) && weapon.bulletsLeft <= 0)
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
            return UnityEngine.Input.GetKey(KeyCode.R) && weapon.bulletsLeft < weapon.data.magazineSize && InventoryManager.Instance.CheckAmmoLeft() > 0;
        }

        public bool CanTransitionShooting()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Mouse0) && weapon.bulletsLeft > 0;
        }
    }
}