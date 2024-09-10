namespace FWGameLib.Common.AudioSystem
{
    public enum AudioVolumeType
    {
        /// <summary>
        /// Audio type for controlling all volume. Not normally used for individual sounds.
        /// </summary>
        Master,
        
        /// <summary>
        /// Audio type for background music.
        /// </summary>
        Music,
        
        /// <summary>
        /// Audio type for sound effects.
        /// </summary>
        SFX,
        
        /// <summary>
        /// Audio type for voice lines.
        /// </summary>
        VoiceLines
    }
}