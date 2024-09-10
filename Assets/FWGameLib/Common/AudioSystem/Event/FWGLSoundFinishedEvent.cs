using FWGameLib.Common.EventSystem;
using FWGameLib.InProject.AudioSystem;
using FWGameLib.InProject.EventSystem;

namespace FWGameLib.Common.AudioSystem.Event
{
    public class FWGLSoundFinishedEvent : IEvent
    {
        public Sounds Sound;
    
        public FWGLSoundFinishedEvent(Sounds sound)
        {
            Sound = sound;
        }
    
        public void Invoke()
        {
            EventRegister.Instance.Invoke(this);
        }
    }
}