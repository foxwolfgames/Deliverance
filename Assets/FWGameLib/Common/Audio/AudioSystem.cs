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
        private const string VOL_MASTER_MASTER = "Master_Master";
        private const string VOL_MASTER_MUSIC = "Master_Music";
        private const string VOL_MASTER_SFX = "Master_SFX";
        private const string VOL_MASTER_VOICELINES = "Master_VoiceLines";
        private const string VOL_SFX_MASTER = "SFX_Master";
        private const string VOL_MUSIC_MASTER = "Music_Master";
        private const string VOL_VOICELINES_MASTER = "VoiceLines_Master";

        public static AudioSystem Instance { get; private set; }

        [Header("Audio Source Pooling")]
        [SerializeField] public int audioSourcePoolSize = 30;
        [SerializeField] public GameObject pooledAudioSourcePrefab;

        [Header("Mixing")]
        [SerializeField] public AudioMixer masterMixer;
        [SerializeField] public AudioMixer musicMixer;
        [SerializeField] public AudioMixer sfxMixer;
        [SerializeField] public AudioMixer voiceLinesMixer;

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
            ChangeVolumeEvent.Handler += On;
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
            return audioSource.PlayClip(clip, position);
        }

        /// <summary>
        /// Play a sound on a parent transform
        /// If the parent is destroyed before the sound ends, the sound will continue to play on the parent's last position
        /// </summary>
        /// <param name="sound">The identifier for the sound to be played</param>
        /// <param name="parent">The parent transform</param>
        /// <returns>A PooledAudioSource if the sound was played, null otherwise</returns>
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
            return audioSource.PlayClip(clip, parent);
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

        private void On(ChangeVolumeEvent e)
        {
            switch (e.AudioType)
            {
                case AudioVolumeType.Master:
                    masterMixer.SetFloat(VOL_MASTER_MASTER, Mathf.Log10(e.Volume) * 20);
                    break;
                case AudioVolumeType.Music:
                    musicMixer.SetFloat(VOL_MUSIC_MASTER, Mathf.Log10(e.Volume) * 20);
                    break;
                case AudioVolumeType.SFX:
                    sfxMixer.SetFloat(VOL_SFX_MASTER, Mathf.Log10(e.Volume) * 20);
                    break;
                case AudioVolumeType.VoiceLines:
                    voiceLinesMixer.SetFloat(VOL_VOICELINES_MASTER, Mathf.Log10(e.Volume) * 20);
                    break;
            }
        }
    }
}