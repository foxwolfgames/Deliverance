using FWGameLib.Common.EventSystem;
using FWGameLib.InProject.AudioSystem;
using FWGameLib.InProject.EventSystem;

namespace FWGameLib.Common.AudioSystem.Event
{
    public class FWGLStopSoundEvent : IEvent
    {
        public Sounds SoundName;

        public FWGLStopSoundEvent(Sounds soundName)
        {
            SoundName = soundName;
        }

        public void Invoke()
        {
            EventRegister.Instance.Invoke(this);
        }
    }
}