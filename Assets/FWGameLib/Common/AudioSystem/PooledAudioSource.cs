using FWGameLib.Common.AudioSystem.Event;
using FWGameLib.InProject.EventSystem;
using UnityEngine;

namespace FWGameLib.Common.AudioSystem
{
    public class PooledAudioSource : MonoBehaviour
    {
        public SoundClip currentSoundClip;
        public AudioSource audioSource;
        public AudioLowPassFilter lowPassFilter;
        [SerializeField] private Transform trackedParent;
        private bool _isPlaying;
        // Second flag to prevent the object from being deactivated when audio source is paused
        private bool _isPaused;

        void Start()
        {
            EventRegister.Instance.FWGLChangeVolumeEventHandler += OnChangeVolumeEvent;
            EventRegister.Instance.FWGLStopSoundEventHandler += OnStopSoundEvent;
            EventRegister.Instance.FWGLAudioPauseEventHandler += OnPauseEvent;
            EventRegister.Instance.FWGLAudioUnpauseEventHandler += OnUnpauseEvent;
        }

        void Update()
        {
            if (trackedParent)
            {
                transform.position = trackedParent.position;
            }

            if (_isPlaying && !audioSource.isPlaying && !_isPaused)
            {
                new FWGLSoundFinishedEvent(currentSoundClip.Data.soundName).Invoke();
                trackedParent = null;
                gameObject.SetActive(false);
            }
        }

        public void PlayClip(SoundClip clip, Transform parent = null)
        {
            // Activate this object
            gameObject.SetActive(true);
        
            // Set the parent if it exists
            if (parent)
            {
                trackedParent = parent;
                transform.position = parent.position;
            }
            else
            {
                trackedParent = null;
            }
        
            _isPaused = false;
            currentSoundClip = clip;
            // Update the audio source
            AssignDefaultVolume();
            audioSource.pitch = currentSoundClip.Data.pitch;
            audioSource.spatialBlend = currentSoundClip.Data.dimensionality;
            audioSource.clip = currentSoundClip.NextClip();
            audioSource.loop = currentSoundClip.Data.loop;
            audioSource.ignoreListenerPause = currentSoundClip.Data.ignorePause;

            lowPassFilter.enabled = currentSoundClip.Data.useLowPassFilter;
            lowPassFilter.cutoffFrequency = currentSoundClip.Data.lowPassFilterCutoffFrequency;

            audioSource.Play();
            // Flag as playing
            _isPlaying = true;
        }

        private void OnChangeVolumeEvent(object _, FWGLChangeVolumeEvent @event)
        {
            if (!_isPlaying) return;

            if (@event.AudioVolumeType == currentSoundClip.Data.audioVolumeType)
            {
                audioSource.AssignVolume(@event.VolumePercentage, currentSoundClip.Data.volume);
            }
        }

        private void OnStopSoundEvent(object _, FWGLStopSoundEvent @event)
        {
            if (!_isPlaying) return;

            if (currentSoundClip.Data.soundName == @event.SoundName)
            {
                // Update loop will take care of deactivating the object
                _isPaused = false;
                audioSource.Stop();
            }
        }

        private void OnPauseEvent(object _, FWGLAudioPauseEvent @event)
        {
            if (!_isPlaying) return;
            if (currentSoundClip.Data.ignorePause) return;
            _isPaused = true;
            audioSource.Pause();
        }

        private void OnUnpauseEvent(object _, FWGLAudioUnpauseEvent @event)
        {
            if (!_isPlaying) return;
            if (currentSoundClip.Data.ignorePause) return;
            _isPaused = false;
            audioSource.UnPause();
        }

        private void AssignDefaultVolume()
        {
            audioSource.AssignVolume(AudioManager.Instance.VolumeValues[currentSoundClip.Data.audioVolumeType],
                currentSoundClip.Data.volume);
        }
    }
}