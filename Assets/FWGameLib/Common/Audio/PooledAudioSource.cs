using System;
using FWGameLib.Common.Audio.Event;
using FWGameLib.Common.AudioSystem;
using UnityEngine;

namespace FWGameLib.Common.Audio
{
    public class PooledAudioSource : MonoBehaviour
    {
        [SerializeField] public AudioSource audioSource;

        [Header("Filters")]
        [SerializeField] public AudioLowPassFilter lowPassFilter;

        [Header("Current Sound")]
        [SerializeField] private Transform parentTransform;
        [SerializeField] private SoundClip currentSound;
        [SerializeField] private bool isPlaying;
        [SerializeField] private bool isPausedByGame;

        void Awake()
        {
            StopSoundEvent.Handler += On;
            PauseAudioEvent.Handler += On;
            UnpauseAudioEvent.Handler += On;
        }

        void Update()
        {
            if (parentTransform)
            {
                transform.position = parentTransform.position;
            }

            // Notify when sound is finished via event
            if (isPlaying && !audioSource.isPlaying && !isPausedByGame)
            {
                new SoundEndEvent(currentSound, this).Invoke();
                parentTransform = null;
                gameObject.SetActive(false);
            }
        }

        public PooledAudioSource PlayClip(SoundClip clip, Vector3 position)
        {
            SetupForPlayState();
            transform.position = position;
            return PlayClip(clip);
        }

        public PooledAudioSource PlayClip(SoundClip clip, Transform parent)
        {
            SetupForPlayState();
            parentTransform = parent;
            transform.position = parent.position;
            return PlayClip(clip);
        }

        private void SetupForPlayState()
        {
            // Unexpected: We should not set up for play state if already playing
            if (gameObject.activeInHierarchy)
            {
                throw new Exception("Unexpected state: PooledAudioSource is already playing a sound");
            }

            gameObject.SetActive(true);
            isPausedByGame = false;
            transform.localPosition = Vector3.zero; // Reset local position
        }

        private PooledAudioSource PlayClip(SoundClip clip)
        {
            currentSound = clip;
            audioSource.outputAudioMixerGroup = clip.Data.output;
            audioSource.pitch = clip.Data.pitch;
            audioSource.clip = clip.NextClip();
            audioSource.loop = clip.Data.loop;
            audioSource.ignoreListenerPause = clip.Data.ignorePause;
            audioSource.spatialBlend = clip.Data.dimensionality;

            // TODO: We should rewrite the filter system to use builder pattern?
            lowPassFilter.enabled = clip.Data.useLowPassFilter;
            lowPassFilter.cutoffFrequency = clip.Data.lowPassFilterCutoffFrequency;

            audioSource.Play();
            isPlaying = true;
            return this;
        }

        private void On(StopSoundEvent e)
        {
            if (!isPlaying) return;
            if (currentSound.Data.soundName != e.Sound) return;
            isPausedByGame = false;
            // Just stop sound, update function will take care of deactivating the object
            audioSource.Stop();
        }

        private void On(PauseAudioEvent e)
        {
            if (!isPlaying) return;
            if (currentSound.Data.ignorePause) return;
            isPausedByGame = true;
            audioSource.Pause();
        }

        private void On(UnpauseAudioEvent e)
        {
            if (!isPlaying) return;
            if (currentSound.Data.ignorePause) return;
            isPausedByGame = false;
            audioSource.UnPause();
        }
    }
}