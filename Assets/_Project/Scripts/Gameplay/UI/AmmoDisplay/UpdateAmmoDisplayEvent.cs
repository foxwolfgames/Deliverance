using FWGameLib.Common.EventSystem;

namespace Deliverance.Gameplay.UI
{
    public class UpdateAmmoDisplayEvent : IEvent
    {
        public int AmmoCount;
        public int ReserveAmmoCount;

        public UpdateAmmoDisplayEvent(int ammoCount, int reserveAmmoCount)
        {
            AmmoCount = ammoCount;
            ReserveAmmoCount = reserveAmmoCount;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}