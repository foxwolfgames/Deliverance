using System.Collections.Generic;
using System.Linq;
using FWGameLib.Common.AudioSystem;
using FWGameLib.InProject.AudioSystem;
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

        public void Play(Sounds sound)
        {
            Play(sound, Vector3.zero);
        }

        public void Play(Sounds sound, Vector3 position)
        {
            SoundClip clip = _sounds[sound];
            if (clip == null)
            {
                Debug.LogWarning($"Sound {sound} not found in sound dictionary");
                return;
            }

            GameObject pooledAudioSource = GetPooledAudioSource();
            if (pooledAudioSource == null)
            {
                Debug.LogWarning("No available audio sources in pool");
                return;
            }

            PooledAudioSource audioSource = pooledAudioSource.GetComponent<PooledAudioSource>();
            audioSource.PlayClip(clip, position);
        }

        public void Play(Sounds sound, Transform parent)
        {

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