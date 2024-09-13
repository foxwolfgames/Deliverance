using FWGameLib.Common.StateMachine;

namespace Deliverance.InteractableObjects.Weapon
{
    /// <summary>
    /// State where a player is actively shooting
    /// </summary>
    public class ShootingState : IState
    {
        private global::Weapon _weapon;

        public ShootingState(global::Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _weapon.FireWeapon();
        }

        public void OnExit()
        {
        }
    }
}