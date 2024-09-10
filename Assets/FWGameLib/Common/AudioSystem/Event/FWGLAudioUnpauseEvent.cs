using FWGameLib.Common.EventSystem;
using FWGameLib.InProject.EventSystem;

namespace FWGameLib.Common.AudioSystem.Event
{
    public class FWGLAudioUnpauseEvent : IEvent
    {
        public void Invoke()
        {
            EventRegister.Instance.Invoke(this);
        }
    }
}