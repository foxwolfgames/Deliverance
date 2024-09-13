using FWGameLib.Common.StateMachine;
using UnityEngine;

namespace Deliverance.InteractableObjects.Weapon
{
    /// <summary>
    /// State where a player is reloading
    /// </summary>
    public class ReloadingState : IState
    {
        private global::Weapon weapon;
        private float reloadTimer;

        public ReloadingState(global::Weapon weapon)
        {
            this.weapon = weapon;
        }

        public void Tick()
        {
            reloadTimer += Time.deltaTime;
        }

        public void OnEnter()
        {
            reloadTimer = 0;
            weapon.ReloadStart();
        }

        public void OnExit()
        {
            weapon.UpdateAmmoAfterReload();
        }

        public bool CanTransitionReloadCompleted()
        {
            return reloadTimer > weapon.data.reloadTime;
        }
    }
}