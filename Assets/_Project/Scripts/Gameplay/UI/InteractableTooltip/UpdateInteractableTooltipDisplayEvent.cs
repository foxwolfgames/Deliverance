using FWGameLib.Common.EventSystem;
using JetBrains.Annotations;

namespace Deliverance.Gameplay.UI
{
    /// <summary>
    /// UI: Update the interactable tooltip
    /// </summary>
    public class UpdateInteractableTooltipDisplayEvent : IEvent
    {
        public delegate void OnEvent(UpdateInteractableTooltipDisplayEvent e);
        public static event OnEvent Handler;

        [CanBeNull] public readonly string Text;

        public UpdateInteractableTooltipDisplayEvent([CanBeNull] string text)
        {
            Text = text;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }
}