using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.UI
{
    /// <summary>
    /// UI: Update the ammo display
    /// </summary>
    public class UpdateAmmoDisplayEvent : IEvent
    {
        public delegate void OnEvent(UpdateAmmoDisplayEvent e);
        public static event OnEvent Handler;

        public readonly int? AmmoCount;
        public readonly int? ReserveAmmoCount;

        public UpdateAmmoDisplayEvent(int? ammoCount, int? reserveAmmoCount)
        {
            AmmoCount = ammoCount;
            ReserveAmmoCount = reserveAmmoCount;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}