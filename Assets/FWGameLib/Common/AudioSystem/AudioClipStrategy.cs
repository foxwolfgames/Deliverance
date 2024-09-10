namespace FWGameLib.Common.AudioSystem
{
    /// <summary>
    /// An enum representing the strategy for selecting the next AudioClip to play from a SoundClip.
    /// </summary>
    public enum AudioClipStrategy
    {
        /// <summary>
        /// Play a random AudioClip from the SoundClip's clips array.
        /// </summary>
        Random,

        /// <summary>
        /// Play the AudioClips in the SoundClip's clips array in order.
        /// </summary>
        Sequential
    }
}