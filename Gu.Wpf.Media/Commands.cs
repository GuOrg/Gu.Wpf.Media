namespace Gu.Wpf.Media
{
    using System.Windows.Input;

    /// <summary>
    /// A set of commands
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Toggle full screen command.
        /// </summary>
        public static RoutedUICommand ToggleFullScreen { get; } = new RoutedUICommand(
            "Toggle full screen.",
            nameof(ToggleFullScreen),
            typeof(Commands));

        /// <summary>
        /// Go to full screen command.
        /// </summary>
        public static RoutedUICommand BeginFullScreen { get; } = new RoutedUICommand(
            "Use full screen.",
            nameof(BeginFullScreen),
            typeof(Commands));

        /// <summary>
        /// End full screen command.
        /// </summary>
        public static RoutedUICommand EndFullScreen { get; } = new RoutedUICommand(
            "Quit full screen.",
            nameof(EndFullScreen),
            typeof(Commands));

        /// <summary>
        /// Toggle sound on and off.
        /// </summary>
        public static RoutedUICommand ToggleMute { get; } = new RoutedUICommand(
            "Toggle sound on and off.",
            nameof(ToggleMute),
            typeof(Commands));

        /// <summary>
        /// Turn sound on.
        /// </summary>
        public static RoutedUICommand UnmuteVolume { get; } = new RoutedUICommand(
            "Turn sound on.",
            nameof(UnmuteVolume),
            typeof(Commands));

        /// <summary>
        /// Change current position.
        /// </summary>
        public static RoutedUICommand Skip { get; } = new RoutedUICommand(
            "Skip.",
            nameof(Skip),
            typeof(Commands));

        /// <summary>
        /// Change current position.
        /// </summary>
        public static RoutedUICommand SkipForward { get; } = new RoutedUICommand(
            "Skip forward.",
            nameof(SkipForward),
            typeof(Commands));

        /// <summary>
        /// Change current position.
        /// </summary>
        public static RoutedUICommand SkipBack { get; } = new RoutedUICommand(
            "Skip forward.",
            nameof(SkipBack),
            typeof(Commands));
    }
}
