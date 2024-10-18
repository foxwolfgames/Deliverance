using System;
using FWGameLib.Common.Audio;
using FWGameLib.Common.EventSystem;
using FWGameLib.InProject.AudioSystem;
using TMPro;
using UnityEngine;

namespace Deliverance.UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField] public string eventName;
        [SerializeField] public TextMeshProUGUI text;
        [SerializeField] public Color textUnselectedColor;
        [SerializeField] public Color textSelectedColor;
        [SerializeField] private bool isMouseOver;
        [SerializeField] private Color currentColor;

        public void OnClick()
        {
            new UIButtonPressEvent(eventName).Invoke();
            AudioSystem.Instance.Play(Sounds.SFX_UI_BUTTON_CLICK);
        }

        public void MouseOn()
        {
            isMouseOver = true;
        }

        public void MouseOff()
        {
            isMouseOver = false;
        }

        void Awake()
        {
            currentColor = textUnselectedColor;
            text.color = currentColor;
        }

        void Update()
        {
            Color targetColor = isMouseOver ? textSelectedColor : textUnselectedColor;
            currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * 10);
            text.color = currentColor;
        }
    }

    /// <summary>
    /// UI: Call when pressing a button
    /// </summary>
    public class UIButtonPressEvent : IEvent
    {
        public delegate void OnEvent(UIButtonPressEvent e);
        public static event OnEvent Handler;

        public String EventName;

        public UIButtonPressEvent(string eventName)
        {
            EventName = eventName;
        }

        public void Invoke()
        {
            Handler?.Invoke(this);
        }
    }

    public static class UIButtonEvents
    {
        public const string StartGame = "StartGame";
    }
}