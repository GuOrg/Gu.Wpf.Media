namespace Gu.Wpf.Media
{
    /// <summary>
    /// States that can be applied to the <see cref="MediaElementWrapper"/>
    /// </summary>
    public enum MediaState : int
    {
        /// <summary>
        /// The media element should play.
        /// </summary>
        Play = 1,

        /// <summary>
        /// The media element should pause.
        /// </summary>
        Pause = 3,

        /// <summary>
        /// The media element should stop.
        /// </summary>
        Stop = 4
    }
}