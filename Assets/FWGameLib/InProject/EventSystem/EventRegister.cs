using System;
using FWGameLib.Common.AudioSystem.Event;

namespace FWGameLib.InProject.EventSystem
{
    /// <summary>
    /// The register for all events in the game. Must be initialized by a singleton master game object.
    /// This class should be edited per-project that uses FWGameLib.
    /// </summary>
    public class EventRegister
    {
        public static EventRegister Instance { get; private set; }
        
        public EventRegister()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                // throw new Exception("EventRegister is a singleton and cannot be instantiated more than once.");
            }
        }
        
        /// <summary>
        /// Changing the volume on a volume slider
        /// FWGL built-in event
        /// </summary>
        public event EventHandler<FWGLChangeVolumeEvent> FWGLChangeVolumeEventHandler;
        public void Invoke(FWGLChangeVolumeEvent @event)
        {
            FWGLChangeVolumeEventHandler?.Invoke(this, @event);
        }
        
        /// <summary>
        /// AudioSystem: Fired when a audio source is finished
        /// FWGL built-in event
        /// </summary>
        public event EventHandler<FWGLSoundFinishedEvent> FWGLSoundFinishedEventHandler;
        public void Invoke(FWGLSoundFinishedEvent @event)
        {
            FWGLSoundFinishedEventHandler?.Invoke(this, @event);
        }
        
        /// <summary>
        /// AudioSystem: Forcefully stop a sound
        /// FWGL built-in event
        /// </summary>
        public event EventHandler<FWGLStopSoundEvent> FWGLStopSoundEventHandler;
        public void Invoke(FWGLStopSoundEvent @event)
        {
            FWGLStopSoundEventHandler?.Invoke(this, @event);
        }
        
        /// <summary>
        /// AudioSystem: Call when pausing audio that is paused with the game
        /// FWGL built-in event
        /// </summary>
        public event EventHandler<FWGLAudioPauseEvent> FWGLAudioPauseEventHandler;
        public void Invoke(FWGLAudioPauseEvent @event)
        {
            FWGLAudioPauseEventHandler?.Invoke(this, @event);
        }
        
        /// <summary>
        /// AudioSystem: Call when unpausing audio that is paused with the game
        /// FWGL built-in event
        /// </summary>
        public event EventHandler<FWGLAudioUnpauseEvent> FWGLAudioUnpauseEventHandler;
        public void Invoke(FWGLAudioUnpauseEvent @event)
        {
            FWGLAudioUnpauseEventHandler?.Invoke(this, @event);
        }
    }
}