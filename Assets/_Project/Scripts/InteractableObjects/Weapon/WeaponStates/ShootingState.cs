using FWGameLib.Common.StateMachine;

namespace Deliverance.InteractableObjects.Weapon
{
    /// <summary>
    /// State where a player is actively shooting
    /// </summary>
    public class ShootingState : IState
    {
        private readonly global::Weapon weapon;

        public ShootingState(global::Weapon weapon)
        {
            this.weapon = weapon;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            weapon.FireWeapon();
        }

        public void OnExit()
        {
        }
    }
}