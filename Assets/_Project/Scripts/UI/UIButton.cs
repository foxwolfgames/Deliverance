using System;
using FWGameLib.Common.EventSystem;
using UnityEngine;

namespace Deliverance.UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField] public string eventName;

        public void OnClick()
        {
            new UIButtonPressEvent(eventName).Invoke();
        }
    }

    public class UIButtonPressEvent : IEvent
    {
        public String EventName;

        public UIButtonPressEvent(string eventName)
        {
            EventName = eventName;
        }

        public void Invoke()
        {
            DeliveranceGameManager.Instance.EventRegister.Invoke(this);
        }
    }

    public static class UIButtonEvents
    {
        public const string StartGame = "StartGame";
    }
}