using FWGameLib.Common.Audio.Event;
using FWGameLib.Common.AudioSystem;
using UnityEngine;
using UnityEngine.Audio;

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

        public PooledAudioSource PlayClip(SoundClip clip, AudioMixerGroup output, Vector3 position)
        {
            gameObject.SetActive(true);
            gameObject.transform.localPosition = Vector3.zero; // Reset local position
            gameObject.transform.position = position;

            isPausedByGame = false;
            currentSound = clip;
            audioSource.pitch = currentSound.Data.pitch;
            audioSource.clip = currentSound.NextClip();
            audioSource.Play();

            return this;
        }

        public PooledAudioSource PlayClip(SoundClip clip, AudioMixerGroup output, Transform parent)
        {
            gameObject.SetActive(true);
            parentTransform = parent;
            transform.position = parent.position;

            isPausedByGame = false;
            currentSound = clip;
            // AssignDefaultVolume();
            audioSource.pitch = currentSound.Data.pitch;
            audioSource.clip = currentSound.NextClip();
            audioSource.Play();

            return this;
        }
    }
}