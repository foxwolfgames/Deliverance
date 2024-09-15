using FWGameLib.Common.AudioSystem;
using FWGameLib.Common.AudioSystem.Event;
using FWGameLib.InProject.AudioSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Deliverance.UI
{
    public class VolumeSlider : MonoBehaviour, IInitializePotentialDragHandler
    {
        public AudioVolumeType audioVolumeType;

        void Start()
        {
            Slider slider = GetComponent<Slider>();
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.onValueChanged.AddListener(OnValueChange);
        }

        void OnEnable()
        {
            Slider slider = GetComponent<Slider>();
            slider.value = DeliveranceGameManager.Instance.Audio.VolumeValues[audioVolumeType];
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (eventData.selectedObject != gameObject) return;
            DeliveranceGameManager.Instance.Audio.PlaySound(Sounds.SFX_UI_BUTTON_CLICK);
        }

        private void OnValueChange(float newValue)
        {
            new FWGLChangeVolumeEvent(audioVolumeType, newValue).Invoke();
        }
    }
}
