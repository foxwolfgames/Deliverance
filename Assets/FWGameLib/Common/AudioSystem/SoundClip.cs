using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FWGameLib.Common.AudioSystem
{
    [Serializable]
    public class SoundClip
    {
        public SoundClipSO Data { get; private set; }
        private int _clipIndex;

        public SoundClip(SoundClipSO data)
        {
            Data = data;
            _clipIndex = 0;
        }

        public AudioClip NextClip()
        {
            switch (Data.audioClipStrategy)
            {
                case AudioClipStrategy.Sequential:
                    AudioClip clip = Data.clips[_clipIndex];
                    _clipIndex = (_clipIndex + 1) % Data.clips.Length;
                    return clip;
                case AudioClipStrategy.Random:
                default:
                    return Data.clips[Random.Range(0, Data.clips.Length)];
            }
        }
    }
}