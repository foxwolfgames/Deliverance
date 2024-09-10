using UnityEngine;

namespace FWGameLib.Common.AudioSystem
{
    public static class FWGLAudioSourceExtensions
    {
        public static void AssignVolume(this AudioSource source, float audioTypeVolume, float volume)
        {
            source.volume = audioTypeVolume * volume;
        }
    }
}