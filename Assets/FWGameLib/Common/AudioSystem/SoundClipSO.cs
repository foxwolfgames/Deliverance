using FWGameLib.InProject.AudioSystem;
using UnityEngine;

namespace FWGameLib.Common.AudioSystem
{
    [CreateAssetMenu(fileName = "New Sound", menuName = "FWGameLib/Audio/Sound")]
    public class SoundClipSO : ScriptableObject
    {
        [Header("Information")]
        
        [Tooltip("The name of the sound. This is used to reference the sound in code.")]
        public Sounds soundName;
        [Tooltip("The type of audio this sound belongs to. This is used to control the volume of the sound.")]
        public AudioVolumeType audioVolumeType;
        [Tooltip("The audio clips that will be played when this sound is played.")]
        public AudioClip[] clips;

        [Header("Properties")]
        
        [Tooltip("The volume of the sound.")] [Range(0f, 1f)]
        public float volume = 1f;
        [Tooltip("The pitch of the sound.")] [Range(.1f, 3f)]
        public float pitch = 1f;
        [Tooltip("A value of 0 plays the sound fully in 2D space, a value of 1 plays the sound fully in 3D space")] [Range(0f, 1f)]
        public float dimensionality = 0.5f;
        [Tooltip("Whether the sound should use a low pass filter.")]
        public bool useLowPassFilter = false;
        [Tooltip("The cutoff frequency of the low pass filter.")]
        public float lowPassFilterCutoffFrequency = 1000f;
        
        [Header("Control")]
        
        [Tooltip("Whether the sound should loop when played.")]
        public bool loop = false;
        [Tooltip("The strategy for selecting the next AudioClip to play from the SoundClip.")]
        public AudioClipStrategy audioClipStrategy = AudioClipStrategy.Random;
        [Tooltip("Whether the sound should continue playing when the game is paused.")]
        public bool ignorePause;
    }
}