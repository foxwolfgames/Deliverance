using System;
using FWGameLib.Common.EventSystem;
using UnityEngine;

namespace Deliverance.UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField] public String eventName;

        public void OnClick()
        {
            new UIButtonPressEvent(eventName).Invoke();
        }
    }

    public class UIButtonPressEvent : IEvent
    {
        public String EventName;

        public UIButtonPressEvent(String eventName)
        {
            EventName = eventName;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }
}