using System;
using System.Collections.Generic;
using FWGameLib.Common.AudioSystem.Event;
using FWGameLib.InProject.AudioSystem;
using FWGameLib.InProject.EventSystem;
using JetBrains.Annotations;
using UnityEngine;

namespace FWGameLib.Common.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        public int audioSourcePoolSize = 30;
        public float defaultVolume = 0.5f;
        // Initialize all sounds in inspector
        public GameObject pooledAudioSourcePrefab;
        [Tooltip("Sounds scriptable objects")] public List<SoundClipSO> clipData;
        public readonly Dictionary<AudioVolumeType, float> VolumeValues = new();

        private readonly List<GameObject> _pooledAudioSources = new();
        /// <summary>
        /// Sound lookup table, initialized based on soundClips
        /// </summary>
        private readonly Dictionary<Sounds, SoundClip> _sounds = new();

        public AudioManager()
        {
            foreach (AudioVolumeType audioType in Enum.GetValues(typeof(AudioVolumeType)))
            {
                switch (audioType)
                {
                    case AudioVolumeType.VoiceLines:
                        VolumeValues.Add(audioType, 0.85f);
                        break;
                    default:
                        VolumeValues.Add(audioType, defaultVolume);
                        break;
                }
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeSoundsFromInspectorValues();
                InitializeAudioSourcePool();
            }
            else
            {
                // throw new Exception("AudioManager is a singleton and cannot be instantiated more than once.");
            }
        }

        void Start()
        {
            EventRegister.Instance.FWGLChangeVolumeEventHandler += OnChangeVolumeEvent;
        }

        /// <summary>
        /// Play a sound at the origin using the audio source object pool.
        /// </summary>
        /// <param name="clipName">The sound to be played.</param>
        public void PlaySound(Sounds clipName)
        {
            PlaySound(clipName, Vector3.zero);
        }

        // Play a sound @ a position using the audio source object pool
        public void PlaySound(Sounds clipName, Vector3 position)
        {
            SoundClip clip = GetSound(clipName);
            if (clip == null)
            {
                throw new NotImplementedException();
            }

            GameObject pooledAudioSource = GetPooledAudioSource();
            if (!pooledAudioSource)
            {
                Debug.LogWarning("No available audio sources in the pool");
                return;
            }

            pooledAudioSource.transform.position = position;
            pooledAudioSource.GetComponent<PooledAudioSource>().PlayClip(clip);
        }

        // Play a sound on a parent transform
        // NOTE POTENTIAL BUG: If the parent is destroyed before sound ends, what will happen to the pooled object???
        public void PlaySound(Sounds clipName, Transform parent)
        {
            SoundClip clip = GetSound(clipName);
            if (clip == null)
            {
                throw new NotImplementedException();
            }

            GameObject pooledAudioSource = GetPooledAudioSource();
            if (!pooledAudioSource)
            {
                Debug.LogWarning("No available audio sources in the pool");
                return;
            }

            pooledAudioSource.GetComponent<PooledAudioSource>().PlayClip(clip, parent);
        }

        [CanBeNull]
        public SoundClip GetSound(Sounds clipName)
        {
            return _sounds[clipName];
        }

        private void OnChangeVolumeEvent(object _, FWGLChangeVolumeEvent @event)
        {
            VolumeValues[@event.AudioVolumeType] = @event.VolumePercentage;
        }

        private void InitializeSoundsFromInspectorValues()
        {
            foreach (SoundClipSO soundClipData in clipData)
            {
                _sounds.Add(soundClipData.soundName, new SoundClip(soundClipData));
            }
        }

        private void InitializeAudioSourcePool()
        {
            for (int i = 0; i < audioSourcePoolSize; i++)
            {
                GameObject pooledAudioSource = Instantiate(pooledAudioSourcePrefab);
                pooledAudioSource.SetActive(false);
                DontDestroyOnLoad(pooledAudioSource);
                _pooledAudioSources.Add(pooledAudioSource);
            }
        }

        private GameObject GetPooledAudioSource()
        {
            for (int i = 0; i < _pooledAudioSources.Count; i++)
            {
                if (!_pooledAudioSources[i].activeInHierarchy)
                {
                    return _pooledAudioSources[i];
                }
            }

            return null;
        }
    }
}