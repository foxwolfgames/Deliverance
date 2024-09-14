using FWGameLib.Common.EventSystem;
using JetBrains.Annotations;

namespace Deliverance.Gameplay.UI
{
    public class UpdateInteractableTooltipDisplayEvent : IEvent
    {
        [CanBeNull] public readonly string Text;

        public UpdateInteractableTooltipDisplayEvent([CanBeNull] string text)
        {
            Text = text;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}