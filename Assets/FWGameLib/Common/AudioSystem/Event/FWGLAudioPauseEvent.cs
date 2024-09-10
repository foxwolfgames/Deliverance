using FWGameLib.Common.EventSystem;
using FWGameLib.InProject.EventSystem;

namespace FWGameLib.Common.AudioSystem.Event
{
    public class FWGLAudioPauseEvent : IEvent
    {
        public void Invoke()
        {
            EventRegister.Instance.Invoke(this);
        }
    }
}