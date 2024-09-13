using System;
using Deliverance.Gameplay;
using Deliverance.Gameplay.UI;
using Deliverance.GameState.Event;
using Deliverance.UI;
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

        /// <summary>
        /// UI: Call when pressing a button
        /// </summary>
        public event EventHandler<UIButtonPressEvent> UIButtonPressEventEventHandler;
        public void Invoke(UIButtonPressEvent @event)
        {
            UIButtonPressEventEventHandler?.Invoke(this, @event);
        }

        /// <summary>
        /// GameState: Main menu scene finished loading
        /// </summary>
        public event EventHandler<MainMenuSceneLoadedEvent> MainMenuSceneLoadedEventHandler;
        public void Invoke(MainMenuSceneLoadedEvent @event)
        {
            MainMenuSceneLoadedEventHandler?.Invoke(this, @event);
        }

        /// <summary>
        /// Gameplay: Call when the in-game manager is initialized
        /// </summary>
        public event EventHandler<InGameManagerInitializeEvent> InGameManagerInitializeEventHandler;
        public void Invoke(InGameManagerInitializeEvent @event)
        {
            InGameManagerInitializeEventHandler?.Invoke(this, @event);
        }

        /// <summary>
        /// GameState: Call when game has finished loading to indicate transition into the InGameState
        /// Not necessarily indicative of when we enter the game scene, but instead when all steps in the LevelManager have been completed
        /// </summary>
        public event EventHandler<GameLoadingCompletedEvent> GameLoadingCompletedEventHandler;
        public void Invoke(GameLoadingCompletedEvent @event)
        {
            GameLoadingCompletedEventHandler?.Invoke(this, @event);
        }

        /// <summary>
        /// UI: Change the visibility of the in-game HUD
        /// </summary>
        public event EventHandler<ChangeHUDVisibilityEvent> ChangeHUDVisibilityEventHandler;
        public void Invoke(ChangeHUDVisibilityEvent @event)
        {
            ChangeHUDVisibilityEventHandler?.Invoke(this, @event);
        }

        /// <summary>
        /// UI: Update the ammo display
        /// </summary>
        public event EventHandler<UpdateAmmoDisplayEvent> UpdateAmmoDisplayEventHandler;
        public void Invoke(UpdateAmmoDisplayEvent @event)
        {
            UpdateAmmoDisplayEventHandler?.Invoke(this, @event);
        }
    }
}