using System.Collections.Generic;
using System.Linq;
using FWGameLib.Common.Audio.Event;
using FWGameLib.Common.AudioSystem;
using FWGameLib.InProject.AudioSystem;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;

namespace FWGameLib.Common.Audio
{
    public class AudioSystem : MonoBehaviour
    {
        public static AudioSystem Instance { get; private set; }

        [Header("Audio Source Pooling")]
        [SerializeField] public int audioSourcePoolSize = 30;
        [SerializeField] public GameObject pooledAudioSourcePrefab;

        [Header("Mixing")]
        [SerializeField] public AudioMixer mixer;

        [Tooltip("Sound scriptable objects")]
        [SerializeField] public List<SoundClipSO> clips;

        private readonly List<GameObject> _audioSourcePool = new();
        private readonly Dictionary<Sounds, SoundClip> _sounds = new();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            InitializeSoundsFromInspectorValues();
            InitializeAudioSourcePool();
        }

        /// <summary>
        /// Play a sound at the origin
        /// </summary>
        /// <param name="sound">The identifier for the sound to be played</param>
        /// <returns>A PooledAudioSource if the sound was played, null otherwise</returns>
        [CanBeNull]
        public PooledAudioSource Play(Sounds sound)
        {
            return Play(sound, Vector3.zero);
        }

        /// <summary>
        /// Play a sound at a given position
        /// </summary>
        /// <param name="sound">The identifier for the sound to be played</param>
        /// <param name="position">The world position to play the sound at</param>
        /// <returns>A PooledAudioSource if the sound was played, null otherwise</returns>
        [CanBeNull]
        public PooledAudioSource Play(Sounds sound, Vector3 position)
        {
            SoundClip clip = _sounds[sound];
            if (clip == null)
            {
                Debug.LogWarning($"Sound {sound} not found in sound dictionary");
                return null;
            }

            GameObject pooledAudioSource = GetPooledAudioSource();
            if (pooledAudioSource == null)
            {
                Debug.LogWarning("No available audio sources in pool");
                return null;
            }

            PooledAudioSource audioSource = pooledAudioSource.GetComponent<PooledAudioSource>();
            return audioSource.PlayClip(clip, mixer.outputAudioMixerGroup, position);
        }

        [CanBeNull]
        public PooledAudioSource Play(Sounds sound, Transform parent)
        {
            SoundClip clip = _sounds[sound];
            if (clip == null)
            {
                Debug.LogWarning($"Sound {sound} not found in sound dictionary");
                return null;
            }

            GameObject pooledAudioSource = GetPooledAudioSource();
            if (pooledAudioSource == null)
            {
                Debug.LogWarning("No available audio sources in pool");
                return null;
            }

            PooledAudioSource audioSource = pooledAudioSource.GetComponent<PooledAudioSource>();
            return audioSource.PlayClip(clip, mixer.outputAudioMixerGroup, parent);
        }

        public void Stop(Sounds sound)
        {
            new StopSoundEvent(sound).Invoke();
        }

        private GameObject GetPooledAudioSource()
        {
            return _audioSourcePool.FirstOrDefault(t => !t.activeInHierarchy);
        }

        private void InitializeSoundsFromInspectorValues()
        {
            foreach (SoundClipSO data in clips)
            {
                _sounds.Add(data.soundName, new SoundClip(data));
            }
        }

        private void InitializeAudioSourcePool()
        {
            for (var i = 0; i < audioSourcePoolSize; i++)
            {
                GameObject pooledAudioSource = Instantiate(pooledAudioSourcePrefab,
                                                           parent: transform,
                                                           worldPositionStays: true);
                pooledAudioSource.SetActive(false);
                _audioSourcePool.Add(pooledAudioSource);
            }
        }
    }
}